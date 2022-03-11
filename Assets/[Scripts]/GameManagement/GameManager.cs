// GameManager.cs
// Owned by Garabatos Inc.
// Created by: Dohyun Kim (301058465)

using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

[DisallowMultipleComponent]
public class GameManager : MonoBehaviour
{
    /// <summary>
    ///   <para>Singleton instance for easy referencing</para>
    ///   <para>It is very important that there is only one instance of this in the whole scene.</para>
    /// </summary>
    public static GameManager Instance { get; private set; }

    /// <summary>
    ///   <para>The fallback camera to use when there are no cameras due to scene switching</para>
    /// </summary>
    [Header("Gameplay")]
    [SerializeField]
    private Camera fallbackCamera;

    /// <summary>
    ///   <para>Whether the game is currently paused</para>
    /// </summary>
    public bool IsGamePaused => Time.timeScale == 0.0f;

    /// <summary>
    ///   <para>Whether there's a gameplay session going on</para>
    /// </summary>
    private bool isGamePlaying;

    /// <summary>
    ///   <para>Property for `isGamePlaying` that automatically fires the `GamePlaying` event</para>
    /// </summary>
    public bool IsGamePlaying
    {
        get => isGamePlaying;
        private set
        {
            bool temp = isGamePlaying;
            isGamePlaying = value;
            if (temp != value) onGamePlaying.Invoke(value);
        }
    }

    /// <summary>
    ///   <para>The game overlay canvas game object</para>
    /// </summary>
    [Header("UI")]
    [SerializeField]
    private GameObject gameOverlay;

    [SerializeField]
    private CanvasGroup hurtPanel;

    /// <summary>
    ///   <para>The pause menu canvas game object</para>
    /// </summary>
    [SerializeField]
    private GameObject pauseMenu;

    /// <summary>
    ///   <para>The name of the main menu scene</para>
    /// </summary>
    [Header("Scenes")]
    [SerializeField]
    private string mainMenuScene;


    /// <summary>
    ///   <para>The name of the game over scene</para>
    /// </summary>
    [SerializeField]
    private string gameOverScene;

    /// <summary>
    ///   <para>The name of the final scene (when the player wins the game)</para>
    /// </summary>
    [SerializeField]
    private string finalScene;

    /// <summary>
    ///   <para>The names of the level scenes, in the order of the levels</para>
    /// </summary>
    [SerializeField]
    private string[] levelScenes;

    /// <summary>
    ///   <para>Event handlers for when the game is paused or resumed; true when paused, false when resumed</para>
    /// </summary>
    [Header("Events")]
    public UnityEvent<bool> onGamePaused;

    /// <summary>
    ///   <para>
    ///     Event handlers for when the game starts or resumes to play (true) or gameplay pauses or stops (false)
    ///   </para>
    /// </summary>
    public UnityEvent<bool> onGamePlaying;

    /// <summary>
    ///   <para>Event handlers for when menu scene load is finished</para>
    ///   <para>
    ///     This is not going to be likely to be used in the editor inspector. Instead, other components will
    ///     dynamically register and unregister delegates.
    ///   </para>
    /// </summary>
    public UnityEvent<string> onMenuLoadFinished;

    /// <summary>
    ///   <para>Event handlers for when level load is finished</para>
    ///   <para>
    ///     This is not going to be likely to be used in the editor inspector. Instead, other components will
    ///     dynamically register and unregister delegates.
    ///   </para>
    /// </summary>
    public UnityEvent<int> onLevelLoadFinished;

    /// <summary>
    ///   <para>Scene cache for the persistent scene</para>
    /// </summary>
    private Scene persistent;

    /// <summary>
    ///   <para>Currently selected level by the index in the `levelScenes` array; -1 if none</para>
    /// </summary>
    private int currentLevel = -1;

    /// <summary>
    ///   <para>Current level by number (index + 1)</para>
    ///   <para>If 0, there is no level running.</para>
    /// </summary>
    public int CurrentLevelNumber => currentLevel + 1;

    /// <summary>
    ///   <para>Utility property to check if there's a level selected</para>
    /// </summary>
    private bool IsOnALevel => currentLevel > -1;

    /// <summary>
    ///   <para>Pause the game</para>
    /// </summary>
    /// <param name="pause">Whether to pause or not; defaults to true</param>
    /// <param name="notify">Whether the `GamePaused` event should fire; defaults to true</param>
    public void PauseGame(bool pause = true, bool notify = true)
    {
        if (pause == IsGamePaused) return; // no need to apply the same value
        if (pause && !IsGamePlaying) return; // cannot pause when not playing the game

        if (notify) IsGamePlaying = !pause;
        else isGamePlaying = !pause;

        Time.timeScale = pause ? 0.0f : 1.0f;

        if (notify) onGamePaused.Invoke(pause);
    }

    /// <summary>
    ///   <para>An alias for `PauseGame(false)`</para>
    /// </summary>
    public void ResumeGame()
    {
        PauseGame(false);
    }

    /// <summary>
    ///   <para>Go back to the main menu</para>
    /// </summary>
    public void BackToMainMenu()
    {
        LoadMenu(mainMenuScene);
        UnloadMenu(gameOverScene);
        UnloadCurrentLevel();
        UnloadMenu(finalScene);
    }

    /// <summary>
    ///   <para>Set the game state as game over</para>
    /// </summary>
    public void SetGameOver()
    {
        LoadMenu(gameOverScene);
        UnloadCurrentLevel();
    }


    public void SetFinishedGame()
    {
        LoadMenu(finalScene);
        UnloadCurrentLevel();
    }


    /// <summary>
    ///   <para>Load the level with the given index</para>
    /// </summary>
    /// <param name="levelIndex">The index of the level in the `levelScenes` array</param>
    public void LoadLevel(int levelIndex)
    {
        CancelPauseAndGamePlaying();
        UnloadMenu(mainMenuScene);
        UnloadMenu(gameOverScene);
        UnloadMenu(finalScene);
        UnloadCurrentLevel();
        string _ = levelScenes[levelIndex]; // checking the index before requesting load
        fallbackCamera.enabled = true;
        currentLevel = levelIndex;
        StartCoroutine(LoadLevelCoroutine(levelIndex));
    }

    /// <summary>
    ///   <para>Closes the game application</para>
    /// </summary>
    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    /// <summary>
    ///   <para>Handle special case where we have to cancel the pause but not going into gameplay mode</para>
    /// </summary>
    private void CancelPauseAndGamePlaying()
    {
        bool previousPaused = IsGamePaused;
        PauseGame(false, false);
        IsGamePlaying = false;
        if (previousPaused) onGamePaused.Invoke(false);
    }

    /// <summary>
    ///   <para>Load the menu scene if it is not already loaded</para>
    /// </summary>
    /// <param name="menuScene">The name of the menu scene to load</param>
    /// <param name="bypassCheck">Whether to ignore the check; defaults to false</param>
    private void LoadMenu(string menuScene, bool bypassCheck = false)
    {
        if (!bypassCheck)
        {
            Scene scene = SceneManager.GetSceneByName(menuScene);
            if (scene.IsValid()) return;
        }

        CancelPauseAndGamePlaying();
        fallbackCamera.enabled = true;
        StartCoroutine(LoadMenuCoroutine(menuScene));
    }

    /// <summary>
    ///   <para>Unload the menu scene with the given name if it is already loaded</para>
    /// </summary>
    /// <param name="menuScene">The name of the menu scene to unload</param>
    private void UnloadMenu(string menuScene)
    {
        Scene scene = SceneManager.GetSceneByName(menuScene);
        if (scene.IsValid() && scene.isLoaded)
        {
            SceneManager.UnloadSceneAsync(menuScene);
        }
    }

    /// <summary>
    ///   <para>Unload the currently loaded level if there is one</para>
    /// </summary>
    private void UnloadCurrentLevel()
    {
        if (!IsOnALevel) return;

        IsGamePlaying = false;
        SceneManager.SetActiveScene(persistent); // set the Persistent scene active before unloading
        SceneManager.UnloadSceneAsync(levelScenes[currentLevel]);
        currentLevel = -1;
    }

    /// <summary>
    ///   <para>
    ///     Coroutine to poll the async task of loading the menu and invoke MenuLoadFinished event when finished
    ///   </para>
    /// </summary>
    /// <param name="menuScene">The name of the menu scene</param>
    /// <returns>IEnumerator</returns>
    private IEnumerator LoadMenuCoroutine(string menuScene)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(menuScene, LoadSceneMode.Additive);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        fallbackCamera.enabled = false;
        onMenuLoadFinished.Invoke(menuScene);
    }

    /// <summary>
    ///   <para>
    ///     Coroutine to poll the async task of loading the level and invoke LevelLoadFinished event when finished
    ///   </para>
    /// </summary>
    /// <param name="levelIndex">The index of the level in the `levelScenes` array</param>
    /// <returns>IEnumerator</returns>
    private IEnumerator LoadLevelCoroutine(int levelIndex)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(levelScenes[levelIndex], LoadSceneMode.Additive);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        fallbackCamera.enabled = false;
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(levelScenes[levelIndex]));
        IsGamePlaying = !IsGamePaused;
        onLevelLoadFinished.Invoke(levelIndex);
    }

    /// <summary>
    ///   <para>Check for a already loaded scenes for the editor build</para>
    /// </summary>
    private void StartupSceneSetup()
    {
#if UNITY_EDITOR // the check for additional loaded levels is only required in the editor
        // load main menu if the persistent scene is the only scene loaded
        if (SceneManager.sceneCount == 1)
        {
#endif
            LoadMenu(mainMenuScene, true);
#if UNITY_EDITOR
            return;
        }

        fallbackCamera.enabled = false;

        // check if there is a level scene open
        // this assumes that only 1 of the level scenes is open and gets the lowest level
        for (int i = 0; i < levelScenes.Length; i++)
        {
            Scene levelScene = SceneManager.GetSceneByName(levelScenes[i]);
            if (levelScene.isLoaded)
            {
                SceneManager.SetActiveScene(levelScene);
                currentLevel = i;
                IsGamePlaying = true;
                break;
            }
        }
#endif
    }

    void Awake()
    {
        // check if there's already an instance, and destroy self if there is
        if (Instance)
        {
            Debug.LogError($"Only one GameManager should exist in the scene - game object `{name}`");
            Destroy(this);
            return;
        }

        // set the singleton instance to use
        Instance = this;

        // cache the persistent scene
        persistent = SceneManager.GetSceneByBuildIndex(0);
    }

    void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    void Start()
    {
        gameOverlay.SetActive(false); // this should not be controlled by `DisableOnStart` component
        pauseMenu.SetActive(false);
        StartupSceneSetup(); // this needs to be in `Start` because it might send events
    }

    private void Update()
    {
        if (hurtPanel.alpha > 0)
        {
            hurtPanel.alpha -= Time.deltaTime;
        }
    }

    public void PlayerHurt()
    {
        hurtPanel.alpha = 1;
    }
}
