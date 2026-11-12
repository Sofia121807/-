using Casino.Interfaces;
using Casino.Models;
using System;

namespace Casino.Core;

internal class CasinoGame
{
    private int _balance = 10000;

    private IStatisticManager _statisticManager;
    private IMenuService _menuService;
    private IGameSession _gameSession;

    public event EventHandler<GameMessageEventArgs>? OnMessage;

    public CasinoGame(IStatisticManager statisticManager, IMenuService menuService, IGameSession gameSession)
    {
        _statisticManager = statisticManager;
        _menuService = menuService;
        _gameSession = gameSession;
    }

    public void Start()
    {
        OnGameMessage("Добро пожаловать в Казино!");

        while (true)
        {
            string startChoice = _menuService.ShowMainMenu();

            if (startChoice == "2")
            {
                _statisticManager.ShowHistory();
                continue;
            }
            else if (startChoice == "3")
            {
                OnGameMessage("Спасибо за игру! До встречи в нашем казино))");
                return;
            }
            else if (startChoice == "1")
            {
                PlayGame();
            }

            else OnGameMessage("Ошибка. Введите 1, 2 или 3.");
        }
    }

    private void PlayGame() 
    {
        OnGameMessage($"Ваш стартовый баланс: {_balance} рублей");

        bool keepPlayng = true;

        while (keepPlayng)
        {
            if (_balance <= 0)
            {
                OnGameMessage("Ваш баланс отрицательный! Игра окончена!");
                return;
            }

            int result = _gameSession.PlayRound(_balance);
            _balance += result;

            if (result > 0) _statisticManager.SaveResult($"Победа. Выигрыш: {result} рублей.");
            else if (result < 0) _statisticManager.SaveResult($"Проигрыш. Ставка: {-result} рублей.");

            OnGameMessage($"\nВаш баланс: {_balance}");

            if (_balance > 0)
            {
                Console.WriteLine("Хотите сыграть еще раз? (y/n): ");
                string answer = Console.ReadLine();
                if (answer != "Y" && answer != "y") keepPlayng = false;
            }
        }
    }

    protected virtual void OnGameMessage(string message)
    {
        OnMessage?.Invoke(this, new GameMessageEventArgs(message));
    }
}