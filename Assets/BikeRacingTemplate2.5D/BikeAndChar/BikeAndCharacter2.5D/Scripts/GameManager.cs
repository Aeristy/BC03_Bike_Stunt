using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;


    private void Awake()
    {
        Instance = this;
        SceneManager.LoadScene("gara", LoadSceneMode.Additive);
    }
    public void LoadTestScene()
    {
        
        SceneManager.UnloadSceneAsync("gara");
        SceneManager.LoadScene("Test Scene", LoadSceneMode.Additive);
    }
}
