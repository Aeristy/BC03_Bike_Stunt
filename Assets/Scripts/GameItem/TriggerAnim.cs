using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class TriggerAnim : MonoBehaviour
{
    public Animator anim;

    private List<Vector3> orgPosition = new List<Vector3> { };
    private List<Vector3> orgRotation = new List<Vector3> { };

    private string defaultAnim;

    private void Awake()
    {
        if (anim == null)
        {
            anim = GetComponent<Animator>();
            defaultAnim = anim.GetCurrentAnimatorClipInfo(0)[0].clip.name;
        }
        foreach (Transform t in transform)
        {
            orgPosition.Add(t.localPosition);
            orgRotation.Add(t.localEulerAngles);
        }
    }

    private void OnEnable()
    {
        anim.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("bike"))
        {
            anim.enabled = true;
            anim.Play(defaultAnim);
        }
    }

    public void ResetTransform()
    {
        anim.enabled = false;
        int i = 0;
        foreach (Transform t in transform)
        {
            t.localPosition = orgPosition[i];
            t.localEulerAngles = orgRotation[i];
            i++;
        }
    }
}
