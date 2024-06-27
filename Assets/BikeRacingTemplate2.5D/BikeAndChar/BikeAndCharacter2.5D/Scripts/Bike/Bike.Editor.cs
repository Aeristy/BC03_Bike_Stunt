#if UNITY_EDITOR
using UnityEngine;
using Kamgam.BikeAndCharacter25D.Helpers;

namespace Kamgam.BikeAndCharacter25D
{
    public partial class Bike // .Editor
    {
        public void OnValidate()
        {
            BackWheelGroundTouchTrigger.groundLayers = GroundLayers;
            BackWheelOuterGroundTouchTrigger.groundLayers = GroundLayers;
            FrontWheelGroundTouchTrigger.groundLayers = GroundLayers;
        }
    }
}
#endif
