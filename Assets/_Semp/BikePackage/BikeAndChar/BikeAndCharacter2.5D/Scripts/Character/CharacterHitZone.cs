using Kamgam.BikeAndCharacter25D;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHitZone : MonoBehaviour
{
    public LayerMask mask;
    private int layerMaskIndex;
    private Character character;
    private void Awake()
    {
        layerMaskIndex = (int)Mathf.Log(mask.value, 2);
        character = transform.parent.GetComponent<Character>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == layerMaskIndex)
        {
            character.OnHitGround();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == layerMaskIndex)
        {
            character.OnHitGround();
        }
    }
}
