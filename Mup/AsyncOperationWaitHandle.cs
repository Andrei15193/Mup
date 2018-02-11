using System;
using System.Threading;

namespace Mup
{
    internal sealed class AsyncOperationWaitHandle : WaitHandle
    {
        private readonly object _lock = new object();
        private volatile bool _isSet = false;

        internal AsyncOperationWaitHandle()
        {
#if net
            SafeWaitHandle = null;
#endif
        }

#if net
        [ObsoleteAttribute("Use the SafeWaitHandle property instead.")]
        public override IntPtr Handle
        {
            get => throw new NotSupportedException();
            set => throw new NotSupportedException();
        }
#endif

        public bool IsSet
            => _isSet;

        public void Set()
        {
            lock (_lock)
            {
                _isSet = true;
                Monitor.PulseAll(_lock);
            }
        }

        public override bool WaitOne()
        {
            if (_isSet)
                return true;

            lock (_lock)
                while (!_isSet)
                    Monitor.Wait(_lock);

            return true;
        }

        public override bool WaitOne(int millisecondsTimeout)
        {
            if (_isSet)
                return true;

            lock (_lock)
                while (!_isSet)
                    Monitor.Wait(_lock, millisecondsTimeout);

            return true;
        }

#if net
        public override bool WaitOne(int millisecondsTimeout, bool exitContext)
        {
            if (_isSet)
                return true;

            lock (_lock)
                while (!_isSet)
                    Monitor.Wait(_lock, millisecondsTimeout, exitContext);

            return true;
        }
#endif

        public override bool WaitOne(TimeSpan timeout)
        {
            if (_isSet)
                return true;

            lock (_lock)
                while (!_isSet)
                    Monitor.Wait(_lock, timeout);

            return true;
        }

#if net
        public override bool WaitOne(TimeSpan timeout, bool exitContext)
        {
            if (_isSet)
                return true;

            lock (_lock)
                while (!_isSet)
                    Monitor.Wait(_lock, timeout, exitContext);

            return true;
        }
#endif
    }
}