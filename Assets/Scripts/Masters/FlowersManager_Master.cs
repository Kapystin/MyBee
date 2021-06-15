using System;
using UnityEngine;

namespace Bee
{
    public class FlowersManager_Master : MasterSingleton<FlowersManager_Master>
    {        
        public event Action EventMoveToDestinationPoint;
        public event Func<Flower> EventGetRandomFlower;
        public event Action<Flower> EventRemoveFlower;
         
        public void CallEventUpdateAnimationState()
        {
            EventMoveToDestinationPoint?.Invoke();
        }

        public Flower CallEventGetRandomFlower()
        {
            return EventGetRandomFlower?.Invoke();
        }

        public void CallEventRemoveFlower(Flower flower)
        {
            EventRemoveFlower?.Invoke(flower);
        }
    }
}