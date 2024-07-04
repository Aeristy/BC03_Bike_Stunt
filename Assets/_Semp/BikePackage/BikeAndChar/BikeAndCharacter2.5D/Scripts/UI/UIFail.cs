public class UIFail : PopupBase
{
    public void BackToGarage()
    {
        GameManager.Instance.BackToGarage();
        Hide();
    }
    public void OnNext()
    {

        LevelController.Instance.OnlevelLoad();
        Hide();

    }
}
