using System.Collections.Generic;

namespace Mup.Tests
{
    public static class CreoleParseroptionsTestData
    {
        public static IEnumerable<object[]> InlineProtocolsTestCases { get; } = new List<object[]>
        {
            new object[]
            {
                "http"
            },
            new object[]
            {
                "https"
            },
            new object[]
            {
                "http,https"
            }
        };

        public static IEnumerable<object[]> InvalidInlineProtocolsTestCases { get; } = new List<object[]>
        {
            new object[]
            {
                null
            },
            new object[]
            {
                ""
            },
            new object[]
            {
                " "
            },
            new object[]
            {
                "\t"
            },
            new object[]
            {
                "\n"
            },
            new object[]
            {
                "\r"
            }
        };
    }
}