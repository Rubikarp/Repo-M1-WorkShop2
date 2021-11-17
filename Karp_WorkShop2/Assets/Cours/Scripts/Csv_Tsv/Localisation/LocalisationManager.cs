using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum Language
{
    English,
    French
}

public class LocalisationManager : MonoBehaviour
{
    public static LocalisationManager instance;

    public Dictionary<string, string> mainDico;
    public Language currentLanguage;

    public event Action OnLanguageChange;
    public TextAsset csvFile;

    private void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;

        mainDico = new Dictionary<string, string>();

        ///Parse CSV
        List<string[]> csvContent =  CsvUtility.ParseCSV(csvFile);
        //attrib les clès
        for (int i = 0; i < csvContent.Count; i++)
        {
            string[] curline = csvContent[i];
            for (int j = 0; j < curline.Length; j++)
            {
                mainDico.Add(curline[0] + "_" + (j - 1).ToString(), curline[j]);
            }
        }
    }

    private IEnumerator Start()
    {
        yield return new WaitForEndOfFrame();
        ChangeLanguage(currentLanguage);
    }

    public void ChangeLanguage(Language newLanguage)
    {
        currentLanguage = newLanguage;
        
        if(OnLanguageChange != null)
        {
            OnLanguageChange?.Invoke();
        }
    }

    public string FetchText(string locKey)
    {
        return mainDico[locKey + "_" + ((int)currentLanguage).ToString()];
    }
}
