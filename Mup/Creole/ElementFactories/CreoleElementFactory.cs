using System.Collections.Generic;
using Mup.Creole.Elements;
using static Mup.Creole.CreoleTokenCode;

namespace Mup.Creole.ElementFactories
{
    internal abstract class CreoleElementFactory
    {
        protected CreoleElementFactory(CreoleParserContext context)
        {
            Context = context;
        }

        internal abstract CreoleFactoryResult TryCreateFrom(CreoleToken token);

        protected static IEnumerable<CreoleTextElement> EmptyContent { get; } = new[] { new CreoleTextElement(string.Empty) };

        protected CreoleParserContext Context { get; }

        protected bool ContainsLineFeed(CreoleToken token)
        {
            if (token?.Code != WhiteSpace)
                return false;

            var index = token.StartIndex;
            while (index < token.EndIndex && Context.Text[index] != '\n')
                index++;
            return (index < token.EndIndex);
        }

        protected bool IsSingleOnLine(CreoleToken token)
            => ((token.Previous == null || ContainsLineFeed(token.Previous))
                && (token.Next == null || ContainsLineFeed(token.Next)));

        protected bool IsAtBeginningOfLine(CreoleToken token)
            => (token.Previous == null || Context.Text[token.StartIndex - 1] == '\n');

        protected bool IsAtEndOfLine(CreoleToken token)
            => (token.Next == null
                || Context.Text[token.Next.StartIndex] == '\n'
                || (token.Next.Length >= 2
                    && Context.Text[token.Next.StartIndex] == '\r'
                    && Context.Text[token.Next.StartIndex + 1] == '\n'));

        protected IEnumerable<CharacterMatch> FindLineFeeds(CreoleToken token)
        {
            if (token?.Code == WhiteSpace)
                for (var index = token.StartIndex; index < token.EndIndex; index++)
                    if (Context.Text[index] == '\n')
                        yield return new CharacterMatch('\n', index);
        }

        protected struct CharacterMatch
        {
            internal CharacterMatch(char character, int index)
            {
                Character = character;
                Index = index;
            }

            internal char Character { get; }

            internal int Index { get; }
        }
    }
}