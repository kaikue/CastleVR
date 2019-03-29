using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

namespace Vale.VR.InteractionSystem
{
    public class LeverLaunch : MonoBehaviour
    {
        public LinearMapping linearMapping;

        private float currentLinearMapping;

        // Update is called once per frame
        void Update()
        {
            if (currentLinearMapping != linearMapping.value)
            {
                currentLinearMapping = linearMapping.value;
                Debug.Log(currentLinearMapping);
            }
        }
    }
}
