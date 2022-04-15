using System;
using System.IO;
using UnityEngine;

public static class SaveManager
{
    static string dataPath = Application.persistentDataPath + "/results.save";

    public static string GetResults()
    {
        CheckDataFile();
        string result = File.ReadAllText(dataPath);
        if (String.IsNullOrWhiteSpace(result)) result = "Результатов нет";
        return result;
    }

    public static void AddResult(TimeSpan result)
    {
        CheckDataFile();

        string newResult = $"{result.Minutes} мин. ";
        newResult += $"{result.Seconds} сек. ";
        newResult += $"{result.Milliseconds} мс. ";

        string oldResult = File.ReadAllText(dataPath);
        File.WriteAllText(dataPath, newResult + "\n" + oldResult);
    }

    static void CheckDataFile()
    {
        if (!File.Exists(dataPath))
            File.CreateText(dataPath).Dispose();
    }
}
