using System;

namespace Mup
{
    internal interface ITaskAsyncOperation : IAsyncResult
    {
        object Instance { get; }

        void Wait();
    }

    internal interface ITaskAsyncOperation<TResult> : ITaskAsyncOperation
    {
        TResult Result { get; }
    }
}