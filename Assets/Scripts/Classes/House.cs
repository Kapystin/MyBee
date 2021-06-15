using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Bee
{
    public class House : MonoBehaviour
    {
        public event Action OnValueChanged;
        public int HoneyCurrentAmount => _honeyCurrentAmount;
        [SerializeField] private Transform _destinationPoint;
        [SerializeField] private int _honeyCurrentAmount;

        private HouseSettings _houseSettings;

        private void Start()
        {
            _houseSettings = Settings.GetReference<HouseSettings>();
        }

        public void GiveHoney(int honeyAmount)
        {
            _honeyCurrentAmount += honeyAmount;
            _honeyCurrentAmount = Mathf.Clamp(_honeyCurrentAmount, 0, _houseSettings.HoneyMaxAmountCapacity);

            OnValueChanged?.Invoke(); 
        }

        public void ZeroHoneyValue()
        {
            _honeyCurrentAmount = 0;
            OnValueChanged?.Invoke(); 
        }


        private void OnMouseDown()
        {
            if (EventSystem.current.IsPointerOverGameObject() == false)
            {
                PlayerManager_Master.Instance.CallEventMoveToDestinationPoint(_destinationPoint.position, CameraManager_Master.Instance.MainCamera.transform);                
                UIManager_Master.Instance.CallEventShowHousePanel(this, ()=> 
                {
                    CameraManager_Master.Instance.CallEventSetCameraPosition(CameraPresetName.House);
                });                
            }
        }
    }
}