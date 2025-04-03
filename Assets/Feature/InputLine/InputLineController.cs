using TaskArcher.Archer.Controllers;
using UnityEngine;

namespace TaskArcher.InputLine.Controllers
{
    public class InputLineController : MonoBehaviour
    {
        [SerializeField] private ArcherInputController _inputController;
        [SerializeField] private LineRenderer trajectoryLine;
        [SerializeField] private int _trajectoryPointCount = 2;

        private void Awake() => SetupTrajectoryLine();

        private void Start() => Subscribe();

        private void OnDestroy() => Unsubscribe();

        private void UpdateLine(Vector3 startPosition, Vector3 currentPosition)
        {
            trajectoryLine.SetPosition(0, startPosition);
            trajectoryLine.SetPosition(1, currentPosition);
        }
        private void ShowLine() => trajectoryLine.enabled = true;

        private void HideLine() => trajectoryLine.enabled = false;

        private void SetupTrajectoryLine()
        {
            trajectoryLine.positionCount = _trajectoryPointCount;
            trajectoryLine.enabled = false;
        }

        private void Subscribe()
        {
            _inputController.onStartAiming += ShowLine;
            _inputController.onAiming += UpdateLine;
            _inputController.onEndAiming += HideLine;
        }

        private void Unsubscribe()
        {
            _inputController.onStartAiming -= ShowLine;
            _inputController.onAiming -= UpdateLine;
            _inputController.onEndAiming -= HideLine;
        }
    }
}