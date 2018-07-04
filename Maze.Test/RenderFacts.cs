using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Maze.Common.Renderers;
using Maze.GameLevelGenerator;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using Xunit;

namespace Maze.Test
{
    public class RenderFacts
    {
        class TestComponentFactory : IGameLevelComponentFactory
        {   
            public IEnumerable<AreaRenderer> CreateBackgroundRenderers()
            {
                yield return new AreaColorRender(Rgba32.Blue);
            }

            public IEnumerable<CellRenderer> CreateWallRenderers()
            {
                var north = CreateImage(Rgba32.LightBlue);
                var south = CreateImage(Rgba32.LightCoral);
                var east = CreateImage(Rgba32.LightGoldenrodYellow);
                var west = CreateImage(Rgba32.LightGray);
                var northSouth = CreateImage(Rgba32.LightGreen);
                var northEast = CreateImage(Rgba32.LightPink);
                var northWest = CreateImage(Rgba32.LightSalmon);
                var southEast = CreateImage(Rgba32.LightSeaGreen);
                var southWest = CreateImage(Rgba32.LightSkyBlue);
                var eastWest = CreateImage(Rgba32.LightSlateGray);
                var northSouthEast = CreateImage(Rgba32.LightSteelBlue);
                var northSouthWest = CreateImage(Rgba32.LightYellow);
                var northEastWest = CreateImage(Rgba32.Aqua);
                var southEastWest = CreateImage(Rgba32.Aquamarine);
                var northSouthEastWest = CreateImage(Rgba32.Azure);

                yield return new DirectedCellRenderer(
                    north,
                    south,
                    east,
                    west,
                    northSouth,
                    northEast,
                    northWest,
                    southEast,
                    southWest,
                    eastWest,
                    northSouthEast,
                    northSouthWest,
                    northEastWest,
                    southEastWest,
                    northSouthEastWest);
            }

            public IEnumerable<CellRenderer> CreateGroundRenderers()
            {
                var north = CreateImage(Rgba32.DarkBlue);
                var south = CreateImage(Rgba32.DarkCyan);
                var east = CreateImage(Rgba32.DarkGoldenrod);
                var west = CreateImage(Rgba32.DarkGray);
                var northSouth = CreateImage(Rgba32.DarkGreen);
                var northEast = CreateImage(Rgba32.DarkKhaki);
                var northWest = CreateImage(Rgba32.DarkMagenta);
                var southEast = CreateImage(Rgba32.DarkOliveGreen);
                var southWest = CreateImage(Rgba32.DarkOrange);
                var eastWest = CreateImage(Rgba32.DarkOrchid);
                var northSouthEast = CreateImage(Rgba32.DarkRed);
                var northSouthWest = CreateImage(Rgba32.DarkSalmon);
                var northEastWest = CreateImage(Rgba32.DarkSeaGreen);
                var southEastWest = CreateImage(Rgba32.DarkSlateBlue);
                var northSouthEastWest = CreateImage(Rgba32.DarkSlateGray);
                var unknown = CreateImage(Rgba32.DarkTurquoise);
                
                yield return new DirectedCellRenderer(
                    north,
                    south,
                    east,
                    west,
                    northSouth,
                    northEast,
                    northWest,
                    southEast,
                    southWest,
                    eastWest,
                    northSouthEast,
                    northSouthWest,
                    northEastWest,
                    southEastWest,
                    northSouthEastWest,
                    unknown);
            }

            public GameLevelRenderSettings CreateSettings()
            {
                return new GameLevelRenderSettings(20, 20);
            }

            static Image<Rgba32> CreateImage(Rgba32 backgroundColor)
            {
                return new Image<Rgba32>(Configuration.Default, 20, 20, backgroundColor);
            }
        }

        class TestLevelWriter
        {
            public void Write(Stream stream)
            {
                var renderGrid = ComplexMazeFixture.Create();
                var factory = new TestComponentFactory();
                var renderer = new NormalGameLevelRenderer(
                    factory.CreateBackgroundRenderers(),
                    factory.CreateGroundRenderers(),
                    factory.CreateWallRenderers(),
                    factory.CreateSettings());
                using (renderer)
                {
                    renderer.Render(renderGrid, stream);
                }
            }
            
            public void Write3D(Stream stream)
            {
                var renderGrid = ComplexMazeFixture.Create();
                var factory = new TestComponentFactory();
                var renderer = new Fake3DGameLevelRenderer(
                    factory.CreateBackgroundRenderers(),
                    factory.CreateGroundRenderers(),
                    factory.CreateWallRenderers(),
                    factory.CreateSettings());
                using (renderer)
                {
                    renderer.Render(renderGrid, stream);
                }
            }
        }

        [Fact]
        public void should_render_normal_game_level()
        {
            var stream = new MemoryStream();
            new TestLevelWriter().Write(stream);
            stream.Seek(0, SeekOrigin.Begin);
            
            using (var actual = Image.Load<Rgba32>(stream))
            using (var expectedStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(
                "Maze.Test.Resources.test.png"))
            using (var expected = Image.Load<Rgba32>(expectedStream))
            {
                AssertImageEqual(actual, expected);
            }
        }

        [Fact]
        public void should_render_fake_3d_game_level()
        {
            var stream = new MemoryStream();
            new TestLevelWriter().Write3D(stream);
            stream.Seek(0, SeekOrigin.Begin);
            
            using (var actual = Image.Load<Rgba32>(stream))
            using (var expectedStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(
                "Maze.Test.Resources.test_3d.png"))
            using (var expected = Image.Load<Rgba32>(expectedStream))
            {
                AssertImageEqual(actual, expected);
            }
        }

        static void AssertImageEqual(Image<Rgba32> actual, Image<Rgba32> expected)
        {
            Assert.Equal(expected.Width, actual.Width);
            Assert.Equal(expected.Height, actual.Height);
            for (var x = 0; x < expected.Width; ++x)
            {
                for (var y = 0; y < expected.Height; ++y)
                {
                    Assert.Equal(expected[x, y], actual[x, y]);
                }
            }
        }
    }
}