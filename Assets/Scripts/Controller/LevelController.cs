using Kamgam.BikeAndCharacter25D;
using System.Collections;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public static LevelController Instance;

    public GameObject BikePrefab;
    private BikeAndCharacter bikeController;
    public BC_Camera cameraman;
    public Level level;
    public GameObject touchInput;

    private GameObject BikeObject;

    [HideInInspector] public Vector2 lastCheckpoint;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        //StartCoroutine(SpawnBike(level.StartPoint.position));
        Physics2D.simulationMode = SimulationMode2D.Script;
        //level.GenMesh();
    }

    protected IEnumerator SpawnBike(Vector2 _pos)
    {
        lastCheckpoint = _pos;

        // destroy old bike
        if (bikeController != null)
        {
            Destroy(BikeObject);
            BikeObject = null;
            bikeController = null;
            cameraman.SetObjectToTrack(null);
        }

        // create new
        BikeObject = Instantiate(BikePrefab, transform);
        BikeObject.transform.position = _pos;
        bikeController = BikeObject.GetComponent<BikeAndCharacter>();


        bikeController.PauseBike(true);
        bikeController.HandleUserInput = false;
        bikeController.Bike.IsBraking = true;
        bikeController.TouchInput = touchInput.GetComponent<IBikeTouchInput>();

        // inform cameraman
        cameraman.SetTarget(bikeController.Character.TorsoBody);

        yield return new WaitForSeconds(1.0f);
        bikeController.HandleUserInput = true;
        bikeController.PauseBike(false);
    }

    public void OnlevelLoaded()
    {
        Debug.Log("Level loaded");
    }


    public void OnReset()
    {
        StartCoroutine(SpawnBike(level.StartPoint.position));
        level.ResetLevel();
    }

    public void SavePoint(Transform savePoint)
    {
        lastCheckpoint = savePoint.position;
        lastCheckpoint += Vector2.up * 0.5f;
    }

    public void LoadSavePoint()
    {
        StartCoroutine(SpawnBike(lastCheckpoint));
        level.ResetAnimation();
    }

    public void FixedUpdate()
    {
        //if (!paused)
        Physics2D.Simulate(Time.fixedDeltaTime);
    }

}
