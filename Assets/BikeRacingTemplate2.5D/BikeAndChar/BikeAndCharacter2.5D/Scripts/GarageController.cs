using UnityEngine;
using UnityEngine.SceneManagement;


public class GarageController : MonoBehaviour
{
    public static GarageController Instance;
    public Transform BikePos;


    private void Awake()
    {
        Instance = this;
    }
    
}


