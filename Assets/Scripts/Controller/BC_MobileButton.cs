using BC.Parking;
using Kamgam.BikeAndCharacter25D;
using UnityEngine;

public class BC_MobileButton : MonoBehaviour, IBikeTouchInput
{
    public BC_UIController GasButton;
    public BC_UIController BrakeButton;
    public BC_UIController RotateRightButton;
    public BC_UIController RotateLeftButton;
    public BC_UIController BoostButton;

    bool IBikeTouchInput.IsSpeedUpPressed()
    {
        return GasButton.pressing;
    }

    bool IBikeTouchInput.IsBrakePressed()
    {
        return BrakeButton.pressing;
    }

    public bool IsRotateCWPressed()
    {
        if (RotateRightButton) return RotateRightButton.pressing;
        else return Input.acceleration.x > 0;
    }

    public bool IsRotateCCWPressed()
    {
        if (RotateLeftButton) return RotateLeftButton.pressing;
        else return Input.acceleration.x < 0;
    }

    public bool IsBoostPressed()
    {
        return BoostButton.pressing;
    }
}
