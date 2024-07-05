using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public PlayerModel _player;
    public string currentSceneName;

    private void Awake()
    {
        Instance = this;       
        _player = Utility.LoadGameData();
        
    }
    private void Start()
    {
        UIManager.Instance.LoadingLevel.Show();
        currentSceneName = "gara";
        SceneManager.LoadScene("gara", LoadSceneMode.Additive);
    }
    public void LoadTestScene()
    {
        currentSceneName = "Test Scene";
        UIManager.Instance.LoadingLevel.Show();
        SceneManager.UnloadSceneAsync("gara");
        SceneManager.LoadScene("Test Scene", LoadSceneMode.Additive);
    }
    public void BackToGarage()
    {
        currentSceneName = "gara";
        UIManager.Instance.LoadingLevel.Show();
        SceneManager.UnloadSceneAsync("Test Scene");
        SceneManager.LoadScene("gara", LoadSceneMode.Additive);
    }
    public bool SceneLoaded()
    {
        Scene scene = SceneManager.GetSceneByName(currentSceneName);
        if (scene != null)
            return true;
        else return false;
    }
}
