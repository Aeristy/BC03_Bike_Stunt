public class UIPause : PopupBase
{
    public void BackToGarage()
    {
        GameManager.Instance.BackToGarage();
        Hide();
    }
    public void OnNext()
    {
        LevelController.Instance.IsPause = false;
        LevelController.Instance.OnlevelLoad();
        Hide();

    }
    public void OnResume()
    {
        LevelController.Instance.IsPause = false;
        UIManager.Instance.Ingame.Show(false);
        UIManager.Instance.Ingame.OnStart();
        Hide();
    }
}
