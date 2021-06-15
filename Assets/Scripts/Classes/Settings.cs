using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

namespace Bee
{    
    public class Settings : MonoBehaviour
    {
        public ScriptableObject[] _references;

        private static List<ScriptableObject> references;

        private void OnEnable()
        {
            CheckAsserts();

        }
        private void Awake()
        {
            references = _references.ToList();
        }

        private void CheckAsserts()
        {
            Assert.IsNotNull(_references, "references is not set!");
        }

        public static T GetReference<T>()
        {
            foreach (var r in references.Where(r => r.GetType() == typeof(T)))
            {
                return (T)(object)r;
            }

            return default(T);
        }
    }
}