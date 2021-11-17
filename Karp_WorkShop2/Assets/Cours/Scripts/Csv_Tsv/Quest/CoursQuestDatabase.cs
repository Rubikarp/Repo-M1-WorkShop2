using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct SingleQuest
{
    public string name;
    public string questLine;
    public int challengeValue;
    public int rewardValue;
    public RewardType rewardType;
}

public enum RewardType
{
    Coins,
    Score,
    Exp
}

[CreateAssetMenu(fileName = "QuestDatabase_new", menuName = "ScriptableObjects/QuestDatabase")]
public class CoursQuestDatabase : ScriptableObject
{
    public SingleQuest[] allQuest;

#if UNITY_EDITOR
    public TextAsset csvFile;

#endif
}
