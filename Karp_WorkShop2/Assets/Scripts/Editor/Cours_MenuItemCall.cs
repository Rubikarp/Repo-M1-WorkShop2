using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

public class Cours_MenuItemCall
{
    //  %+lettre = bind ctrl + lettre
    //  #+lettre = bind shift + lettre
    //  &+lettre = bind alt + lettre
    //  _+lettre = bind lettre pur !!!curesed de fou!!!

    [MenuItem("CoursTool/HelloWorld #h")]
    public static void CallHelloWorld()
    {
        Debug.Log("Hello World");
    }

    /*----------------------------------------------------------------------------------*/

    [MenuItem("CoursTool/PingSelectObj")]
    public static void CallSelectObj()
    {
        //Object Unique
        //Debug.Log(Selection.activeObject.name);

        //Object multiple
        //Debug.Log(Selection.objects.Length);

        //Ping
        Object go = GameObject.Find("Main Camera");
        EditorGUIUtility.PingObject(go);
        Selection.activeObject = go;
    }

    [MenuItem("CoursTool/LightColorRed")]
    public static void LightColorRed()
    {
        //Ping
        Light light = GameObject.FindObjectOfType<Light>();
        if (light == null)
        {
            Debug.LogWarning("No light have been find");

            GameObject newGo = new GameObject("LightRed");
            Undo.RegisterCreatedObjectUndo(newGo, "Red Light creation");

            light = Undo.AddComponent<Light>(newGo);
            //light = newGo.AddComponent<Light>();

            newGo.transform.position = Vector3.zero;
            EditorUtility.SetDirty(newGo);
        }


        EditorGUIUtility.PingObject(light);
        Selection.activeObject = light;

        //Save avant action
        Undo.RecordObject(light.gameObject, "light to red");
        light.color = Color.red;

        #region Prévenir l'éditeur d'un changement
        //prevenir d'un changement !!!!!!!!Ne pas oublier!!!!!!!
        EditorUtility.SetDirty(light);
        //Alternative
        EditorSceneManager.MarkSceneDirty(light.gameObject.scene);
        //EditorSceneManager.MarkAllScenesDirty();
        #endregion
    }
    
    public static void UndoMethods()
    {
        GameObject myGameObject = new GameObject("myGameObject");
        Transform newTransformParent  = myGameObject.transform;

        ///Modifying a single property:
        Undo.RecordObject(myGameObject.transform, "Zero Transform Position");
        myGameObject.transform.position = Vector3.zero;

        ///Adding a component:
        Undo.AddComponent<Rigidbody>(myGameObject);

        ///Creating a new game object:
        var go = new GameObject();
        Undo.RegisterCreatedObjectUndo(go, "Created go");

        ///Destroying a game object or component:
        Undo.DestroyObjectImmediate(myGameObject);

        ///Changing transform parenting:
        Undo.SetTransformParent(myGameObject.transform, newTransformParent, "Set new parent");

        //Undo et redo are the same operation, it's a state change
        //https://docs.unity3d.com/ScriptReference/Undo.GetCurrentGroup.html
        //https://docs.unity3d.com/ScriptReference/Undo.IncrementCurrentGroup.html
    }
    
    public static void Other()
    {
        //in play Mode ?
        bool test = EditorApplication.isPlaying;
        //PlayModeChange CallBack
        // https://docs.unity3d.com/ScriptReference/PlayModeStateChange.html

    }

}
