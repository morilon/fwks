using System;
using System.Collections.Generic;

namespace Fwks.Core.Extensions;

public static class ExceptionExtensions
{
    public static List<string> ExtractMessages(this Exception ex)
    {
        var messages = new List<string>();

        do
        {
            if (ex == null)
                return messages;

            messages.Add(ex.Message);

            ex = ex.InnerException;

        } while (ex != null);

        return messages;
    }
}