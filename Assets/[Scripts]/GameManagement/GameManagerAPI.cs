// GameManagerAPI.cs
// Owned by Garabatos Inc.
// Created by: Dohyun Kim (301058465)

using UnityEngine;

/// <summary>
///   <para>A simple frontend API component for GameManager, for event handling in the editor inspector</para>
/// </summary>
[DisallowMultipleComponent]
public class GameManagerAPI : MonoBehaviour
{
    /// <summary>
    ///   <para>Pause the game</para>
    /// </summary>
    public void PauseGame()
    {
        GameManager.Instance.PauseGame();
    }

    /// <summary>
    ///   <para>Resume the paused game</para>
    /// </summary>
    public void ResumeGame()
    {
        GameManager.Instance.ResumeGame();
    }

    /// <summary>
    ///   <para>Load the main menu</para>
    /// </summary>
    public void BackToMainMenu()
    {
        GameManager.Instance.BackToMainMenu();
    }

    /// <summary>
    ///   <para>Set the game as over</para>
    /// </summary>
    public void SetGameOver()
    {
        GameManager.Instance.SetGameOver();
    }

    /// <summary>
    ///   <para>Load the level with the given index</para>
    /// </summary>
    /// <param name="levelIndex">The index of the level to load from `levelScenes` array in GameManager</param>
    public void LoadLevel(int levelIndex)
    {
        GameManager.Instance.LoadLevel(levelIndex);
    }

    /// <summary>
    ///   <para>Exit the game application</para>
    /// </summary>
    public void ExitGame()
    {
        GameManager.Instance.ExitGame();
    }

    public void SetFinishedGame()
    {
        GameManager.Instance.SetFinishedGame();
    }
}
