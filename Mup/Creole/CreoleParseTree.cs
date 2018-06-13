using System;
using System.Collections.Generic;
using Mup.Creole.Elements;
#if netstandard10
using System.Threading;
using System.Threading.Tasks;
#endif

namespace Mup.Creole
{
    internal class CreoleParseTree : IParseTree
    {
        private readonly IEnumerable<CreoleElement> _elements;

        internal CreoleParseTree(IEnumerable<CreoleElement> elements)
        {
            _elements = elements;
        }

#if net20
        public void Accept(ParseTreeVisitor visitor)
        {
            visitor.BeginVisit();
            foreach (var blockElement in _elements)
                blockElement.Accept(visitor);
            visitor.EndVisit();
        }

        public TResult Accept<TResult>(ParseTreeVisitor<TResult> visitor)
        {
            Accept((ParseTreeVisitor)visitor);
            return visitor.GetResult();
        }
#else
        public void Accept(ParseTreeVisitor visitor)
            => Task.Run(() => AcceptAsync(visitor, CancellationToken.None)).Wait();

        public TResult Accept<TResult>(ParseTreeVisitor<TResult> visitor)
            => Task.Run(() => AcceptAsync<TResult>(visitor, CancellationToken.None)).Result;
#endif

        public IAsyncResult BeginAccept(ParseTreeVisitor visitor)
            => BeginAccept(visitor, null, null);

        public IAsyncResult BeginAccept(ParseTreeVisitor visitor, object asyncState)
            => BeginAccept(visitor, null, asyncState);

        public IAsyncResult BeginAccept(ParseTreeVisitor visitor, AsyncCallback asyncCallback)
            => BeginAccept(visitor, asyncCallback, null);

        public IAsyncResult BeginAccept(ParseTreeVisitor visitor, AsyncCallback asyncCallback, object asyncState)
        {
            if (visitor == null)
                throw new ArgumentNullException(nameof(visitor));

            return TaskAsyncOperationHelper.BeginAcceptVisitor(this, visitor, asyncCallback, asyncState);
        }

        public IAsyncResult BeginAccept<TResult>(ParseTreeVisitor<TResult> visitor)
            => BeginAccept<TResult>(visitor, null, null);

        public IAsyncResult BeginAccept<TResult>(ParseTreeVisitor<TResult> visitor, object asyncState)
            => BeginAccept<TResult>(visitor, null, asyncState);

        public IAsyncResult BeginAccept<TResult>(ParseTreeVisitor<TResult> visitor, AsyncCallback asyncCallback)
            => BeginAccept<TResult>(visitor, asyncCallback, null);

        public IAsyncResult BeginAccept<TResult>(ParseTreeVisitor<TResult> visitor, AsyncCallback asyncCallback, object asyncState)
        {
            if (visitor == null)
                throw new ArgumentNullException(nameof(visitor));

            return TaskAsyncOperationHelper.BeginAcceptVisitor(this, visitor, asyncCallback, asyncState);
        }
        public void EndAccept(IAsyncResult asyncResult)
            => TaskAsyncOperationHelper.Wait(this, asyncResult);

        public TResult EndAccept<TResult>(IAsyncResult asyncResult)
            => TaskAsyncOperationHelper.GetResult<TResult>(this, asyncResult);

#if netstandard10
        public Task AcceptAsync(ParseTreeVisitor visitor)
            => AcceptAsync(visitor, CancellationToken.None);

        public async Task AcceptAsync(ParseTreeVisitor visitor, CancellationToken cancellationToken)
        {
            await visitor.BeginVisitAsync(cancellationToken).ConfigureAwait(false);
            foreach (var blockElement in _elements)
            {
                await blockElement.AcceptAsync(visitor, cancellationToken).ConfigureAwait(false);
                cancellationToken.ThrowIfCancellationRequested();
            }
            await visitor.EndVisitAsync(cancellationToken).ConfigureAwait(false);
        }

        public Task<TResult> AcceptAsync<TResult>(ParseTreeVisitor<TResult> visitor)
            => AcceptAsync(visitor, CancellationToken.None);

        public async Task<TResult> AcceptAsync<TResult>(ParseTreeVisitor<TResult> visitor, CancellationToken cancellationToken)
        {
            await AcceptAsync((ParseTreeVisitor)visitor, cancellationToken).ConfigureAwait(false);

            var result = await visitor.GetResultAsync(cancellationToken).ConfigureAwait(false);
            cancellationToken.ThrowIfCancellationRequested();
            return result;
        }
#endif
    }
}