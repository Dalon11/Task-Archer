using Spine.Unity;
using System;
using UnityEngine;

namespace TaskArcher.Archer.Models
{
    [Serializable]
    public class ArcherAnimationModel
    {
        [SerializeField] private AnimationReferenceAsset _idleAnimation;
        [SerializeField] private AnimationReferenceAsset _attackStartAnimation;
        [SerializeField] private AnimationReferenceAsset _aimingAnimation;
        [SerializeField] private AnimationReferenceAsset _attackFinishAnimation;
        [SerializeField] private float _maxUpperBodyAngle = 90f;
        [SerializeField] private string _upperBodyBoneName = "gun";

        public AnimationReferenceAsset IdleAnimation => _idleAnimation;
        public AnimationReferenceAsset AttackStartAnimation => _attackStartAnimation;
        public AnimationReferenceAsset AimingAnimation => _aimingAnimation;
        public AnimationReferenceAsset AttackFinishAnimation => _attackFinishAnimation;
        public float MaxUpperBodyAngle => _maxUpperBodyAngle;
        public string UpperBodyBoneName => _upperBodyBoneName;
    }
}