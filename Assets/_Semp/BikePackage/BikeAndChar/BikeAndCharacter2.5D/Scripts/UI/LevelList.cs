using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelList : MonoBehaviour
{
    public Transform levelContainer;
    public GameObject levelItem1;
    public GameObject levelItem2;

    private void OnEnable()
    {
        LoadLevelData();
    }
    public void LoadLevelData()
    {
        foreach (Transform child in levelContainer)
        {
            Destroy(child.gameObject);
        }
        List<LevelModel> levels = DataManager.Instance.levels.listLevel;
        GameObject levelObject;
        foreach(LevelModel level in levels)
        {
            if(level.Id % 2 == 0)
            {
                levelObject = Instantiate(levelItem2, levelContainer);
            }
            else
            {
                levelObject = Instantiate(levelItem1, levelContainer);
            }
            LevelItem levelItem = levelObject.GetComponent<LevelItem>();
            if(level.Id == 1)
            {
                levelItem.line.SetActive(false);
            }
            levelItem.levelNumber.text = level.Id.ToString("00");
            levelItem.levelId = level.Id;
        }
    }
}
