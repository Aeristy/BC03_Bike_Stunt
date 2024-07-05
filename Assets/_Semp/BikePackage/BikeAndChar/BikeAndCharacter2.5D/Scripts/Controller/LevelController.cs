using Kamgam.BikeAndCharacter25D;
using System.Collections;
using System.Linq;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public static LevelController Instance;

    public BikeAndCharacter bikeController;
    public BC_Camera cameraman;
    public LevelPrefab levelPrefab;
    public bool IsPlaying = false;
    public bool IsPause = false;
    public float timer = 0;

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
        

        OnlevelLoad();
        
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

        
        if (!UIManager.Instance.LoadingLevel.isActiveAndEnabled)
        {
            UIManager.Instance.Ingame.Show(false);
            UIManager.Instance.Ingame.OnStart();
            IsPlaying = true;
        }
        yield return new WaitForSeconds(0f);
        bikeController.HandleUserInput = true;
        bikeController.PauseBike(false);

    }

    public void OnlevelLoad()
    {
        foreach(Transform t in transform)
        {
            Destroy(t.gameObject);
        }
        timer = 0;
        GameObject level = Instantiate(DataManager.Instance.levels.listLevel.Where(x => x.Id == _player.currentLevel).FirstOrDefault().LevelSource, transform);
        levelPrefab = level.GetComponent<LevelPrefab>();
        StartCoroutine(SpawnBike(levelPrefab.startPoint.position));
        
    }




    public void SavePoint(Transform savePoint)
    {
        lastCheckpoint = savePoint.position;
        lastCheckpoint += Vector3.up * 0.5f;
    }



    public void FixedUpdate()
    {
        //if (!paused)

        if(!IsPause)
        {
            Physics2D.Simulate(Time.fixedDeltaTime);
            timer += Time.fixedDeltaTime;
        }
    }
    public void OnWIn()
    {
        if (!IsPlaying) return;
        IsPlaying = false;
        UIManager.Instance.Ingame.OnWin();
        bikeController.Bike.IsBraking = true;
        bikeController.Bike.GetComponent<Rigidbody2D>().velocity = bikeController.Bike.GetComponent<Rigidbody2D>().velocity.normalized * 20;
        StartCoroutine(ShowComplete());
        
    }
    public void OnFail()
    {
        if (!IsPlaying) return;
        IsPlaying = false;
        bikeController.Bike.enabled = false;
        bikeController.Character.enabled = false;
        UIManager.Instance.Ingame.OnFail();
        StartCoroutine(ShowFail());
    }
    public IEnumerator ShowComplete()
    {
        yield return new WaitForSeconds(1.5f);
        UIManager.Instance.Complete.Show();
        UIManager.Instance.Ingame.Hide(false);
    }
    public IEnumerator ShowFail()
    {
        yield return new WaitForSeconds(1.5f);
        UIManager.Instance.Fail.Show();
        UIManager.Instance.Ingame.Hide(false);
    }
    public void OnPause()
    {
        IsPause = true;
        UIManager.Instance.Pause.Show();
        UIManager.Instance.Ingame.Hide(false);
    }

}
