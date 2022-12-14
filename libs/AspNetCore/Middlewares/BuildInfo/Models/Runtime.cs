using System.Reflection;

namespace Fwks.AspNetCore.Middlewares.BuildInfo.Models;

public static class Runtime
{
    private static Assembly _assembly;

    public static Assembly Instance
    {
        get
        {
            return _assembly ??= Assembly.GetEntryAssembly();
        }
    }
}