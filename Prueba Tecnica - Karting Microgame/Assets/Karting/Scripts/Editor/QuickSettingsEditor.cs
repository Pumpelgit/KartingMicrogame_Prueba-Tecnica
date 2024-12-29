using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using Codice.Client.Common.GameUI;
using KartGame.KartSystems;

[CreateAssetMenu(fileName = "Quick Settings", menuName = "Karting/Quick Settings Window")]
public class QuickSettingsEditor : EditorWindow
{
    QuickSettingsSO quickSettingsSO;
    SerializedObject serializedObjectData;

    [MenuItem("Karting/Quick Settings Window", false, 1)]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow<QuickSettingsEditor>();
    }

    private void OnGUI()
    {


        EditorGUILayout.Space(10);
        GUIStyle labelStyle = new GUIStyle(EditorStyles.label);
        labelStyle.fontStyle = FontStyle.Bold;
        labelStyle.wordWrap = true;
        labelStyle.fontSize = 14;
        labelStyle.richText = true;

        EditorGUILayout.LabelField("Quick Settings", labelStyle);
        EditorGUILayout.Space(20);

        var newQuickSettingsSO = (QuickSettingsSO)EditorGUILayout.ObjectField("Quick Settings Data", quickSettingsSO, typeof(QuickSettingsSO), false);

        if (newQuickSettingsSO != quickSettingsSO)
        {
            quickSettingsSO = newQuickSettingsSO;
            if (quickSettingsSO != null)
            {
                serializedObjectData = new SerializedObject(quickSettingsSO);
            }
            else
            {
                serializedObjectData = null;
            }
        }

        if (serializedObjectData != null)
        {
            serializedObjectData.Update();

            SerializedProperty property = serializedObjectData.GetIterator();
            property.NextVisible(true);

            while (property.NextVisible(false))
            {
                EditorGUILayout.PropertyField(property, true);
            }

            if (GUI.changed)
            {
                serializedObjectData.ApplyModifiedProperties();
            }

            if (GUILayout.Button("Save Changes"))
            {
                serializedObjectData.ApplyModifiedProperties();
                EditorUtility.SetDirty(quickSettingsSO);
                AssetDatabase.SaveAssets();
                GameSettingsDataSO myScriptableObject = AssetDatabase.LoadAssetAtPath<GameSettingsDataSO>("Assets/Karting/ScriptableObjects/GameSettingsDataSO.asset");
                myScriptableObject.GameSettings = quickSettingsSO;
                Debug.Log("Changes saved!");
            }
            if (GUILayout.Button("Create New Settings"))
            {
                QuickSettingsSO newSettingsSO = ScriptableObject.CreateInstance<QuickSettingsSO>();
                string assetPath = AssetDatabase.GenerateUniqueAssetPath("Assets/Karting/ScriptableObjects/NewQuickSettings.asset");
                AssetDatabase.CreateAsset(newSettingsSO, assetPath);
                quickSettingsSO = newSettingsSO;
                serializedObjectData = new SerializedObject(quickSettingsSO);
                AssetDatabase.SaveAssets();
                Debug.Log("New Settings Created! " + assetPath);
            }
        }
        else
        {
            GUILayout.Label("No ScriptableObject selected.", EditorStyles.helpBox);
        }

        EditorGUILayout.Space(20);

        if (GUILayout.Button("Abort"))
        {
            this.Close();
        }
    }
}
