using System;
using UnityEngine;

namespace Bee
{
    public class StatisticManager_Master : MasterSingleton<StatisticManager_Master>
    {         
        public event Action EventHiveRegister;
        public event Action EventBeeWasCreated;
        public event Action EventBeeHasDied;        
        public event Action<int> EventBeeHarvestHoney;
        public event Action<int> EventPlayerHarvestHoney;
        public event Action<int> EventPlayerSaleHoneyAmount;
        public event Action<int> EventPlayerRaiseMoney;

        public void CallEventHiveRegister()
        {
            EventHiveRegister?.Invoke();
        }

        public void CallEventBeeWasCreated()
        {
            EventBeeWasCreated?.Invoke();
        }

        public void CallEventBeeHasDied()
        {
            EventBeeHasDied?.Invoke();
        }

        public void CallEventBeeHarvestHoney(int honeyValue)
        {
            EventBeeHarvestHoney?.Invoke(honeyValue);
        }
        
        public void CallEventPlayerHarvestHoney(int honeyValue)
        {
            EventPlayerHarvestHoney?.Invoke(honeyValue);
        }
        
        public void CallEventPlayerSaleHoneyAmount(int honeyValue)
        {
            EventPlayerSaleHoneyAmount?.Invoke(honeyValue);
        }

        public void CallEventPlayerRaiseMoney(int moneyValue)
        {
            EventPlayerRaiseMoney?.Invoke(moneyValue);
        }

    }
}