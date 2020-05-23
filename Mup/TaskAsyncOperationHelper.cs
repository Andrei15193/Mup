using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Mup
{
    internal static class TaskAsyncOperationHelper
    {
        internal static IAsyncResult BeginParse(IMarkupParser markupParser, string text, AsyncCallback asyncCallback, object asyncState)
            => TaskAsyncOperationHelper.Run<IParseTree>(markupParser, () => markupParser.ParseAsync(text), asyncCallback, asyncState);

        internal static IAsyncResult BeginParse(IMarkupParser markupParser, TextReader reader, AsyncCallback asyncCallback, object asyncState)
            => TaskAsyncOperationHelper.Run<IParseTree>(markupParser, () => markupParser.ParseAsync(reader), asyncCallback, asyncState);

        internal static IAsyncResult BeginParse(IMarkupParser markupParser, TextReader reader, int bufferSize, AsyncCallback asyncCallback, object asyncState)
            => TaskAsyncOperationHelper.Run<IParseTree>(markupParser, () => markupParser.ParseAsync(reader, bufferSize), asyncCallback, asyncState);

        internal static IAsyncResult BeginAcceptVisitor(IParseTree parseTree, ParseTreeVisitor visitor, AsyncCallback asyncCallback, object asyncState)
            => TaskAsyncOperationHelper.Run(parseTree, () => parseTree.AcceptAsync(visitor), asyncCallback, asyncState);

        internal static IAsyncResult BeginAcceptVisitor<TResult>(IParseTree parseTree, ParseTreeVisitor<TResult> visitor, AsyncCallback asyncCallback, object asyncState)
            => TaskAsyncOperationHelper.Run(parseTree, () => parseTree.AcceptAsync(visitor), asyncCallback, asyncState);

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
    }
}