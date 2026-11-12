using Casino.Interfaces;
using System;
using System.IO;

namespace Casino.Services;

public class StatisticManager : IStatisticManager
{
    private string _filePath = "casino_stats.txt";

    public void SaveResult (string result)
    {
        File.AppendAllText(_filePath, $"{DateTime.Now:dd.MM.yyyy H:mm} - {result}\n");
    }

    public void ShowHistory()
    {
        if (!File.Exists(_filePath) || string.IsNullOrEmpty(File.ReadAllText(_filePath))) Console.WriteLine("\nИстория игр пуста.");

        else
        {
            Console.WriteLine("История ваших игр: ");
            string content = File.ReadAllText(_filePath);
            Console.WriteLine(content);
        }
    }
}
