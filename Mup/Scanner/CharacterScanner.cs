using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Mup.Scanner
{
    internal abstract class CharacterScanner
    {
        private const int _defaultBufferSize = 2048;

        internal void Scan(string text)
        {
            if (text == null)
                throw new ArgumentNullException(nameof(text));

            _Reset();
            foreach (var character in text)
                _Process(character);
            ScanCompleted();
        }

        internal void Scan(TextReader reader)
            =>Scan(reader, DefaultBuffer);

        internal void Scan(TextReader reader, int bufferSize)
        {
            if (reader == null)
                throw new ArgumentNullException(nameof(reader));
            if (bufferSize <= 0)
                throw new ArgumentException("The buffer size must be greater than zero.", nameof(bufferSize));
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

        internal async Task ScanAsync(string text, CancellationToken cancellationToken)
        {
            if (text == null)
                throw new ArgumentNullException(nameof(text));

            _Reset();
            foreach (var character in text)
            {
                _Process(character);
                if (Index / DefaultBuffer == 0)
                {
                    await Task.Yield();
                    cancellationToken.ThrowIfCancellationRequested();
                }
            }
            await Task.Yield();
            cancellationToken.ThrowIfCancellationRequested();

            ScanCompleted();
        }

        internal Task ScanAsync(TextReader reader, CancellationToken cancellationToken)
            => ScanAsync(reader, DefaultBuffer, cancellationToken);

        internal async Task ScanAsync(TextReader reader, int bufferSize, CancellationToken cancellationToken)
        {
            if (reader == null)
                throw new ArgumentNullException(nameof(reader));
            if (bufferSize <= 0)
                throw new ArgumentException("The buffer size must be greater than zero.", nameof(bufferSize));

            _Reset();

            int bufferLength;
            var buffer = new char[bufferSize];
            var textBuilder = new StringBuilder();
            do
            {
                bufferLength = await reader.ReadAsync(buffer, 0, bufferSize).ConfigureAwait(false);
                cancellationToken.ThrowIfCancellationRequested();

                for (var bufferIndex = 0; bufferIndex < bufferLength; bufferIndex++)
                    _Process(buffer[bufferIndex]);
            }
            while (bufferLength > 0);

            ScanCompleted();
        }

        protected int Line { get; private set; }

        protected int Column { get; private set; }

        protected int Index { get; private set; }

        protected virtual int DefaultBuffer
            => _defaultBufferSize;

        protected virtual void Reset()
        {
        }

        protected abstract void Process(char character);

        protected abstract void ScanCompleted();

        private void _Reset()
        {
            Line = 1;
            Column = 1;
            Index = 0;
            Reset();
        }

        private void _Process(char character)
        {
            Process(character);

            Index++;
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