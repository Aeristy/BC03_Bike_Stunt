using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;
    public BikeData bikes;
    public LevelData levels;

    private void Awake()
    {
        Instance = this;
    }
}
