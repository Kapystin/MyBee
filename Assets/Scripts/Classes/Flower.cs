using System;
using UnityEngine;

namespace Bee
{
    public class Flower: MonoBehaviour
    {
        public int CurrentBeeAttackAmount;        
        public int MaxBeeAttachAmount;
        public int HoneyCapacityAmount => _honeyCapacityAmount;
        public int _honeyCapacityAmount;

        private float _tickRate = 1f;         
        private float _tickCooldown;

        private FlowersSettings _flowersSettings;

        private void Start()
        {
            _flowersSettings = Settings.GetReference<FlowersSettings>();
            _tickRate = _flowersSettings.TickRate;
            
        }

        private void Update()
        {
            if (_tickCooldown <= 0f)
            {
                _tickCooldown = _tickRate;

                GenerateHoney(_flowersSettings.HoneyRecoveredAmount);
            }

            _tickCooldown -= Time.deltaTime;
        }
        
        public void SetHoneyCapacity(int honeyValue)
        {
            _honeyCapacityAmount = honeyValue;
        }

        public int GetHoney(int honeyValue)
        {
            _honeyCapacityAmount -= honeyValue;
            if (_honeyCapacityAmount <= 0)
            {
                honeyValue += _honeyCapacityAmount;
                FlowersManager_Master.Instance.CallEventRemoveFlower(this);
                Destroy(gameObject, 0.1f);
                return honeyValue;
            }
            return honeyValue;
        }

        private void GenerateHoney(int honeyValue)
        {
            _honeyCapacityAmount += honeyValue;
        }
 
    }
}
