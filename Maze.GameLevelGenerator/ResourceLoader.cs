using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace Maze.GameLevelGenerator
{
    static class ResourceLoader
    {
        public static Image<Rgba32> LoadEmbeddedResource(
            this Assembly assembly, string name)
        {
            using (var stream = assembly.GetManifestResourceStream(name))
            {
                return Image.Load<Rgba32>(stream);
            }
        }

        public static IEnumerable<Image<Rgba32>> LoadEmbeddedResources(
            this Assembly assembly, IEnumerable<string> names)
        {
            return names.Select(name => LoadEmbeddedResource(assembly, name)).ToArray();
        }
    }
}