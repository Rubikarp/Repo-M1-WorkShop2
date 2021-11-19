using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SCOparameterSettingsProfider
{
    public const string settingsDataPath = "Assets/Cours/Scripts/SettingsUnity/Parameter.asset";
    private static SCOparameter data;

    [SettingsProvider]
    public static SettingsProvider CreateParameterSettingsProvider()
    {

        data = GetOrCreateSettings();

        // (path in Settings window, scope of setting [User or Project])
        var provider = new SettingsProvider("_Tool/test", SettingsScope.Project)
        {
            // Create the SettingsProvider and initialize its drawing (IMGUI) function in place:
            guiHandler = (searchContext) =>
            {
                //Draw GUI
                using (new GUILayout.VerticalScope(EditorStyles.helpBox))
                {
                    GUILayout.Label("Parametrage", EditorStyles.whiteLargeLabel);
                    EditorGUILayout.Space(EditorGUIUtility.singleLineHeight / 3);

                    //Path
                    using (new EditorGUILayout.HorizontalScope())
                    {
                        //Nom
                        GUILayout.Label("Choose your Path", EditorStyles.boldLabel, GUILayout.Width(160));
                        //Champs
                        data.path = EditorGUILayout.TextField(data.path, EditorStyles.textArea);
                    }
                    //Prefab
                    using (new GUILayout.HorizontalScope())
                    {
                        //Nom
                        GUILayout.Label("Valeurs", EditorStyles.boldLabel, GUILayout.Width(160));
                        //Champs
                        data.height = EditorGUILayout.IntField(data.height, EditorStyles.textArea);
                        data.width = EditorGUILayout.IntField(data.width, EditorStyles.textArea);
                    }
                }
            },

            // Search keywords for people using the search bar (how are you ?)
            keywords = new HashSet<string>(new[] { "Thales", "Integration", "SCO" })
        };
        
        return provider;

    }
    public static SCOparameter GetOrCreateSettings()
    {
        var settings = AssetDatabase.LoadAssetAtPath<SCOparameter>(settingsDataPath);
        if (settings == null)
        {
            settings = ScriptableObject.CreateInstance<SCOparameter>();

            string path = "Assets/";
            int width = 5;
            int height = 5;

            AssetDatabase.CreateAsset(settings, settingsDataPath);
            AssetDatabase.SaveAssets();
        }
        return settings;
    }
    public static SerializedObject GetSerializedSettings()
    {
        return new SerializedObject(GetOrCreateSettings());
    }
}
