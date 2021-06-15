using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Bee
{
    public class StatisticPanelUI : UI
    {
        [SerializeField] private bool _isHideOnStart;

        [SerializeField] private StatisticData _statisticData;
        [SerializeField] private RectTransform _secondPanel;
        [SerializeField] private Button _closeButton;

        [SerializeField] private TMP_Text _hivesCount;
        [SerializeField] private TMP_Text _beesWasCreated;
        [SerializeField] private TMP_Text _beesHasDied;
        [SerializeField] private TMP_Text _beesHarvestHoney;
        [SerializeField] private TMP_Text _playerHarvestHoney;
        [SerializeField] private TMP_Text _playerSaleHoneyAmount;
        [SerializeField] private TMP_Text _playerRaiseMoney;

        private void Start()
        {
            if (_isHideOnStart)
            {
                base.Hide(instaHide: true);
            }

            base.ResetRectPosition();
            AddListeners();
            _statisticData.OnValueChanged += UpdateUI;
        }

        private void OnEnable()
        {
            UIManager_Master.Instance.EventShowStatisticPanel += ShowStatisticPanel;
            UIManager_Master.Instance.EventHideStatisticPanel += HideStatisticPanel;
        }

        private void OnDisable()
        {
            UIManager_Master.Instance.EventShowStatisticPanel -= ShowStatisticPanel;
            UIManager_Master.Instance.EventHideStatisticPanel -= HideStatisticPanel;
        }

    
        private void AddListeners()
        {
            _closeButton.onClick.RemoveAllListeners();
            _closeButton.onClick.AddListener(() => HideStatisticPanel());
        }
         
        protected void ShowStatisticPanel(Action callBack = null)
        {
            base.Show(callBack);
            UpdateUI();
        } 

        protected void HideStatisticPanel(Action callBack = null)
        {
            UIManager_Master.Instance.CallEventShowMenuPanel();
            base.Hide(callBack: callBack);
        }

        private void UpdateUI()
        {
            _hivesCount.text = $"{_statisticData.HivesCount}";
            _beesWasCreated.text = $"{_statisticData.BeesWasCreated}";
            _beesHasDied.text = $"{_statisticData.BeesHasDied}";
            _beesHarvestHoney.text = $"{_statisticData.BeesHarvestHoney}";
            _playerHarvestHoney.text = $"{_statisticData.PlayerHarvestHoney}";
            _playerSaleHoneyAmount.text = $"{_statisticData.PlayerSaleHoneyAmount}";
            _playerRaiseMoney.text = $"{_statisticData.PlayerRaiseMoney}";
        }
    }
}