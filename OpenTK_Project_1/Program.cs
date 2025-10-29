using GameEngine;
using OpenTK.Windowing.Desktop;

namespace RenderEngine
{
    internal static class Program
    {
        static void Main()
        {
            using (Game game = new Game(800, 600, "LearnOpenTK"))
            {
                game.Run();
            }
        }
    }
}