using System;
using System.IO;
using Axe.Cli.Parser;
using Axe.Cli.Parser.Transformers;
using Maze.GameLevelGenerator;
using C = System.Console;

namespace Maze.Console
{
    static class Program
    {
        const int InvalidArgumentCode = -2;

        static int Main(string[] args)
        {
            var parser = new ArgsParserBuilder()
                .BeginDefaultCommand()
                .AddOptionWithValue("kind", 'k', "Specify the kind of maze to render.", true)
                .AddOptionWithValue("row", 'r', "Specify the number of rows in the maze.", true,
                    new IntegerTransformer())
                .AddOptionWithValue("column", 'c', "Specify the number of columns in the maze.", true,
                    new IntegerTransformer())
                .EndCommand()
                .Build();

            var argsParsingResult = parser.Parse(args);
            if (!argsParsingResult.IsSuccess)
            {
                PrintUsage(argsParsingResult.Error.Code.ToString(), argsParsingResult.Error.Trigger);
                return (int)argsParsingResult.Error.Code;
            }

            var imagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "maze.png");
            return RenderPredefinedMaze(argsParsingResult, imagePath);
        }

        static int RenderPredefinedMaze(ArgsParsingResult argsParsingResult, string imagePath)
        {
            var mazeKind = argsParsingResult.GetFirstOptionValue<string>("--kind");
            var numberOfRows = argsParsingResult.GetFirstOptionValue<int>("--row");
            var numberOfColumns = argsParsingResult.GetFirstOptionValue<int>("--column");

            if (numberOfRows <= 0 || numberOfColumns <= 0)
            {
                PrintUsage("InvalidArgument", $"--row {numberOfRows} --column {numberOfColumns}");
                return InvalidArgumentCode;
            }

            using (var stream = File.Create(imagePath))
            {
                return RenderPredefinedMaze(stream, mazeKind, new MazeGridSettings(numberOfRows, numberOfColumns));
            }
        }

        static int RenderPredefinedMaze(FileStream stream, string mazeKind, MazeGridSettings mazeGridSettings)
        {
            var writer = new WriterFactory().Build(mazeKind);
            if (writer == null)
            {
                PrintUsage("InvalidArgument", $"--kind {mazeKind}");
                return InvalidArgumentCode;
            }

            writer.Write(stream, mazeGridSettings);
            return 0;
        }

        static void PrintUsage(string code, string trigger)
        {
            C.Error.WriteLine("Error: ");
            C.Error.WriteLine($"Code: {code}. Tigger: {trigger}");
        }
    }
}
