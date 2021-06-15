using System;
using System.Collections.Generic;
using UnityEngine;

namespace Bee
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private float _rotationSmooth = 3f;
        [SerializeField] private float _movementSmooth = 3f;
        [SerializeField] private List<CameraPosition> _cameraPresetPosition;
        private CameraPosition _currentCameraPosition;

        private void OnEnable()
        {
            CameraManager_Master.Instance.EventSetCameraPosition += SetCameraPosition;
        }

        private void OnDisable()
        {
            CameraManager_Master.Instance.EventSetCameraPosition -= SetCameraPosition;
        }

        private void Update()
        {             
            LookAt();
            Movement();
        }
 
        private void LookAt()
        {
            if (_currentCameraPosition == null)
            {
                return;
            }

            Quaternion startRotation = transform.rotation;

            transform.LookAt(_currentCameraPosition.Target);
            Quaternion targetRotation = transform.rotation * Quaternion.Euler(_currentCameraPosition.RotationOffset);
            //transform.rotation = Quaternion.Lerp(startRotation, transform.rotation, Time.deltaTime * _rotationSmooth);
            transform.rotation = Quaternion.Lerp(startRotation, targetRotation, .1f);
            //transform.rotation *= Quaternion.Euler(_currentCameraPosition.RotationOffset);
        }


        private void Movement()
        {
            if (_currentCameraPosition == null)
            {
                return;
            }

            Vector3 targetPosition = new Vector3(_currentCameraPosition.Target.position.x,
                                             _currentCameraPosition.Target.position.y,
                                             _currentCameraPosition.Target.position.z) + _currentCameraPosition.PositionOffset;

            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * _movementSmooth);                      
        }

        private void SetCameraPosition(CameraPresetName cameraPresetName, Transform target = null)
        {
            foreach (var preset in _cameraPresetPosition)
            {
                if (preset.PresetName == cameraPresetName)
                {
                    if (target != null)
                    {
                        preset.Target = target;
                    }
                    _currentCameraPosition = preset;                                 
                }
            }
        }         
    }     
}