using System;
using TaskArcher.GameInput;
using UnityEngine;

namespace TaskArcher.Archer.Controllers
{
    public class ArcherInputController : MonoBehaviour
    {
        [SerializeField] private InputHandler _input;

        private Camera _mainCamera;
        private bool _isAiming = false;
        private Vector3 _startAimPosition;

        public Action onStartAiming;
        public Action<Vector3, Vector3> onAiming;
        public Action onEndAiming;

        private void Start()
        {
            _mainCamera = Camera.main;
            Subscribe();
        }

        private void OnDestroy() => Unsubscribe();

        private void OnStartAiming()
        {
            if (_isAiming)
                return;

            _isAiming = true;
            _startAimPosition = GetMouseWorldPosition();
            onStartAiming?.Invoke();
        }

        private void OnAiming()
        {
            if (!_isAiming)
                return;

            onAiming?.Invoke(_startAimPosition, GetMouseWorldPosition());
        }

        private void OnEndAiming()
        {
            if (!_isAiming)
                return;

            _isAiming = false;
            onEndAiming?.Invoke();
        }

        private Vector3 GetMouseWorldPosition()
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = -_mainCamera.transform.position.z;
            return _mainCamera.ScreenToWorldPoint(mousePos);
        }
        private void Subscribe()
        {
            _input.onButtonActionDown += OnStartAiming;
            _input.onButtonAction += OnAiming;
            _input.onButtonActionUp += OnEndAiming;
        }

        private void Unsubscribe()
        {
            _input.onButtonActionDown -= OnStartAiming;
            _input.onButtonAction -= OnAiming;
            _input.onButtonActionUp -= OnEndAiming;
        }
    }
}