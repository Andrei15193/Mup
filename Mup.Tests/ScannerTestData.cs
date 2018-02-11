using System.Collections.Generic;

namespace Mup.Tests
{
    public static class ScannerTestData
    {
        public static IEnumerable<object[]> BufferSizeTestData { get; } = new List<object[]>
        {
            new object[]
            {
                10
            },
            new object[]
            {
                2
            },
            new object[]
            {
                1
            },
            new object[]
            {
                100
            }
        };

        public static IEnumerable<object[]> InvalidBufferSizeTestData { get; } = new List<object[]>
        {
            new object[]
            {
                -1
            },
            new object[]
            {
                -2
            },
            new object[]
            {
                0
            }
        };

        public static IEnumerable<object[]> UnrecognizedCharacterTestData { get; } = new List<object[]>
        {
            new object[]
            {
                "baaa\naaaa", 'b', 1, 1
            },
            new object[]
            {
                "aaaa\nbaaa", 'b', 2, 1
            },
            new object[]
            {
                "aaba\naaaa", 'b', 1, 3
            },
            new object[]
            {
                "aaaa\naaba", 'b', 2, 3
            },
            new object[]
            {
                "aaaa\r\nbaaa", 'b', 2, 1
            },
            new object[]
            {
                "aaaa\r\naaba", 'b', 2, 3
            },
            new object[]
            {
                "aaaa\n\rbaaa", 'b', 2, 2
            },
            new object[]
            {
                "aaaa\n\raaba", 'b', 2, 4
            }
        };
    }
}