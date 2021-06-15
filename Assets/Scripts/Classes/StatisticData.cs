using System;
using UnityEngine;

namespace Bee
{
    public class StatisticData : MonoBehaviour
    {
        public event Action OnValueChanged;

        public int HivesCount;
        public int BeesWasCreated;
        public int BeesHasDied;
        public int BeesHarvestHoney;
        public int PlayerHarvestHoney;
        public int PlayerSaleHoneyAmount;
        public int PlayerRaiseMoney;

        private void OnEnable()
        {
            StatisticManager_Master.Instance.EventHiveRegister += AddHivesCount;
            StatisticManager_Master.Instance.EventBeeWasCreated += AddBeesWasCreatedCount;
            StatisticManager_Master.Instance.EventBeeHasDied += AddBeesHasDiedCount;
            StatisticManager_Master.Instance.EventBeeHarvestHoney += AddBeesHarvestHoneyCount;
            StatisticManager_Master.Instance.EventPlayerHarvestHoney += AddPlayerHarvestHoneyCount;
            StatisticManager_Master.Instance.EventPlayerSaleHoneyAmount += AddPlayerSaleHoneyAmountCount;
            StatisticManager_Master.Instance.EventPlayerRaiseMoney += AddPlayerRaiseMoneyCount;
        }

        private void OnDisable()
        {
            StatisticManager_Master.Instance.EventHiveRegister -= AddHivesCount;
            StatisticManager_Master.Instance.EventBeeWasCreated -= AddBeesWasCreatedCount;
            StatisticManager_Master.Instance.EventBeeHasDied -= AddBeesHasDiedCount;
            StatisticManager_Master.Instance.EventBeeHarvestHoney -= AddBeesHarvestHoneyCount;
            StatisticManager_Master.Instance.EventPlayerHarvestHoney -= AddPlayerHarvestHoneyCount;
            StatisticManager_Master.Instance.EventPlayerSaleHoneyAmount -= AddPlayerSaleHoneyAmountCount;
            StatisticManager_Master.Instance.EventPlayerRaiseMoney -= AddPlayerRaiseMoneyCount;
        }

        private void AddHivesCount()
        {
            HivesCount++;
            OnValueChanged?.Invoke();
        }

        private void AddBeesWasCreatedCount()
        {
            BeesWasCreated++;
            OnValueChanged?.Invoke();
        }

        private void AddBeesHasDiedCount()
        {
            BeesHasDied++;
            OnValueChanged?.Invoke();
        }

        private void AddBeesHarvestHoneyCount(int honeyValue)
        {
            BeesHarvestHoney += honeyValue;
            OnValueChanged?.Invoke();
        }

        private void AddPlayerHarvestHoneyCount(int honeyValue)
        {
            PlayerHarvestHoney += honeyValue;
            OnValueChanged?.Invoke();
        }

        private void AddPlayerSaleHoneyAmountCount(int honeyValue)
        {
            PlayerSaleHoneyAmount += honeyValue;
            OnValueChanged?.Invoke();
        }

        private void AddPlayerRaiseMoneyCount(int moneyValue)
        {
            PlayerRaiseMoney += moneyValue;
            OnValueChanged?.Invoke();
        }
    }
}