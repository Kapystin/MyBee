using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Bee
{
    public class Player_Animations : MonoBehaviour
    {
        [SerializeField]
        private Animator _activeAnimator;
  
        private void OnEnable()
        {
            PlayerManager_Master.Instance.EventUpdateAnimationState += UpdateAnimationState;           
        }

        private void OnDisable()
        {
            PlayerManager_Master.Instance.EventUpdateAnimationState -= UpdateAnimationState; 
        }
 
        private void UpdateAnimationState(PlayerAnimationState state)
        {
            int animID = GetAnimationStateIntValue(state);
            _activeAnimator.SetInteger(state.ToString(), animID);
            
            StartCoroutine(ResetAnimationState(state));
        }

        private IEnumerator ResetAnimationState(PlayerAnimationState state)
        {
            yield return new WaitForSeconds(0.3f);
            _activeAnimator.SetInteger(state.ToString(), 0);
        }
 
        private int GetAnimationStateIntValue(PlayerAnimationState state)
        {
            switch (state)
            {
                case PlayerAnimationState.Idle:
                    return Random.Range(1,3);
                case PlayerAnimationState.Walk:
                    return Random.Range(1, 3); 
                case PlayerAnimationState.Interact:
                    return 1; 
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
        }
    } 
}