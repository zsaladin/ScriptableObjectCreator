using System;
using System.IO;
using UnityEngine;
using UnityEditor;

public class ScriptableObjectCreator
{
	private static string WARNING_TITLE = "Warning";
	private const string WARNING_MESSAGE = "Select ScriptableObject script";
	private const string WARNING_OK = "OK";

	[MenuItem("Assets/Create/Scriptable Object")]
    public static void Init()
    {
        MonoScript monoScript = Selection.activeObject as MonoScript;
		if (monoScript == null)
		{
			EditorUtility.DisplayDialog(WARNING_TITLE, WARNING_MESSAGE, WARNING_OK);
			return;
		}

		Type type = monoScript.GetClass();
		if (type.IsSubclassOf(typeof(ScriptableObject)) == false)
		{
			EditorUtility.DisplayDialog(WARNING_TITLE, WARNING_MESSAGE, WARNING_OK);
			return;
		}

		ScriptableObject scriptableObjectInstance = ScriptableObject.CreateInstance(type);
		AssetDatabase.CreateAsset(scriptableObjectInstance, Path.ChangeExtension(AssetDatabase.GetAssetPath(monoScript), "asset"));
		AssetDatabase.SaveAssets();

		EditorUtility.FocusProjectWindow();
		Selection.activeObject = scriptableObjectInstance;
    }
}