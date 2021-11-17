using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CsvUtility 
{
    public static List<string[]> ParseCSV(TextAsset csvFile)
    {
        List<string[]> result = new List<string[]>();
        string rawContent = csvFile.text;

        string[] separator = new string[] { "\n" };
        string[] lines = rawContent.Split(separator, StringSplitOptions.None);

        string[] cellSeparator = new string[] { "\t" };
        //Skip first line
        for (int i = 1; i < lines.Length; i++)
        {
            string[] cells = lines[i].Split(cellSeparator, StringSplitOptions.None);
            result.Add(cells);
        }

        return result;
    }

}
