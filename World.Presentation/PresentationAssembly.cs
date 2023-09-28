using System.Reflection;

namespace World.Presentation
{
    public static class PresentationAssembly
    {
        public static readonly Assembly Instance = typeof(PresentationAssembly).Assembly;
    }
}