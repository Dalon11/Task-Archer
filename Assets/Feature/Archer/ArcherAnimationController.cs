using UnityEngine;
using Spine;
using TaskArcher.Animations;

namespace TaskArcher.Archer.Controllers
{
    using Archer.Models;

    public class ArcherAnimationController : MonoBehaviour
    {
        [SerializeField] private SkeletonAnimationController _animationController;
        [Header("Settings")]
        [SerializeField] private ArcherAnimationModel _model;

        private Bone _upperBodyBone;

        private void Start()
        {
            _upperBodyBone = _animationController.SkeletonObject.FindBone(_model.UpperBodyBoneName);
            PlayIdleAnimation();
        }

        public void PlayIdleAnimation() => _animationController.PlayAnimation(_model.IdleAnimation, true);

        public void PlayStartAttackAnimation() => _animationController.PlayAnimation(_model.AttackStartAnimation, false);

        public void PlayAimingAnimation() => _animationController.PlayAnimation(_model.AimingAnimation, true);

        public void PlayEndAttackAnimation()
        {
            _animationController.PlayAnimation(_model.AttackFinishAnimation, false);
            _animationController.AddAnimation(_model.IdleAnimation, true);
        }

        public void RotateToDirection(Vector3 direction)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            AimAtMouse(angle);
        }

        private void AimAtMouse(float angle)
        {
            if (Mathf.Abs(angle) > _model.MaxUpperBodyAngle)
            {
                angle -= 180;
                angle *= -1;
                _animationController.SkeletonObject.ScaleX = -1;
            }
            else
                _animationController.SkeletonObject.ScaleX = 1;

            _upperBodyBone.Rotation = angle;
        }

    }
}