using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Isshi777
{
    public class Test : MonoBehaviour
    {
        [System.Serializable]
        class Parameter
        {
            public Sprite sprite;
            public List<Sprite> spriteList;
        }

        [SerializeField]
        private Sprite spriteA;
        [SerializeField]
        private Sprite spriteB;

        [SerializeField]
        private Sprite[] spriteList;

        [SerializeField]
        private Parameter testParam;
    }
}
