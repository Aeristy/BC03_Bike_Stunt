
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public UI_Controller TouchZone;
    private void Awake()
    {
        Instance = this;
    }


}
