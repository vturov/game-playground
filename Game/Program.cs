namespace Game
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            using var app = new Application();
            app.Run(args);
        }
    }
}