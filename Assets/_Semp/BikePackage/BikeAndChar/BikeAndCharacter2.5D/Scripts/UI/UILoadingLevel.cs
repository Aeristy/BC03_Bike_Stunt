using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILoadingLevel : PopupBase
{
    public Image Fill;
    public Text Percentage;
    public float percentage;

    private GameManager gameManager;
    protected override void OnEnable()
    {
        base.OnEnable();
        Fill.fillAmount = 0;
        percentage = 0;
        gameManager = GameManager.Instance;
    }
    private void Update()
    {
        if (!gameManager.SceneLoaded())
        {
            if (percentage < 30)
            {
                percentage += Time.deltaTime * 4;
            }
        }
        else
        {
            percentage += Time.deltaTime * 60;
        }
        

        Percentage.text = Mathf.FloorToInt(percentage).ToString() + "%";
        Fill.fillAmount = percentage / 100;
        if (percentage >= 100) 
        {
            if (LevelController.Instance)
            {
                UIManager.Instance.Ingame.Show(false);
                UIManager.Instance.Ingame.OnStart();
                LevelController.Instance.IsPlaying = true;
            }
            gameObject.SetActive(false);
        }
        
    }
}
