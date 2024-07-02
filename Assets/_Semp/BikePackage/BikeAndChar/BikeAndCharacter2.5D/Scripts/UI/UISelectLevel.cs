using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISelectLevel : PopupBase
{
   public void OnBack()
    {
        Hide();
        UIManager.Instance.Garage.Show();
    }
    public void OnSelectLevel(int levelId)
    {
        _player.currentLevel = levelId;
        Utility.SaveGameData(_player);
        GameManager.Instance.LoadTestScene();       
        Hide();
    }
    
}
