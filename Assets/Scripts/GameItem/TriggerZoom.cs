using Kamgam.BikeAndCharacter25D.Helpers;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[RequireComponent(typeof(BoxCollider2D))]
public class TriggerZoom : MonoBehaviour, ITrigger2DReceiver
{
    public float targetZoom = 10;

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
        mainCam.ZoomByItem = 0;
    }
    public void OnCustomTriggerStay2D(Trigger2D trigger, Collider2D other)
    {
        float currentDistance = Vector2.Distance(transform.position, other.transform.position);
        if (currentDistance > maxDistance) currentDistance = maxDistance;

        if (mainCam == null) mainCam = FindObjectOfType<BC_Camera>();

        if (mainCam)
        {
            mainCam.ZoomByItem = targetZoom * (maxDistance - currentDistance) / maxDistance;
        }
    }


#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0f, 0f, 1f, 0.1f);
        if (EditorApplication.isPlayingOrWillChangePlaymode)
        {
            Gizmos.DrawWireCube(transform.position + Vector3.up * 1.5f, new Vector3(0.5f, 3f, 1f));
        }
        else
        {
            Gizmos.DrawCube(transform.position + Vector3.up * 1.5f, new Vector3(0.5f, 3f, 1f));

            // While we are at it, constrain the z pos to 0.
            var pos = transform.position;
            pos.z = 0;
            transform.position = pos;
        }
    }
#endif
}
