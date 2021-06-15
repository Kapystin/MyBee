using System;
using UnityEngine;

namespace Bee
{
    [Serializable]
    public class CameraPosition
    {
        public CameraPresetName PresetName;
        public Transform Target;
        public Vector3 PositionOffset;
        public Vector3 RotationOffset;
    }
}