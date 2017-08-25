namespace Mup
{
    internal interface IToken<TTokenCode>
        where TTokenCode : struct
    {
        TTokenCode Code { get; }

        int Start { get; }

        int Length { get; }

        int End { get; }

        IToken<TTokenCode> Previous { get; }

        IToken<TTokenCode> Next { get; }
    }
}