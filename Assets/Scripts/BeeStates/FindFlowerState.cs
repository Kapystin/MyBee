using UnityEngine;

namespace Bee
{
    public class FindFlowerState : IBeeState
    {        
        private Vector3 _flowerOffset;

        public void Action(BeeBase bee)
        {
            if (bee.GetType() != typeof(Bee))
            {
                return;
            }

            Bee currentBee = (Bee)bee;

            if (currentBee.CurrentTargetFlower == null)
            {
                FindFlower(currentBee);                
            }
            else
            {
                CheckDistanceToTarget(currentBee);
                MoveToFlower(currentBee);                
            }
        }    
        
        private void FindFlower(Bee bee)
        {
            bee.CurrentTargetFlower = FlowersManager_Master.Instance.CallEventGetRandomFlower();
            
            if (bee.CurrentHoneyAmount >= bee.BeeHoneyMaxCapacity)
            {
                bee.SetState(BeeStateName.ReturnToHiveState);                
                return;
            }

            if (bee.CurrentTargetFlower.CurrentBeeAttackAmount + 1 >= bee.CurrentTargetFlower.MaxBeeAttachAmount)
            {
                bee.CurrentTargetFlower = null;                
                return;
            }

            _flowerOffset = new Vector3(0f, 1f, 0f);
            bee.CurrentTargetFlower.CurrentBeeAttackAmount++;
        }

        private void MoveToFlower(Bee bee)
        {       
            if (bee.CurrentTargetFlower == null)
            {
                return;
            }

            bee.transform.LookAt(bee.CurrentTargetFlower.transform.position + _flowerOffset);
            Vector3 targetPosition = Vector3.MoveTowards(bee.transform.position,
                                                        bee.CurrentTargetFlower.transform.position + _flowerOffset, 
                                                        bee.MoveSpeed * Time.deltaTime);
            bee.transform.position = targetPosition;
        }

        private void CheckDistanceToTarget(Bee bee)
        {
            if (bee.CurrentTargetFlower == null)
            {
                return;
            }

            if (Vector3.Distance(bee.transform.position, bee.CurrentTargetFlower.transform.position + _flowerOffset) < 0.5f)
            {                
                bee.SetState(BeeStateName.CollectHoneyState);                
            }
        }
    }
}