using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bee
{
    public class Player_Inventory : MonoBehaviour
    {
        public int HoneyCurrentAmount => _honeyCurrentAmount;
        [SerializeField] private int _honeyCurrentAmount;

        private PlayerSettings _playerSettings;
        private void OnEnable()
        {
            PlayerManager_Master.Instance.EventCollectHoney += CollectHoney;
        }

        private void OnDisable()
        {
            PlayerManager_Master.Instance.EventCollectHoney -= CollectHoney;
        }

        private void Start()
        {
            _playerSettings = Settings.GetReference<PlayerSettings>();
        }

        public int GetHoney()
        {
            int value = _honeyCurrentAmount;
            _honeyCurrentAmount = 0;
            return value;
        }

        private void CollectHoney(int honeyValue)
        {
            _honeyCurrentAmount += honeyValue;
            _honeyCurrentAmount = Mathf.Clamp(_honeyCurrentAmount, 0, _playerSettings.HoneyMaxAmountCapacity);

            StatisticManager_Master.Instance.CallEventPlayerHarvestHoney(honeyValue);
        }
    }
}