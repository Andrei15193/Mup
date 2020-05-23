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
        }

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

        public override bool WaitOne(TimeSpan timeout)
        {
            if (_isSet)
                return true;

            lock (_lock)
                while (!_isSet)
                    Monitor.Wait(_lock, timeout);

            return true;
        }
    }
}