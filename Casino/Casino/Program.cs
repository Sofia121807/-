namespace Casino;

internal class Program
{
    static void Main(string[] args)
    {
        CasinoGame game = new CasinoGame();

        game.OnMessage += Game_OnMessage;

        game.Start();

        game.OnMessage -= Game_OnMessage;
    }
    static void Game_OnMessage(object? sender, GameMessageEventArgs e)
    {
        Console.WriteLine(e.Message);
    }
}
