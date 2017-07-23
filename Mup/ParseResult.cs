using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Mup
{
    internal class ParseResult : IParseResult
    {
        private readonly IEnumerable<ElementMark> _marks;

        internal ParseResult(string text, IEnumerable<ElementMark> marks)
        {
            Text = text;
            _marks = marks;
        }

        public string Text { get; }

        public Task AcceptAsync(ParseResultVisitor visitor)
            => AcceptAsync(visitor, CancellationToken.None);

        public async Task AcceptAsync(ParseResultVisitor visitor, CancellationToken cancellationToken)
        {
            if (visitor == null)
                throw new ArgumentNullException(nameof(visitor));

            await visitor.VisitAsync(Text, _marks, cancellationToken);
        }
    }
}