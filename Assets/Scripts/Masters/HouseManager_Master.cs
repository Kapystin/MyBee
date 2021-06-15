using System;
using UnityEngine;

namespace Bee
{
    public class HouseManager_Master : MasterSingleton<HouseManager_Master>
    {        
        public event Action<Vector3, Transform> EventMoveToDestinationPoint;
       
        public void CallEventMoveToDestinationPoint(Vector3 destinationPoint, Transform lookAtPoint = null)
        {
            EventMoveToDestinationPoint?.Invoke(destinationPoint, lookAtPoint);
        } 
 
    }
}