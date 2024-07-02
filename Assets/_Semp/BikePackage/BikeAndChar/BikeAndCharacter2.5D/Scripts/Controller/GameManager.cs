using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public PlayerModel _player;

    private void Awake()
    {
        Instance = this;
        SceneManager.LoadScene("gara", LoadSceneMode.Additive);
        
        _player = Utility.LoadGameData();
        
    }
    public void LoadTestScene()
    {
        
        SceneManager.UnloadSceneAsync("gara");
        SceneManager.LoadScene("Test Scene", LoadSceneMode.Additive);
    }
}
