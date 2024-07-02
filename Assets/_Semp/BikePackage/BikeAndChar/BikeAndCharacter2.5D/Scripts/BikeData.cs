using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BikeDataAsset", menuName = "DataAsset/BikeDataAsset")]
public class BikeData : ScriptableObject
{
    public List<BikeModel> listBike;
}


[Serializable]
public class BikeModel
{
    public int Id;
    public int price;
    public float priceIAP;
    public int numberAds;
    public int currentAds;
    public string bikeName;
    public GameObject bikeSource;
    public GameObject bikeGhost;
    public int baseColor;
    public Sprite bikeThumb;
    public string bikeDes;


}