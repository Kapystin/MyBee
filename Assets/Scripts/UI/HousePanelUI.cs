using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Bee
{
    public class HousePanelUI : UI
    {
        [SerializeField] private bool _isHideOnStart;

        [SerializeField] private RectTransform _secondPanel;

        [SerializeField] private int _currentMoneyBalance; 

        [SerializeField] private float _slidePixel = 450f;
        [SerializeField] private float _slideStep = 9f;

        [SerializeField] private Button _hideSecondPanelButton;           
        [SerializeField] private Button _transferHoneyButton;           
        [SerializeField] private Button _saleHoneyButton;

        [SerializeField] private TMP_Text _moneyBalance;
        [SerializeField] private TMP_Text _honeyStoreAmount;
        [SerializeField] private TMP_Text _honeyPlayerStorAmount;

        private House _selectedHouse;
        private HouseSettings _houseSettings;
        private Player_Inventory _playerInventory;
        private PlayerSettings _playerSettings;


        private void Start()
        {
            if (_isHideOnStart)
            {
                Hide();
            }
            _playerInventory = PlayerManager_Master.Instance.GetComponent<Player_Inventory>();
            _houseSettings = Settings.GetReference<HouseSettings>();
            _playerSettings = Settings.GetReference<PlayerSettings>();
            base.ResetRectPosition();             
            AddListeners();
        }

        private void OnEnable()
        {
            UIManager_Master.Instance.EventShowHousePanel += ShowHousePanel;
            UIManager_Master.Instance.EventHideHousePanel += HideHousePanel;
        }

        private void OnDisable()
        {
            UIManager_Master.Instance.EventShowHousePanel -= ShowHousePanel;
            UIManager_Master.Instance.EventHideHousePanel -= HideHousePanel;
        }

        private void AddListeners()
        {
            _hideSecondPanelButton.onClick.RemoveAllListeners();
            _hideSecondPanelButton.onClick.AddListener(SlideHide);

            _saleHoneyButton.onClick.RemoveAllListeners();
            _saleHoneyButton.onClick.AddListener(SaleHoney);

            _transferHoneyButton.onClick.RemoveAllListeners();
            _transferHoneyButton.onClick.AddListener(TransferHoneyToHouse);
        }
  
        protected void ShowHousePanel(House house, Action callBack = null)
        {
            if (_lockState)
            {
                return;
            }

            callBack?.Invoke();
            base.Show();
            SlideShow();
            _selectedHouse = house;
            UpdateUI();
            _selectedHouse.OnValueChanged += UpdateUI;
        }

        protected void HideHousePanel(Action callBack = null)
        {
            base.Hide(callBack: callBack);
        }
        
        private void SlideShow()
        {            
            StartCoroutine(Slide(_secondPanel, SlideDirectionName.Horizontal, - _slideStep, _slidePixel)); 
        }

        private void SlideHide()
        {
            if (_lockState)
            {
                return;
            }
            
            StartCoroutine(Slide(_secondPanel, SlideDirectionName.Horizontal, _slideStep, _slidePixel));
            Hide(); 
            CameraManager_Master.Instance.CallEventSetCameraPosition(CameraPresetName.Player);
            PlayerManager_Master.Instance.CallEventUpdateAnimationState(PlayerAnimationState.Idle);
        }

        private void SaleHoney()
        {
            _currentMoneyBalance += _houseSettings.HoneyPrice * _selectedHouse.HoneyCurrentAmount;
            StatisticManager_Master.Instance.CallEventPlayerSaleHoneyAmount(_selectedHouse.HoneyCurrentAmount);
            StatisticManager_Master.Instance.CallEventPlayerRaiseMoney(_currentMoneyBalance);
            _selectedHouse.ZeroHoneyValue();
            UpdateUI();
        }

        private void TransferHoneyToHouse()
        {
            _selectedHouse.GiveHoney(_playerInventory.GetHoney());
            UpdateUI();
        }

        private void UpdateUI()
        {
            _moneyBalance.text = $"{_currentMoneyBalance}";
            _honeyStoreAmount.text = $"{_selectedHouse.HoneyCurrentAmount}/{_houseSettings.HoneyMaxAmountCapacity}";
            _honeyPlayerStorAmount.text = $"{_playerInventory.HoneyCurrentAmount}/{_playerSettings.HoneyMaxAmountCapacity}";
        }
    }
}