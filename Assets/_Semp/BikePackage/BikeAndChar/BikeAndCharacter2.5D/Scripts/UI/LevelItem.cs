using UnityEngine;
using UnityEngine.UI;

public class LevelItem : MonoBehaviour
{
    public GameObject line;
    public Image fillImage;
    public Text levelNumber;
    public Button levelButton;
    public Image levelState;
    public Image[] star;
    public int levelId;

    public void OnLevelSelect()
    {
        UIManager.Instance.SelectLevel.OnSelectLevel(levelId);
    }
}

