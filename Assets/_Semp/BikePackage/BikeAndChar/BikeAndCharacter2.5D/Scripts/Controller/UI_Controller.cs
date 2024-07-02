using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_Controller : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    // Start is called before the first frame update

    public void OnPointerDown(PointerEventData eventData)
    {
        pressing = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        pressing = false;
    }


    private void Update()
    {
        if (pressing)
        {
            input += Time.deltaTime * 3f;
        }
        else
        {
            input -= Time.deltaTime * 5f;
        }
        if (input < 0f)
        {
            input = 0f;
        }
        if (input > 1f)
        {
            input = 1f;
        }
    }

    private void OnDisable()
    {
        input = 0f;
        pressing = false;
    }

    public float input;

    public bool pressing;
}

