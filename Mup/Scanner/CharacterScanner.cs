using System.IO;

namespace Mup.Scanner
{
    internal abstract class CharacterScanner
    {
        internal void Scan(TextReader reader, int bufferSize)
        {
            _Reset();

            int bufferLength;
            var buffer = new char[bufferSize];
            do
            {
                bufferLength = reader.Read(buffer, 0, bufferSize);

                for (var bufferIndex = 0; bufferIndex < bufferLength; bufferIndex++)
                    _Process(buffer[bufferIndex]);
            }
            while (bufferLength > 0);

            ScanCompleted();
        }

        protected int Line { get; private set; }

        protected int Column { get; private set; }

        protected virtual void Reset()
        {
        }

        protected abstract void Process(char character);

        protected abstract void ScanCompleted();

        private void _Reset()
        {
            Line = 1;
            Column = 1;
            Reset();
        }

        private void _Process(char character)
        {
            Process(character);

            if (character == '\n')
            {
                Line++;
                Column = 1;
            }
            else
                Column++;
        }
    }
}