using Kamgam.BikeAndCharacter25D;
using System.Collections;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public static LevelController Instance;

    public BikeAndCharacter bikeController;
    public BC_Camera cameraman;
    public LevelPrefab levelPrefab;

    private GameObject BikeObject;

    private PlayerModel _player;
    [HideInInspector] public Vector3 lastCheckpoint;

    private void Awake()
    {
        Instance = this;
        _player = GameManager.Instance._player;
    }

    private void Start()
    {
        //StartCoroutine(SpawnBike(level.StartPoint.position));
        Physics2D.simulationMode = SimulationMode2D.Script;
        //level.GenMesh();
        UIManager.Instance.Ingame.Show();
        OnlevelLoad();
        StartCoroutine(SpawnBike(levelPrefab.startPoint.position));
    }

    protected IEnumerator SpawnBike(Vector3 _pos)
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
        //BikeObject = Instantiate(BikePrefab, transform);
        BikeObject = Instantiate(DataManager.Instance.bikes.listBike[_player.currentBike - 1].bikeSource, _pos, Quaternion.identity,transform);
        //BikeObject.transform.position = _pos;
        bikeController = BikeObject.GetComponent<BikeAndCharacter>();
        bikeController.PauseBike(true);
        bikeController.HandleUserInput = false;
        bikeController.Bike.IsBraking = true;

        // inform cameraman
        cameraman.SetTarget(bikeController.Character.TorsoBody);

        yield return new WaitForSeconds(0.5f);
        bikeController.HandleUserInput = true;
        bikeController.PauseBike(false);
    }

    public void OnlevelLoad()
    {
        GameObject level = Instantiate(DataManager.Instance.levels.listLevel[0].LevelSource, transform);
        levelPrefab = level.GetComponent<LevelPrefab>();
        UIManager.Instance.Ingame.Show();
    }




    public void SavePoint(Transform savePoint)
    {
        lastCheckpoint = savePoint.position;
        lastCheckpoint += Vector3.up * 0.5f;
    }



    public void FixedUpdate()
    {
        //if (!paused)
        Physics2D.Simulate(Time.fixedDeltaTime);
    }

}
