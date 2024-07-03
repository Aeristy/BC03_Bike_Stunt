using DG.Tweening;
using System.Collections;
using UnityEngine;

public class UIComplete : PopupBase
{
    public Transform[] Stars;

    protected override void OnEnable()
    {
        base.OnEnable();
        foreach (Transform _star in Stars)
        {
            _star.GetChild(0).gameObject.SetActive(false);
        }
        StartCoroutine(StarAnim());
    }
    public IEnumerator StarAnim()
    {
        int i = 0;
        
        foreach (Transform _star in Stars)
        {
            if (i < 3)
            {

                _star.GetChild(0).gameObject.SetActive(true);
                _star.GetChild(0).localScale = Vector3.zero;
                _star.GetChild(0).localPosition = new Vector3(0f, -100f, 0f);
                _star.GetChild(0).DOScale(Vector3.one, 0.4f).SetEase(Ease.OutBack);
                _star.GetChild(0).DOLocalMove(Vector3.zero, 0.4f).SetEase(Ease.OutBack).OnComplete(() =>
                {
                    //Blam.transform.position = _star.transform.position;
                    //Blam.Play();
                    //if (AudioController.Instance)
                    //    AudioController.Instance.Star();
                });

                yield return new WaitForSeconds(0.4f);
            }
            i++;
        }
        yield return new WaitForSeconds(0.5f);


    }
    public void BackToGarage()
    {
        GameManager.Instance.BackToGarage();
        Hide();
    }
    public void OnNext()
    {
        if (_player.currentLevel == DataManager.Instance.levels.listLevel.Count)
            BackToGarage();
        else
        {
            _player.currentLevel++;
            Utility.SaveGameData(_player);
            LevelController.Instance.OnlevelLoad();
            Hide();
        }
    }
}
