// SaveSystem.cs
// Owned by Garabatos Inc.
// Created by: Zhengliang Ding (301222388)

using UnityEngine;
using System.IO;
using System.Linq;

[DisallowMultipleComponent]
public class SaveSystem : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Game objects that need save/load functionalities")]
    private GameObject[] saveLoadFor;

    /// <summary>
    ///   <para>Save the current game state to a save file</para>
    /// </summary>
    public void SaveGame()
    {
        // Path to the save file
        string path = $"{Application.persistentDataPath}/player.txt";

        // `using` is used to invoke the Dispose method from IDisposable interface on the StreamWriter
        // StreamWriter: https://docs.microsoft.com/en-us/dotnet/api/system.io.streamwriter?view=netstandard-2.1#remarks
        // IDisposable: https://docs.microsoft.com/en-us/dotnet/api/system.idisposable?view=netstandard-2.1
        using (StreamWriter writer = new(path, false))
        {
            SaveGameToWriter(writer);
        }

        Debug.Log($"Write completed to save file {path}");
    }

#if UNITY_EDITOR // only used for the custom drawer
    /// <summary>
    ///   <para>Save the current game state to a string</para>
    /// </summary>
    /// <returns>String with the saved data</returns>
    public string GetSaveString()
    {
        // StringWriter, although it implements IDisposable, does not need a call to Dispose
        // StringWriter: https://docs.microsoft.com/en-us/dotnet/api/system.io.stringwriter?view=netstandard-2.1#remarks
        StringWriter writer = new StringWriter();
        SaveGameToWriter(writer);
        return writer.ToString();
    }
#endif

    private void SaveGameToWriter(TextWriter writer)
    {
        // Save current level
        writer.WriteLine(Application.isPlaying ? $"Level {GameManager.Instance.CurrentLevelNumber}" : "Level --");

        // Save state from the level scene
        foreach (ISaveLoad component in saveLoadFor.SelectMany(obj => obj.GetComponents<ISaveLoad>()))
        {
            component.Save(writer);
        }
    }
}
