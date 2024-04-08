using UnityEngine;

namespace Player.InputReader
{
    public class PlayerInput : MonoBehaviour
    {
        public bool IsButtonHold => Input.GetMouseButton(0);
        public bool IsButtonReleased => Input.GetMouseButtonUp(0);

        public bool IsInDrivingMode { get; private set; } = true;

        public void SwitchInput(bool state) =>
            IsInDrivingMode = state;
    }
}
