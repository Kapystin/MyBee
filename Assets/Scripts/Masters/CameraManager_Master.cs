using System;
using UnityEngine;

namespace Bee
{
    public class CameraManager_Master : MasterSingleton<CameraManager_Master>
    {
        public Camera MainCamera;
        public event Action<CameraPresetName, Transform> EventSetCameraPosition;

        public void CallEventSetCameraPosition(CameraPresetName cameraPresetName, Transform target = null)
        {
            EventSetCameraPosition?.Invoke(cameraPresetName, target);
        }
    }
}