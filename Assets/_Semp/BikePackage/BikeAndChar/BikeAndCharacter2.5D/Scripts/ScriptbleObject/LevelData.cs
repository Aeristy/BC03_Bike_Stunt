using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelDataAsset", menuName = "DataAsset/LevelDataAsset")]
public class LevelData : ScriptableObject
{
    public List<LevelModel> listLevel;
}


[Serializable]
public class LevelModel
{
    public int Id;
    public string LevelName;
    public GameObject LevelSource;
    public Sprite LevelThumb;
    public string LevelDes;


}