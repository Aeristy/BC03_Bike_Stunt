using UnityEngine;

namespace Kamgam.BikeAndCharacter25D
{
    public partial class Bike
    {
        [Header("GLIDER")]
        public bool isGliding;

        public Vector2 GliderForce;

        public float TurnMultiplier = 1f;

        public float GliderMaxSpeed = 10f;

        public Transform GliderForceLocation;

        protected void fixedUpdateGlider()
        {
            if (this.isGliding)
            {
                var gliderForce = GliderForce;
                var upVector = this.transform.TransformVector(Vector3.up);
                var idealAngle = new Vector3(-0.4f * TurnMultiplier, 1.0f, upVector.z);
                float directionFactorUp = Mathf.Clamp(1.0f - (Vector3.Angle(upVector, idealAngle) / 90f), 0.0f, 1.0f); // 0.0 - 1.0

                float speedFactorUp = Mathf.Clamp(Velocity - 5.0f, 5.0f, GliderMaxSpeed) / 10.0f; // 0.0 - GliderMaxSpeed/10

                gliderForce.x *= Mathf.Clamp(1.0f - directionFactorUp, 0.15f, 1.0f); // forward speed is inverse of up speed.
                gliderForce.x *= TurnMultiplier;
                gliderForce.y *= directionFactorUp * speedFactorUp;
                if (IsTouchingGround == false || Velocity > 5)
                {
                    BikeBody.gravityScale = Config.BikeGravityScale / 4f;
                    Debug.Log("Velocity: " + Velocity + ", Angle: " + directionFactorUp + " * Speed: " + speedFactorUp + " = " + (directionFactorUp * speedFactorUp));
                    Debug.Log(gliderForce);
                    this.BikeBody.AddForceAtPosition(gliderForce, GliderForceLocation.position, ForceMode2D.Force);
                }
                else
                {
                    BikeBody.gravityScale = Config.BikeGravityScale;
                }
            }
        }
    }
}