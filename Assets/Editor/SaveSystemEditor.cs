// SaveSystemEditor.cs
// Owned by Garabatos Inc.
// Created by: Dohyun Kim (301058465)

using UnityEditor;
using UnityEngine;

/// <summary>
///   <para>Custom script for drawing the inspector for SaveSystem component</para>
///   <para>This is used for utility features like showing the saved text on the fly.</para>
/// </summary>
[CustomEditor(typeof(SaveSystem))]
public class SaveSystemEditor : Editor
{
    private string saveText = "";
    private string errorText = "";

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        EditorGUILayout.Space(10.0f);

        using (new EditorGUILayout.HorizontalScope())
        {
            if (GUILayout.Button("Show Save Text")) ShowSaveText();
            if (GUILayout.Button("Clear Save Text")) saveText = "";
        }

        EditorGUILayout.Space();

        if (saveText != "")
        {
            EditorGUILayout.LabelField("Save Text", EditorStyles.boldLabel);
            EditorGUILayout.HelpBox(saveText, MessageType.Info);
        }

        if (errorText != "")
        {
            EditorGUILayout.HelpBox(errorText, MessageType.Error);
        }
    }

    private void ShowSaveText()
    {
        saveText = "";
        errorText = "";

        SaveSystem saveSystem = target as SaveSystem;

        if (!saveSystem)
        {
            errorText = "Failed to convert the object to SaveSystem";
            return;
        }

        saveText = saveSystem.GetSaveString();
    }
}
