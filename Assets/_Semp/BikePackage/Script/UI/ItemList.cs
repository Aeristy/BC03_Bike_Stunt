using UnityEngine;
using UnityEngine.UI;

public class ItemList : MonoBehaviour
{
    public Transform itemContainer;
    public GameObject itemPrefab;
    public ITEM_LIST_STATE state = ITEM_LIST_STATE.MOTORBIKE;

    private PlayerModel _player;
    private void Awake()
    {
        _player = GameManager.Instance._player;
    }

    private void Start()
    {
        state = ITEM_LIST_STATE.MOTORBIKE;
        LoadBikeList();

    }
    public void LoadBikeList()
    {
        foreach (BikeModel bike in DataManager.Instance.bikes.listBike)
        {
            GameObject item = Instantiate(itemPrefab, itemContainer);
            GarageItem garageItem = item.GetComponent<GarageItem>();
            garageItem.Thumb.sprite = bike.bikeThumb;
            garageItem.itemID = bike.Id;
            item.GetComponent<Button>().onClick.AddListener(() => OnChangeBike(bike.Id));
        }
    }
    public void OnChangeBike(int id)
    {
        if (_player.currentBike != id)
            GarageController.Instance.ChangeBike(id);
    }
}
public enum ITEM_LIST_STATE
{
    MOTORBIKE = 0,
    CHARACTER = 1,
}
