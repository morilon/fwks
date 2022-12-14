using System;

namespace Fwks.Core.Exceptions;

public sealed class FormatConversionException : Exception
{
    public FormatConversionException(string format)
        : base($"""The provided format "{format}" is not valid.""")
    { }
}