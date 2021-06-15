using System;
using UnityEngine;
using UnityEngine.UI;

namespace Bee
{
    public class MenuPanelUI : UI
    {
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _continueButton;
        [SerializeField] private Button _statisticButton;
        [SerializeField] private Button _quitButton;
        

        private void Start()
        {
            _continueButton.gameObject.SetActive(false);
            _statisticButton.gameObject.SetActive(false);
            base.ResetRectPosition();
            AddListeners();
        }

        private void OnEnable()
        {
            UIManager_Master.Instance.EventShowMenuPanel += ShowMenuPanel;
            UIManager_Master.Instance.EventHideMenuPanel += HideMenuPanel;
        }

        private void OnDisable()
        {
            UIManager_Master.Instance.EventShowMenuPanel -= ShowMenuPanel;
            UIManager_Master.Instance.EventHideMenuPanel -= HideMenuPanel;
        }

        private void AddListeners()
        {
            _playButton.onClick.RemoveAllListeners();
            _playButton.onClick.AddListener(Play);

            _quitButton.onClick.RemoveAllListeners();
            _quitButton.onClick.AddListener(Quit);

            _continueButton.onClick.RemoveAllListeners();
            _continueButton.onClick.AddListener(Continue);

            _statisticButton.onClick.RemoveAllListeners();
            _statisticButton.onClick.AddListener(ShowStatistic);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                UIManager_Master.Instance.CallEventShowMenuPanel();
            }
        }

        private void Play()
        {            
            HideMenuPanel(callBack:() => 
                {
                    LevelManager_Master.Instance.CallEventStarLevel();
                    _playButton.gameObject.SetActive(false);
                    _continueButton.gameObject.SetActive(true);
                    _statisticButton.gameObject.SetActive(true);

                });
        }

        private void Continue()
        {
            HideMenuPanel();
        }

        private void ShowStatistic()
        {
            UIManager_Master.Instance.CallEventShowStatisticPanel();
            HideMenuPanel();
        }

        protected void ShowMenuPanel(Action callBack = null)
        {
            base.Show(callBack);
        }

        protected void HideMenuPanel(Action callBack = null)
        {
            base.Hide(callBack: callBack);
        }

        private void Quit()
        {
            Application.Quit();
        }
    }
}