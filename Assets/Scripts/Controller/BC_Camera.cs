using Kamgam.BikeAndCharacter25D.Helpers;
using UnityEngine;

public class BC_Camera : MonoBehaviour
{
    public Vector3 Offset = new Vector3(2.5f, 1f, -5f);

    public Vector2 SpeedZoomOffset = new Vector2(0f, 0f);

    public Vector2 SpeedZoomMinMaxVelocity = new Vector2(0, 13f);

    public float OffsetByItem = 0;

    public float TargetOffset = 0;

    public float PreOffset = 0;

    public TriggerOffset preTrigger;

    public TriggerOffset nextTrigger;

    public float ZoomByItem = 0;

    public float MaxDistance = 0;


    protected Vector2AverageQueue velocityAverage = new Vector2AverageQueue(30);

    public Transform cameraToMove;
    public Rigidbody2D objectToTrack;


    public void SetTarget(Rigidbody2D obj)
    {
        cameraToMove = transform;
        objectToTrack = obj;
        OffsetByItem = 0;
        TargetOffset = 0;
        PreOffset = 0;
        ZoomByItem = 0;
        MaxDistance = 0;

        preTrigger = null;
        nextTrigger = null;
    }


    public void SetObjectToTrack(Rigidbody2D obj)
    {
        this.objectToTrack = obj;
    }

    public bool HasValidTargets()
    {
        return cameraToMove != null && cameraToMove.gameObject != null
            && objectToTrack != null && objectToTrack.gameObject != null;
    }


    public void LateUpdate()
    {
        if (!HasValidTargets())
            return;

        // update average velocity
        velocityAverage.Enqueue(objectToTrack.velocity);
        velocityAverage.UpdateAverage();

        // calc final offset based on the velocity average.
        var offset = Offset;
        float delta = SpeedZoomMinMaxVelocity.y - SpeedZoomMinMaxVelocity.x;
        float zoomFactor = (velocityAverage.Average().magnitude - SpeedZoomMinMaxVelocity.x) / delta;

        offset.y += zoomFactor * SpeedZoomOffset.x + OffsetByItem;
        offset.z -= zoomFactor * SpeedZoomOffset.y + ZoomByItem;

        // move camera
        if (objectToTrack != null && cameraToMove != null)
        {
            // use LateUpdate for tracking if the rigidbody is interpolating
            if (objectToTrack.interpolation != RigidbodyInterpolation2D.None)
                cameraToMove.position = objectToTrack.transform.position + offset;
        }
    }

}
