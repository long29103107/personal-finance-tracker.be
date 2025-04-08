using System.Reflection;

namespace Tracker.Api;

public static class TrackerApiReference
{
    public static readonly Assembly Assembly = typeof(TrackerApiReference).Assembly;
    public static readonly string AssemblyName = typeof(TrackerApiReference).Assembly.GetName().Name;
}