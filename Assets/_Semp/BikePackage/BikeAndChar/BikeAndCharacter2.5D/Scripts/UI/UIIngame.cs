using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIIngame : PopupBase
{
    public GameObject touchZone;
    public GameObject fail;
    public GameObject win;
    public Text timer;

    protected override void OnEnable()
    {
        base.OnEnable();
        fail.SetActive(false);
        win.SetActive(false);
        touchZone.SetActive(false);
    }
    private void Update()
    {
        TimeSpan span = TimeSpan.FromSeconds((double)(new decimal(LevelController.Instance.timer)));
        //timer.text = span.Minutes.ToString("00") +":"+ span.Seconds.ToString("00") + ":"+ span.Milliseconds;
        timer.text = span.ToString("mm\\:ss\\:ff");
        
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
    public void OnPause()
    {
        LevelController.Instance.OnPause();
    }
}
