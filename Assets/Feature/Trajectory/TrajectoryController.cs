using UnityEngine;
using TaskArcher.Other;

namespace TaskArcher.Trajectory.Controllers
{
    using Trajectory.Models;

    public class TrajectoryController : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _trajectoryPointPrefab;
        [SerializeField] private Transform _pointsParent;
        [Header("Settings")]
        [SerializeField] private TrajectoryModel _model;

        private ObjectPool _pointsPool;

        private void Awake() => _pointsPool = new ObjectPool(_trajectoryPointPrefab.gameObject);

        private void Start()
        {
            CreateTrajectoryPoints();
            HideTrajectory();
        }

        public void ShowTrajectory() => TrajectoryActive(true);

        public void HideTrajectory() => TrajectoryActive(false);

        public void UpdateTrajectory(Vector3 position, Vector3 direction, float power, float gravity = 9.8f)
        {
            Vector3 velocity = direction * power;
            for (int i = 0; i < _pointsPool.GetPool.Count; i++)
            {
                float timeStep = (i + 1) * _model.TrajectoryPointSpacing;
                _pointsPool[i].transform.position = TrajectoryFormula(position, velocity, timeStep, gravity);
            }
        }

        private void CreateTrajectoryPoints()
        {
            for (int i = 0; i < _model.TrajectoryPointCount; i++)
            {
                GameObject point = _pointsPool.GetObject();
                point.transform.SetParent(_pointsParent);
                float step = (float)i / (_model.TrajectoryPointCount - 1);
                ChengeScalePoints(point, step);
                ChengeColorPoints(point, step);
            }
        }

        private void ChengeScalePoints(GameObject point, float step) =>
            point.transform.localScale = Vector3.Lerp(Vector3.one * _model.PointStartScale, Vector3.one * _model.PointEndScale, step);

        private void ChengeColorPoints(GameObject point, float step)
        {
            if (point.TryGetComponent(out SpriteRenderer renderer))
                renderer.color = Color.Lerp(_model.StartColor, _model.EndColor, step);
        }

        private void TrajectoryActive(bool isActive)
        {
            for (int i = 0; i < _pointsPool.GetPool.Count; i++)
                _pointsPool[i].SetActive(isActive);
        }

        /// <summary>
        /// x = x0 + vx * t;
        /// y = y0 + vy * t - 0.5 * g * t^2;
        /// </summary>
        private Vector3 TrajectoryFormula(Vector3 position, Vector3 velocity, float time, float gravity) => new Vector3(
                position.x + velocity.x * time,
                position.y + velocity.y * time - 0.5f * gravity * time * time,
                position.z);
    }
}