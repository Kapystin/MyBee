using System;
using UnityEngine;

namespace Bee
{
    public class PlayerManager_Master : MasterSingleton<PlayerManager_Master>
    {         
        public event Action<Vector3, Transform> EventMoveToDestinationPoint;
        public event Action<PlayerAnimationState> EventUpdateAnimationState;
        public event Action<int> EventCollectHoney;

        public void CallEventMoveToDestinationPoint(Vector3 destinationPoint, Transform lookAtPoint = null)
        {
            EventMoveToDestinationPoint?.Invoke(destinationPoint, lookAtPoint);
        } 

        public void CallEventUpdateAnimationState(PlayerAnimationState state)
        {
            EventUpdateAnimationState?.Invoke(state);
        } 

        public void CallEventCollectHoney(int honeyValue)
        {
            EventCollectHoney?.Invoke(honeyValue);
        } 
    }
}