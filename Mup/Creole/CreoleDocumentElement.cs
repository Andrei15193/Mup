using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Mup.Creole.Elements;

namespace Mup.Creole
{
    internal class CreoleDocument : IParseTree
    {
        private readonly IEnumerable<CreoleElement> _elements;

        internal CreoleDocument(IEnumerable<CreoleElement> elements)
        {
            _elements = elements;
        }

        public void Accept(ParseTreeVisitor visitor)
            => Task.Run(() => AcceptAsync(visitor, CancellationToken.None)).Wait();

        public TResult Accept<TResult>(ParseTreeVisitor<TResult> visitor)
            => Task.Run(() => AcceptAsync<TResult>(visitor, CancellationToken.None)).Result;

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
    }
}