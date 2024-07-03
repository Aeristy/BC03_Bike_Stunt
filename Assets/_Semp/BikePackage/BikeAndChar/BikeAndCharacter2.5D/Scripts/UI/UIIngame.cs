using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIIngame : PopupBase
{
    public GameObject touchZone;
    public GameObject fail;
    public GameObject win;
    protected override void OnEnable()
    {
        base.OnEnable();
        fail.SetActive(false);
        win.SetActive(false);
        touchZone.SetActive(false);
    }
    public void OnStart()
    {
        touchZone.SetActive(true);
        
    }
    public void OnWin()
    {
        win.SetActive(true);
        win.transform.GetChild(0).localPosition = new Vector3(-2000, 0, 0);
        win.transform.GetChild(0).DOLocalMoveX(0, 0.5f, false);
    }
    public void OnFail()
    {
        fail.SetActive(true);
        fail.transform.GetChild(0).localPosition = new Vector3(-2000, 0, 0);
        fail.transform.GetChild(0).DOLocalMoveX(0, 0.5f, false);
    }
}
