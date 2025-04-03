using System;
using UnityEngine;

namespace TaskArcher.Trajectory.Models
{
    [Serializable]
    public class TrajectoryModel
    {
        [SerializeField] private int _trajectoryPointCount = 10;
        [SerializeField] private float _trajectoryPointSpacing = 0.07f;
        [SerializeField] private Color _startColor = Color.red;
        [SerializeField] private Color _endColor = new Color(1, 0, 0, 0.2f);
        [SerializeField] private float _pointStartScale = 0.5f;
        [SerializeField] private float _pointEndScale = 0.2f;

        public int TrajectoryPointCount => _trajectoryPointCount;
        public float TrajectoryPointSpacing => _trajectoryPointSpacing;
        public Color StartColor => _startColor;
        public Color EndColor => _endColor;
        public float PointStartScale => _pointStartScale;
        public float PointEndScale => _pointEndScale;
    }
}