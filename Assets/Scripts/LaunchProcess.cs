using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Valve.VR.InteractionSystem
{
    public class LaunchProcess : MonoBehaviour
    {
        public LayerMask traceLayerMask;
        public float arcDistance = 10.0f;
        private TeleportArc teleportArc = null;

        // Start is called before the first frame update
        void Awake()
        {
            teleportArc = GetComponent<TeleportArc>();
            teleportArc.traceLayerMask = traceLayerMask;
        }

        // Update is called once per frame
        void Update()
        {
            teleportArc.Show();
        }
    }
}
