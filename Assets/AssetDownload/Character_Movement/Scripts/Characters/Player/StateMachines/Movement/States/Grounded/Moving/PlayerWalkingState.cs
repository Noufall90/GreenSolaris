using UnityEngine;
using UnityEngine.InputSystem;

namespace GenshinImpactMovementSystem
{
    public class PlayerWalkingState : PlayerMovingState
    {
        private AudioSource walkAudio;

        public PlayerWalkingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
        }

        public override void Enter()
        {
            stateMachine.ReusableData.MovementSpeedModifier = groundedData.WalkData.SpeedModifier;
            stateMachine.ReusableData.BackwardsCameraRecenteringData = groundedData.WalkData.BackwardsCameraRecenteringData;

            base.Enter();

            StartAnimation(stateMachine.Player.AnimationData.WalkParameterHash);
            stateMachine.ReusableData.CurrentJumpForce = airborneData.JumpData.WeakForce;
        }

        public override void Exit()
        {
            base.Exit();

            StopAnimation(stateMachine.Player.AnimationData.WalkParameterHash);
            SetBaseCameraRecenteringData();

            // 🔇 Stop suara "walk"
            if (walkAudio != null)
            {
                walkAudio.Stop();
                walkAudio = null;
            }
        }

        protected override void OnWalkToggleStarted(InputAction.CallbackContext context)
        {
            base.OnWalkToggleStarted(context);
            stateMachine.ChangeState(stateMachine.RunningState);
        }

        protected override void OnMovementCanceled(InputAction.CallbackContext context)
        {
            stateMachine.ChangeState(stateMachine.LightStoppingState);
            base.OnMovementCanceled(context);
        }
    }
}
