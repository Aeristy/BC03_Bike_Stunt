
using System.Collections;
using UnityEngine;

namespace Kamgam.BikeAndCharacter25D
{
    public class BikeAndCharacter : MonoBehaviour
    {
        public Bike Bike;
        public Character Character;

        [System.NonSerialized]
        public bool HandleUserInput = true;

        [System.NonSerialized]
        public IBikeTouchInput TouchInput;

        [HideInInspector] public bool Pause;

        public float CurrentSpeed  
        {
            get
            {
                return Bike.Velocity;
            }
        }

        public float IsAccel
        {
            get
            {
                return TouchInput.IsSpeedUpPressed() ? 1 : 0f;
            }
        }

        public bool InAir
        {
            get
            {
                return !Bike.IsTouchingGround;
            }
        }
        public bool isRotating;
        public bool isInGarage = false;
        private void OnEnable()
        {
            
        }
        public void Update()
        {
            if (GarageController.Instance)
            {
                isInGarage = true;
            }
            if (isInGarage) return;
            if (!HandleUserInput)
                return;

            //if (TouchInput != null)
            //{
            //    Bike.IsBraking = TouchInput.IsBrakePressed();
            //    Bike.IsSpeedingUp = TouchInput.IsSpeedUpPressed();
            //    Bike.IsRotatingCW = TouchInput.IsRotateCWPressed();
            //    Bike.IsRotatingCCW = TouchInput.IsRotateCCWPressed();
            //    Bike.IsBoost = TouchInput.IsBoostPressed();
            //}

            //Bike.IsBraking = Bike.IsBraking || (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A));
            //Bike.IsSpeedingUp = Bike.IsSpeedingUp || (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.Space));
            //Bike.IsRotatingCW = Bike.IsRotatingCW || (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W));
            //Bike.IsRotatingCCW = Bike.IsRotatingCCW || (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S));

            Bike.IsBoost = Bike.IsBoost;
            Bike.IsSpeedingUp = UIManager.Instance.TouchZone.pressing;
            if (InAir && Bike.IsSpeedingUp)
            {
                
                if (!isRotating)
                {
                    StartCoroutine(StartRotating());


                }

                //else
                //Bike.IsRotatingCW = true;
            }
            else
            {
                isRotating = false;
                Bike.IsRotatingCW = false;
                Bike.IsRotatingCCW = false;
            }
            if (Bike.IsRotatingCW)
                Character.TiltForward();
            else if (Bike.IsRotatingCCW)
                Character.TiltBackward();
            else
                Character.StopTilt();
            
        }

        public void StopAllInput()
        {
            Bike.IsBraking = false;
            Bike.IsSpeedingUp = false;
            Bike.IsRotatingCW = false;
            Bike.IsRotatingCCW = false;

            Character.StopTilt();
        }
        public IEnumerator StartRotating()
        {
            if (isRotating)
            {
                
                Bike.IsRotatingCCW = true;
            }
            else
            {
                yield return new WaitForSeconds(0.1f);
                isRotating = true;
                Bike.IsRotatingCCW = true;
            }
            
            //if (Bike.transform.eulerAngles.z > 90) Bike.IsRotatingCCW = true;
            //else Bike.IsRotatingCW = true;
            
        }
        public void PauseBike(bool pause)
        {
            Pause = pause;
            HandleUserInput = !pause;
            Bike.SetPaused(pause);
        }
        public void SetRigidKinematic()
        {
            Bike.BikeBody.isKinematic = true;
            Bike.FrontWheelBody.isKinematic = true;
            Bike.BackWheelBody.isKinematic = true;
            Character.HeadBody.isKinematic = true;
            Character.TorsoBody.isKinematic = true;
            Character.UpperArmBody.isKinematic = true;
            Character.LowerArmBody.isKinematic = true;
            Character.UpperLegBody.isKinematic = true;
            Character.LowerLegBody.isKinematic = true;

        }
        public void AddForce(Vector2 force)
        {
            Bike.GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Impulse);
        }

        public void DisconectCharacter()
        {
            DisconnectCharacterFromBike(Bike, Character);
        }

        public void DisconnectCharacterFromBike(Bike bike, Character character, bool addImpulse = true)
        {
            // disconnect char if needed
            if (character.IsConnectedToBike)
            {
                character.DisconnectFromBike(bike, addImpulse);
                bike.StopAllInput();
            }
        }


        public void ConnectCharacterToBike(Bike bike, Character character)
        {
            if (!character.IsConnectedToBike)
            {
                character.ConnectToBike(Bike);
                bike.StopAllInput();
            }
        }

        public Vector3 GetPosition()
        {
            return Bike.transform.position;
        }

        public void DisableRigi()
        {
            foreach (Rigidbody2D rigi in GetComponentsInChildren<Rigidbody2D>())
            {
                rigi.isKinematic = true;
            }
        }

        public void EnableRigi()
        {
            foreach (Rigidbody2D rigi in GetComponentsInChildren<Rigidbody2D>())
            {
                rigi.isKinematic = false;
            }
        }
    }
}