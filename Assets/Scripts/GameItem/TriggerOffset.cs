using Kamgam.BikeAndCharacter25D.Helpers;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class TriggerOffset : MonoBehaviour, ITrigger2DReceiver
{
    public float targetOffset = -1;

    public bool autoDistance = false;

    public float maxDistance;

    private BC_Camera mainCam;

    public void OnCustomTriggerEnter2D(Trigger2D trigger, Collider2D other)
    {
        if (!autoDistance)
            maxDistance = Vector2.Distance(transform.position, other.transform.position);

        mainCam = FindObjectOfType<BC_Camera>();
    }

    public void OnCustomTriggerExit2D(Trigger2D trigger, Collider2D other)
    {
        if (mainCam == null) mainCam = FindObjectOfType<BC_Camera>();
        mainCam.OffsetByItem = 0;
    }
    public void OnCustomTriggerStay2D(Trigger2D trigger, Collider2D other)
    {
        float currentDistance = Vector2.Distance(transform.position, other.transform.position);
        if (currentDistance > maxDistance) currentDistance = maxDistance;

        if (mainCam == null) mainCam = FindObjectOfType<BC_Camera>();

        if (mainCam)
        {
            mainCam.OffsetByItem = targetOffset * (maxDistance - currentDistance) / maxDistance;

        }
    }
}
