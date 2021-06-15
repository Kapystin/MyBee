using UnityEngine;

namespace Bee
{
    public class Dron : BeeBase
    {
        public void Initialize(Hive homeHive, float moveSpeed)
        {
            HomeHive = homeHive;
            MoveSpeed = moveSpeed;             
        }
    }     
}