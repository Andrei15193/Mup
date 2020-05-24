﻿using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Mup
{
    /// <summary>A common interface for each markup parser implementation.</summary>
    public interface IMarkupParser
    {
        /// <summary>Parses the given <paramref name="text"/>.</summary>
        /// <param name="text">The text to parse.</param>
        /// <returns>Returns an <see cref="IParseTree"/> that can be traversed using a <see cref="ParseTreeVisitor"/>.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="text"/> is <c>null</c>.</exception>
        IParseTree Parse(string text);

        /// <summary>Parses text from the given <paramref name="reader"/>.</summary>
        /// <param name="reader">A text reader from which to parse text.</param>
        /// <returns>Returns an <see cref="IParseTree"/> that can be traversed using a <see cref="ParseTreeVisitor"/>.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="reader"/> is <c>null</c>.</exception>
        IParseTree Parse(TextReader reader);

        /// <summary>Parses text from the given <paramref name="reader"/>.</summary>
        /// <param name="reader">A text reader from which to parse text.</param>
        /// <param name="bufferSize">The buffer size to use when reading text from the <paramref name="reader"/>.</param>
        /// <returns>Returns an <see cref="IParseTree"/> that can be traversed using a <see cref="ParseTreeVisitor"/>.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="reader"/> is <c>null</c>.</exception>
        IParseTree Parse(TextReader reader, int bufferSize);
    }
}