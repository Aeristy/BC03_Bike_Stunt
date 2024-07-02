using Kamgam.Terrain25DLib;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public Transform StartPoint;

    public List<TriggerAnim> Animations;

    public MeshGenerator meshGenerator;

    public bool levelReady;

    private void OnEnable()
    {
        meshGenerator.OnPostMeshGenerated += OnGenMeshComplete;
    }

    private void OnDisable()
    {
        meshGenerator.OnPostMeshGenerated += OnGenMeshComplete;
    }

    public void ResetAnimation()
    {
        foreach (var anim in Animations)
        {
            anim.ResetTransform();
        }
    }

    public void ResetLevel()
    {
        ResetAnimation();
    }

    public void GenMesh()
    {
        meshGenerator.GenerateMesh();
    }

    private void OnGenMeshComplete(List<MeshFilter> lstMest)
    {
        levelReady = true;
        //LevelController.Instance.OnlevelLoaded();
    }
}
