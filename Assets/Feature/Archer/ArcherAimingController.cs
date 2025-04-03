using UnityEngine;
using TaskArcher.Arrow.Controllers;
using TaskArcher.Trajectory.Controllers;

namespace TaskArcher.Archer.Controllers
{
    using Archer.Models;

    public class ArcherAimingController : MonoBehaviour
    {
        [SerializeField] private ArcherInputController _inputController;
        [SerializeField] private ArcherAnimationController _animationController;
        [SerializeField] private TrajectoryController _trajectorySystem;
        [SerializeField] private ArrowSpawner _arrowSpawner;
        [Header("Settings")]
        [SerializeField] private ArcherAimingModel _model;


        private Vector3 _aimDirection;
        private float _currentPower;

        public Vector3 AimDirection => _aimDirection;
        public float CurrentPower => _currentPower;

        private void Start() => Subscribe();

        private void OnDestroy() => Unsubscribe();

        public void StartAiming()
        {
            _animationController.PlayStartAttackAnimation();
            _trajectorySystem.ShowTrajectory();
        }

        public void UpdateAiming(Vector3 startPosition, Vector3 currentPosition)
        {
            _animationController.PlayAimingAnimation();
            CalculateDirection(startPosition, currentPosition);
            CalculatePower(startPosition, currentPosition);
            _animationController.RotateToDirection(_aimDirection);
            _trajectorySystem.UpdateTrajectory(_arrowSpawner.ArrowSpawnPoint.position, _aimDirection, _currentPower);
        }

        public void EndAiming()
        {
            _animationController.PlayEndAttackAnimation();
            _trajectorySystem.HideTrajectory(); Shoot();
        }

        private void Shoot() => _arrowSpawner.SpawnArrow(_aimDirection, _currentPower);

        private void CalculateDirection(Vector3 startPosition, Vector3 currentPosition)
        {
            if (startPosition.Equals(currentPosition))
            {
                _aimDirection = Vector2.right;
                return;
            }

            _aimDirection = (startPosition - currentPosition).normalized;
        }

        private void CalculatePower(Vector3 startPosition, Vector3 currentPosition)
        {
            float dragDistance = Vector3.Distance(startPosition, currentPosition);
            _currentPower = Mathf.Clamp(dragDistance * _model.PowerMultiplier, _model.MinPower, _model.MaxPower);
        }

        private void Subscribe()
        {
            _inputController.onStartAiming += StartAiming;
            _inputController.onAiming += UpdateAiming;
            _inputController.onEndAiming += EndAiming;
        }

        private void Unsubscribe()
        {
            _inputController.onStartAiming -= StartAiming;
            _inputController.onAiming -= UpdateAiming;
            _inputController.onEndAiming -= EndAiming;
        }
    }
}