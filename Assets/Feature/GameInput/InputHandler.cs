using System;
using UnityEngine;

namespace TaskArcher.GameInput
{
    public class InputHandler : MonoBehaviour
    {
        [SerializeField] private int _buttonActionIndex = 0;

        public event Action onButtonActionDown;
        public event Action onButtonAction;
        public event Action onButtonActionUp;

        private void Update() => MouseButtonHandler();

        private void MouseButtonHandler()
        {
            if (Input.GetMouseButtonDown(_buttonActionIndex))
                onButtonActionDown?.Invoke();
            if (Input.GetMouseButton(_buttonActionIndex))
                onButtonAction?.Invoke();
            if (Input.GetMouseButtonUp(_buttonActionIndex))
                onButtonActionUp?.Invoke();
        }
    }
}