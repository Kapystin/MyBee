using System.Collections.Generic;
using UnityEngine;

namespace Bee
{
    public class Bee : BeeBase
    {
        public Flower CurrentTargetFlower;
        public int CurrentHoneyAmount;
        public int BeeHoneyMaxCapacity;
        public int BeeHoneyCollected;

        public void Initialize(Hive homeHive, float moveSpeed, int beeHoneyCollected, int beeHoneyMaxCapacity)
        {
            HomeHive = homeHive;
            MoveSpeed = moveSpeed;
            BeeHoneyCollected = beeHoneyCollected;
            BeeHoneyMaxCapacity = beeHoneyMaxCapacity;            
        }
    }

     
}