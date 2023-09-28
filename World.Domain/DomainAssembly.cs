using System.Reflection;

namespace World.Domain;

public static class DomainAssembly
{
    public static readonly Assembly Instance = typeof(DomainAssembly).Assembly;
}
