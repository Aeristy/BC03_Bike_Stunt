using UnityEngine;
using System.Collections;
using System;

namespace Kamgam.BikeAndCharacter25D
{
    /// <summary>
    /// Is used by the bike to determine if it hits the ground.
    /// </summary>
    [RequireComponent(typeof(Collider2D))]
    public class GroundTouchTrigger : MonoBehaviour
    {
        public LayerMask groundLayers;

        [HideInInspector]
        public bool IsTouching = false;

        [HideInInspector]
        public bool HasChanged = false;

        protected float lastChangeTime = 0.0f;
        protected bool lastIsTouchingState = false;

        [HideInInspector]
        public Collider2D Other = null;

        [System.NonSerialized]
        public Collider2D Collider;

        [HideInInspector]
        public float TimeSinceLastChange
        {
            get
            {
                return Time.time - lastChangeTime;
            }
        }

        void Awake()
        {
            Collider = this.GetComponent<Collider2D>();
        }

        void FixedUpdate()
        {
            IsTouching = Collider.IsTouchingLayers(groundLayers);

            if (lastIsTouchingState != IsTouching)
            {
                lastChangeTime = Time.time;
                lastIsTouchingState = IsTouching;
                HasChanged = true;
            }
            else
            {
                HasChanged = false;
            }
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            Other = collider;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            OnTriggerEnter2D(collision.collider);
        }
    }
}
