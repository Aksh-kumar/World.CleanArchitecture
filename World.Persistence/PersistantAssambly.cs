using System.Reflection;

namespace World.Persistence;

public static class PersistantAssambly
{
    public static readonly Assembly Instance = typeof(PersistantAssambly).Assembly;
}
