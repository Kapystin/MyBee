using System.Collections;
using UnityEngine;

namespace Bee
{
    public class LevelManager_StartLevel : MonoBehaviour
    {
        private void OnEnable()
        {
            LevelManager_Master.Instance.EventStarLevel += StartLevel;
        }

        private void OnDisable()
        {
            LevelManager_Master.Instance.EventStarLevel -= StartLevel;
        }

        private void StartLevel()
        {            
            CameraManager_Master.Instance.CallEventSetCameraPosition(CameraPresetName.Player); 
        }       
    }
}