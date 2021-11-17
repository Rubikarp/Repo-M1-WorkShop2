using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class Localizer : MonoBehaviour
{
    public TextMeshProUGUI selfText;
    public string localisationKey;

    public UnityEvent onRefreshText;

    private void Start()
    {
        LocalisationManager.instance.OnLanguageChange += RefreshText;
    }

    public void RefreshText()
    {
        selfText.text = LocalisationManager.instance.FetchText(localisationKey);

        onRefreshText?.Invoke();
    }
}
