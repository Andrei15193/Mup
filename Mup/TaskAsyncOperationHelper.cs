using System;
using System.Threading;
#if netstandard10
using System.Threading.Tasks;
#endif

namespace Mup
{
#if net20
    internal delegate void AsyncOperation();

    internal delegate TResult AsyncOperation<TResult>();
#endif

    internal static class TaskAsyncOperationHelper
    {
#if net20
        internal static ITaskAsyncOperation Run(object instance, AsyncOperation asyncOperation, AsyncCallback asyncCallback, object asyncState)
        {
            var taskAsyncOperation = new TaskAsyncOperation(instance, asyncOperation, asyncCallback, asyncState);
            taskAsyncOperation.Start();
            return taskAsyncOperation;
        }

        internal static ITaskAsyncOperation Run<TResult>(object instance, AsyncOperation<TResult> asyncOperation, AsyncCallback asyncCallback, object asyncState)
        {
            var taskAsyncOperation = new TaskAsyncOperation<TResult>(instance, asyncOperation, asyncCallback, asyncState);
            taskAsyncOperation.Start();
            return taskAsyncOperation;
        }

#else
        internal static ITaskAsyncOperation Run(object instance, Func<Task> asyncOperation, AsyncCallback asyncCallback, object asyncState)
        {
            var taskAsyncOperation = new TaskAsyncOperation(instance, asyncOperation, asyncCallback, asyncState);
            taskAsyncOperation.Start();
            return taskAsyncOperation;
        }

        internal static ITaskAsyncOperation Run<TResult>(object instance, Func<Task<TResult>> asyncOperation, AsyncCallback asyncCallback, object asyncState)
        {
            var taskAsyncOperation = new TaskAsyncOperation<TResult>(instance, asyncOperation, asyncCallback, asyncState);
            taskAsyncOperation.Start();
            return taskAsyncOperation;
        }
#endif

        internal static void Wait(object instance, IAsyncResult asyncResult)
        {
            if (asyncResult == null)
                throw new ArgumentNullException(nameof(asyncResult));

            var taskAsyncOperation = (asyncResult as ITaskAsyncOperation);
            if (taskAsyncOperation == null || taskAsyncOperation.Instance != instance)
                throw new ArgumentException("The given async result was not returned by one of the Begin methods of this instance.", nameof(asyncResult));

            taskAsyncOperation.Wait();
        }

        internal static TResult GetResult<TResult>(object instance, IAsyncResult asyncResult)
        {
            if (asyncResult == null)
                throw new ArgumentNullException(nameof(asyncResult));

            var taskAsyncOperation = (asyncResult as ITaskAsyncOperation<TResult>);
            if (taskAsyncOperation == null || taskAsyncOperation.Instance != instance)
                throw new ArgumentException("The given async result was not returned by one of the Begin methods of this instance.", nameof(asyncResult));

            taskAsyncOperation.Wait();
            return taskAsyncOperation.Result;
        }

#if net20
        private class TaskAsyncOperation : ITaskAsyncOperation
        {
            private readonly int _initalTheadId;
            private readonly AsyncOperationWaitHandle _waitHandle;
            private readonly object _instance;
            private readonly AsyncOperation _asyncOperation;
            private readonly AsyncCallback _asyncCallback;
            private readonly object _asyncState;
            private volatile Exception _exception;
            private volatile bool _completedSynchronously;

            protected TaskAsyncOperation(object instance, AsyncCallback asyncCallback, object asyncState)
            {
                _initalTheadId = Thread.CurrentThread.ManagedThreadId;
                _waitHandle = new AsyncOperationWaitHandle();
                _instance = instance;
                _asyncCallback = asyncCallback;
                _asyncState = asyncState;
                _exception = null;
                _completedSynchronously = false;
            }

            internal TaskAsyncOperation(object instance, AsyncOperation asyncOperation, AsyncCallback asyncCallback, object asyncState)
                : this(instance, asyncCallback, asyncState)
            {
                _asyncOperation = asyncOperation;
            }

            public bool IsCompleted
                => _waitHandle.IsSet;

            public WaitHandle AsyncWaitHandle
                => _waitHandle;

            public object AsyncState
                => _asyncState;

            public bool CompletedSynchronously
            {
                get
                {
                    if (!IsCompleted)
                        throw new InvalidOperationException();

                    return _completedSynchronously;
                }
            }

            object ITaskAsyncOperation.Instance
                => _instance;

            public void Wait()
            {
                _waitHandle.WaitOne();
                if (_exception != null)
                    throw _exception;
            }

            protected virtual void Execute()
            {
                _asyncOperation();
            }

            internal void Start()
            {
                ThreadPool.QueueUserWorkItem(
                    delegate
                    {
                        try
                        {
                            Execute();
                            _CompleteAsyncOperation();
                        }
                        catch (Exception exception)
                        {
                            _CompleteAsyncOperation(exception);
                        }
                    });
            }

            private void _CompleteAsyncOperation()
            {
                _completedSynchronously = (_initalTheadId == Thread.CurrentThread.ManagedThreadId);
                _waitHandle.Set();
                _asyncCallback?.Invoke(this);
            }

            private void _CompleteAsyncOperation(Exception exception)
            {
                _exception = exception;
                _completedSynchronously = (_initalTheadId == Thread.CurrentThread.ManagedThreadId);
                _waitHandle.Set();
                _asyncCallback?.Invoke(this);
            }
        }

        private class TaskAsyncOperation<TResult> : TaskAsyncOperation, ITaskAsyncOperation<TResult>
        {
            private readonly AsyncOperation<TResult> _asyncOperation;
            private volatile object _result;

            internal TaskAsyncOperation(object instance, AsyncOperation<TResult> asyncOperation, AsyncCallback asyncCallback, object asyncState)
                : base(instance, asyncCallback, asyncState)
            {
                _asyncOperation = asyncOperation;
                _result = null;
            }

            protected override void Execute()
            {
                _result = _asyncOperation();
            }

            public TResult Result
            {
                get
                {
                    Wait();
                    return (TResult)_result;
                }
            }
        }

#else
        private class TaskAsyncOperation : ITaskAsyncOperation
        {
            private readonly object _instance;
            private readonly AsyncCallback _asyncCallback;
            private readonly object _asyncState;
            private readonly Func<Task> _asyncOperation;
            private volatile Task _task;

            protected TaskAsyncOperation(object instance, AsyncCallback asyncCallback, object asyncState)
            {
                _instance = instance;
                _asyncState = asyncState;
                _asyncCallback = asyncCallback;
            }

            internal TaskAsyncOperation(object instance, Func<Task> asyncOperation, AsyncCallback asyncCallback, object asyncState)
                : this(instance, asyncCallback, asyncState)
            {
                _instance = instance;
                _asyncCallback = asyncCallback;
                _asyncState = asyncState;
                _asyncOperation = asyncOperation;
            }

            public bool IsCompleted
                => _task.IsCompleted;

            public WaitHandle AsyncWaitHandle
                => ((IAsyncResult)_task).AsyncWaitHandle;

            public object AsyncState
                => _asyncState;

            public bool CompletedSynchronously
                => ((IAsyncResult)_task).CompletedSynchronously;

            public object Instance
                => _instance;

            public void Wait()
            {
                try
                {
                    _task.Wait();
                }
                catch (AggregateException aggregateException) when (aggregateException.InnerExceptions.Count == 1)
                {
                    throw aggregateException.InnerException;
                }
            }

            protected virtual Task ExecuteAsync()
                => _asyncOperation();

            internal void Start()
            {
                _task = Task.Run(() => ExecuteAsync());
                if (_asyncCallback != null)
                    _task.ContinueWith(result => _asyncCallback.Invoke(this));
            }
        }

        private class TaskAsyncOperation<TResult> : TaskAsyncOperation, ITaskAsyncOperation<TResult>
        {
            private readonly Func<Task<TResult>> _asyncOperation;
            private volatile object _result;

            internal TaskAsyncOperation(object instance, Func<Task<TResult>> asyncOperation, AsyncCallback asyncCallback, object asyncState)
                : base(instance, asyncCallback, asyncState)
            {
                _asyncOperation = asyncOperation;
            }

            public TResult Result
            {
                get
                {
                    Wait();
                    return (TResult)_result;
                }
            }

            protected override async Task ExecuteAsync()
            {
                _result = await _asyncOperation();
            }
        }
#endif
    }
}