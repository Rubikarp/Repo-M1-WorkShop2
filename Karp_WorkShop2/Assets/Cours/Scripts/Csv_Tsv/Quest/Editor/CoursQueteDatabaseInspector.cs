using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CoursQuestDatabase))]
public class CoursQueteDatabaseInspector : Editor
{
    CoursQuestDatabase questBase;

    // Start like
    private void OnEnable()
    {
        questBase = target as CoursQuestDatabase;
    }

    // Update like
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        serializedObject.Update();


        if (questBase.csvFile == null) return;

        if (GUILayout.Button("Parse CSV"))
        {
            List<string[]> csvContent = ParseCSV();
            questBase.allQuest = GenerateQuests(csvContent);
            EditorUtility.SetDirty(questBase);
        }

        //target recois serializedObject values (comprend le set dirty et le 
        serializedObject.ApplyModifiedProperties();
    }
    List<string[]> ParseCSV()
    {
        return CsvUtility.ParseCSV(questBase.csvFile); ;
    }

    private SingleQuest[] GenerateQuests(List<string[]> csvContent)
    {
        if (csvContent == null) return null;

        SingleQuest[] result = new SingleQuest[csvContent.Count];
        if (csvContent.Count == 0) return result;

        for (int i = 0; i < csvContent.Count; i++)
        {
            result[i] = GenerateQuest(csvContent[i]);
        }

        return result;
    }

    private SingleQuest GenerateQuest(string[] csvLine)
    {
        SingleQuest result = new SingleQuest();

        result.name = csvLine[1];
        result.questLine = csvLine[2];

        int.TryParse(csvLine[3], out result.challengeValue);
        int.TryParse(csvLine[4], out result.rewardValue);

        if (csvLine[5] == "Score") result.rewardType = RewardType.Score;
        if (csvLine[5] == "Pièces") result.rewardType = RewardType.Coins;
        if (csvLine[5] == "Exp") result.rewardType = RewardType.Exp;

        //Valeur Dynamique
        result.questLine = result.questLine.Replace("$amout", result.challengeValue.ToString());

        return result;
    }

}
