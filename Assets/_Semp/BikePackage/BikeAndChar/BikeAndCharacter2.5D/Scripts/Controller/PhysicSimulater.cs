using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicSimulater : MonoBehaviour
{
    public void FixedUpdate()
    {
        //if (!paused)
        Physics2D.Simulate(Time.fixedDeltaTime);
    }
}
