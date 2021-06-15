using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Bee
{
    public class HivePanelUI : UI
    {
        [SerializeField] private bool _isHideOnStart;

        [SerializeField] private RectTransform _secondPanel;

        [SerializeField] private float _slidePixel = 450f;
        [SerializeField] private float _slideStep = 9f;

        [SerializeField] private TMP_Text _honeyAmount;
        [SerializeField] private TMP_Text _beesAmount;

        [SerializeField] private Button _hideSecondPanelButton;
        [SerializeField] private Button _collectHoneyButton;

        [SerializeField] private Image _collectProgressBar;

        private Hive _selectedHive;
        private HiveSettings _hiveSettings;
         
        private void Start()
        {
            if (_isHideOnStart)
            {
                HideHivePanel();
            }
            
            base.ResetRectPosition();
            _hiveSettings = Settings.GetReference<HiveSettings>();
            _collectProgressBar.fillAmount = 0f;
            AddListeners();
        }

        private void OnEnable()
        {
            UIManager_Master.Instance.EventShowHivePanel += ShowHivePanel;
            UIManager_Master.Instance.EventHideHivePanel += HideHivePanel;
        }

        private void OnDisable()
        {
            UIManager_Master.Instance.EventShowHivePanel += ShowHivePanel;
            UIManager_Master.Instance.EventHideHivePanel += HideHivePanel;
        }

        private void AddListeners()
        {
            _hideSecondPanelButton.onClick.RemoveAllListeners();
            _hideSecondPanelButton.onClick.AddListener(SlideHide);

            _collectHoneyButton.onClick.RemoveAllListeners();
            _collectHoneyButton.onClick.AddListener(CollectHoney);
        }
  
        private void ShowHivePanel(Hive hive, Action callBack = null)
        {
            if (_lockState)
            {
                return;
            }

            callBack?.Invoke();
            base.Show();
            _selectedHive = hive;
            UpdateUI();
            _selectedHive.OnValueChanged += UpdateUI;
            SlideShow();
        }

        private void HideHivePanel(Action callBack = null)
        {
            base.Hide(callBack: callBack);

            if (_selectedHive != null)
            {
                _selectedHive.OnValueChanged -= UpdateUI;
            }
        }
        
        private void SlideShow()
        {            
            StartCoroutine(Slide(_secondPanel, SlideDirectionName.Vertical,- _slideStep, _slidePixel)); 
        }

        private void SlideHide()
        {
            if (_lockState)
            {
                return;
            }
            
            StartCoroutine(Slide(_secondPanel, SlideDirectionName.Vertical, _slideStep, _slidePixel));
            HideHivePanel(); 
            CameraManager_Master.Instance.CallEventSetCameraPosition(CameraPresetName.Player);
            PlayerManager_Master.Instance.CallEventUpdateAnimationState(PlayerAnimationState.Idle);
        }

        private void UpdateUI()
        {
            if (_selectedHive == null)
            {
                return;
            }

            if (_collectProgressBar.fillAmount == 0)
            {
                _collectHoneyButton.interactable = _selectedHive.HoneyCurrentAmount <= 0 ? false : true;                             
            }             

            _honeyAmount.text = $"{_selectedHive.HoneyCurrentAmount}/{_hiveSettings.HoneyMaxAmount}";
            _beesAmount.text = $"{_selectedHive.GetActiveBeeAmount()}/{_selectedHive.BeesAmount}";
        }

        private void CollectHoney()
        {
            StartCoroutine(CollectHoneyProgress());
        }

        private IEnumerator CollectHoneyProgress()
        {
            PlayerManager_Master.Instance.CallEventUpdateAnimationState(PlayerAnimationState.Interact);
            _collectProgressBar.fillAmount = 0f;
            _collectHoneyButton.interactable = false;
            do
            {
                _collectProgressBar.fillAmount += 0.032f;
                yield return new WaitForSeconds(0.1f);

            } while (_collectProgressBar.fillAmount < 1);
                        
            PlayerManager_Master.Instance.CallEventCollectHoney(_selectedHive.GetHoney());
            _collectProgressBar.fillAmount = 0f;
            _collectHoneyButton.interactable = _selectedHive.HoneyCurrentAmount <= 0 ? false : true;
            PlayerManager_Master.Instance.CallEventUpdateAnimationState(PlayerAnimationState.Idle);
        }
    }
}