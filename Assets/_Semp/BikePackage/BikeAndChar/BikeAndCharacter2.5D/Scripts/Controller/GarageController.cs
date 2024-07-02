using Kamgam.BikeAndCharacter25D;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GarageController : MonoBehaviour
{
    public static GarageController Instance;
    public Transform BikePos;
    public BikeAndCharacter currentBike;
    public GameObject newBike;

    private PlayerModel _player;

    private void Awake()
    {
        Instance = this;
        _player = GameManager.Instance._player;
    }
    public void FixedUpdate()
    {
        //if (!paused)
        Physics2D.Simulate(Time.fixedDeltaTime);
    }
    private void Start()
    {
        //StartCoroutine(SpawnBike(level.StartPoint.position));
        Physics2D.simulationMode = SimulationMode2D.Script;
        ChangeBike(_player.currentBike);
        //level.GenMesh();
        //if(currentBike)
        //    currentBike.SetRigidKinematic();
    }
    public void ChangeBike(int bikeid)
    {
        Destroy(currentBike.gameObject);
        BikeModel bike = DataManager.Instance.bikes.listBike.Where(x => x.Id == bikeid).FirstOrDefault();
        GameObject bikeObject = Instantiate(bike.bikeSource, BikePos);
        currentBike = bikeObject.GetComponent<BikeAndCharacter>();
        _player.currentBike = bikeid;
        Utility.SaveGameData(_player);
        //currentBike.SetRigidKinematic();
    }
    
}


