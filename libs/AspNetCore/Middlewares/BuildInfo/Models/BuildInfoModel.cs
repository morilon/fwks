using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace Fwks.AspNetCore.Middlewares.BuildInfo.Models;

public class BuildInfoModel
{
    public BuildInfoModel()
    {
        ServerName = Environment.MachineName;
        BuildMode = GetAttribute<DebuggableAttribute>() == null ? "Release" : "Debug";
        Version = Runtime.Instance.GetName().Version.ToString();
        Date = File.GetCreationTimeUtc(Runtime.Instance.Location).ToString("yyyy-MM-dd HH:mm:ss");
        Name = Runtime.Instance.GetName().Name;
        Title = GetAttribute<AssemblyTitleAttribute>()?.Title ?? Path.GetFileNameWithoutExtension(Runtime.Instance.Location);
        Description = GetAttribute<AssemblyDescriptionAttribute>()?.Description;
        Company = GetAttribute<AssemblyCompanyAttribute>()?.Company;
        CopyrightHolder = GetAttribute<AssemblyCopyrightAttribute>()?.Copyright.Replace("[YEAR]", DateTime.UtcNow.Year.ToString());
    }

    public string ServerName { get; }
    public string BuildMode { get; }
    public string Version { get; }
    public string Date { get; }
    public string Name { get; }
    public string Title { get; }
    public string Description { get; }
    public string Company { get; }
    public string CopyrightHolder { get; }

    public static BuildInfoModel Create()
    {
        return new BuildInfoModel();
    }

    private static T GetAttribute<T>() where T : Attribute
    {
        return Runtime.Instance.GetCustomAttribute<T>();
    }
}