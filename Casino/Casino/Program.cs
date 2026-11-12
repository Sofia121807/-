using Casino.Core;
using Casino.Interfaces;
using Casino.Models;
using Casino.Services;

namespace Casino;

internal class Program
{
    static void Main(string[] args)
    {
        IStatisticManager stats = new StatisticManager();
        IMenuService menu = new MenuService();
        IGameSession session = new GameSession();

        CasinoGame game = new CasinoGame(stats, menu, session);

        game.OnMessage += Game_OnMessage;

        game.Start();

        game.OnMessage -= Game_OnMessage;
    }

    static void Game_OnMessage(object? sender, GameMessageEventArgs e)
    {
        Console.WriteLine(e.Message);
    }
}