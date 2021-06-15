using UnityEngine;

namespace Bee
{
    public class CollectHoneyState : IBeeState
    {
        private float _tickRate = 1f;         
        private float _tickCooldown;

        public CollectHoneyState(float tickRate)
        {
            _tickRate = tickRate;            
        }

        public void Action(BeeBase bee)
        {
            if (_tickCooldown <= 0)
            {
                _tickCooldown = _tickRate;
                StartCollectHoney(bee);
            }

            _tickCooldown -= Time.deltaTime;
        }

        private void StartCollectHoney(BeeBase bee)
        {
            if (bee.GetType() != typeof(Bee))
            {
                return;
            }

            Bee currentBee = (Bee)bee;

            if (currentBee.CurrentTargetFlower == null)
            {
                bee.SetState(BeeStateName.FindFlowerState);                
                return;
            }
             
            if (currentBee.CurrentTargetFlower.HoneyCapacityAmount > 0)
            {
                if (currentBee.CurrentHoneyAmount >= currentBee.BeeHoneyMaxCapacity)
                {
                    currentBee.CurrentTargetFlower.CurrentBeeAttackAmount--;
                    currentBee.SetState(BeeStateName.ReturnToHiveState);
                    return;
                }

                currentBee.CurrentHoneyAmount += currentBee.CurrentTargetFlower.GetHoney(currentBee.BeeHoneyCollected);

                if (currentBee.CurrentHoneyAmount >= currentBee.BeeHoneyMaxCapacity)
                {
                    currentBee.CurrentTargetFlower.CurrentBeeAttackAmount--;
                    currentBee.SetState(BeeStateName.ReturnToHiveState);
                    return;
                }
            }             
        }
    } 
}