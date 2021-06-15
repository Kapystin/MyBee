using UnityEngine;

namespace Bee
{
    public class ReturnToHiveState : IBeeState
    {
        private Vector3 _hiveOffset = new Vector3(0f, 0.5f, 0f);

        public void Action(BeeBase bee)
        {
            CheckDistanceToTarget(bee);
            MoveToHive(bee);
        }

        private void MoveToHive(BeeBase bee)
        {
            bee.transform.LookAt(bee.HomeHive.transform.position + _hiveOffset);
            Vector3 targetPosition = Vector3.MoveTowards(bee.transform.position,
                                                        bee.HomeHive.transform.position + _hiveOffset,
                                                        bee.MoveSpeed * Time.deltaTime);
            bee.transform.position = targetPosition;
        }

        private void CheckDistanceToTarget(BeeBase bee)
        {
            if (Vector3.Distance(bee.transform.position, bee.HomeHive.transform.position + _hiveOffset) < 0.1f)
            {
                if (bee.GetType() == typeof(Bee))
                {
                    Bee currentBee = (Bee)bee;
                    currentBee.HomeHive.GiveHoney(currentBee.CurrentHoneyAmount);
                    currentBee.CurrentHoneyAmount = 0;
                    currentBee.CurrentTargetFlower = null;                    
                }

                bee.SetState(BeeStateName.WaitInHiveState);
            }
        }
    }
}