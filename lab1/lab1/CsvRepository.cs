using System;
using System.Collections.Generic;
using System.IO;

namespace lab1;

/// <summary>
/// Репозиторий, читающий данные из CSV-файлов
/// </summary>
public class CsvRepository
{
    /// <summary>
    /// Путь к папке, в которой находятся CSV-файлы с данными.
    /// </summary>
    private string _basePath;

    /// <summary>
    /// Конструктор, который принимает путь к папке с CSV-файлами
    /// </summary>
    public CsvRepository(string basePath)
    {
        _basePath = basePath;
    }

    /// <summary>
    /// Читает список направлений из файла directions.csv.
    /// Первая строка файла считается заголовком и пропускается.
    /// </summary>
    /// <returns>Список объектов Direction. Если файл пуст или содержит только заголовок — пустой список.</returns>
    public List<Direction> GetDirections()
    {
        List<Direction> result = new List<Direction>();
        string[] lines = File.ReadAllLines(Path.Combine(_basePath, "directions.csv"));

        if (lines.Length < 2) return result;

        for (int i = 1; i < lines.Length; i++)
        {
            string[] parts = lines[i].Split(",");

            if (parts.Length < 3) continue;

            Direction d = new Direction();

            d.Id = int.Parse(parts[0]);
            d.Name = parts[1];
            d.Distance = int.Parse(parts[2]);

            if (string.IsNullOrEmpty(d.Name))
                throw new ArgumentException($"Строка {i}: название направления пустое");

            if (d.Id <= 0)
                throw new ArgumentException($"Строка {i}: Id должен быть больше нуля");

            if (d.Distance <= 0)
                throw new ArgumentException($"Строка {i}: расстояние должно быть больше нуля");

            result.Add(d);
        }

        return result;
    }


    /// <summary>
    /// Читает список машинистов из файла drivers.csv.
    /// Первая строка файла считается заголовком и пропускается.
    /// </summary>
    /// <returns>Список объектов Driver. Если файл пуст или содержит только заголовок — пустой список.</returns>
    public List<Driver> GetDrivers()
    {
        List<Driver> result = new List<Driver>();
        string[] lines = File.ReadAllLines(Path.Combine(_basePath, "drivers.csv"));

        if (lines.Length < 2) return result;

        for (int i = 1; i < lines.Length; i++)
        {

            string[] parts = lines[i].Split(",");
            if (parts.Length < 4) continue;

            Driver d = new Driver();
            d.Id = int.Parse(parts[0]);
            d.FullName = parts[1];
            d.Experience = int.Parse(parts[2]);
            d.License = parts[3];

            result.Add(d);
        }

        return result;
    }

    /// <summary>
    /// Читает список поездов из файла trains.csv.
    /// Первая строка файла считается заголовком и пропускается.
    /// </summary>
    /// <returns>Список объектов Train. Если файл пуст или содержит только заголовок — пустой список.</returns>
    public List<Train> GetTrains()
    {

        List<Train> result = new List<Train>();
        string[] lines = File.ReadAllLines(Path.Combine(_basePath, "trains.csv"));

        if (lines.Length < 2) return result;

        for (int i = 1; i < lines.Length; i++)
        {
            string[] parts = lines[i].Split(',');
            if (parts.Length < 6) continue;

            Train t = new Train();
            t.Id = int.Parse(parts[0]);
            t.Number = parts[1];
            t.DirectionId = int.Parse(parts[2]);
            t.DriverId = int.Parse(parts[3]);
            t.Capacity = int.Parse(parts[4]);
            t.Type = parts[5];

            result.Add(t);
        }

        return result;
    }
}
