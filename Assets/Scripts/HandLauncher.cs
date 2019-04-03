using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

namespace Valve.VR.InteractionSystem
{
    public class HandLauncher : MonoBehaviour
    {
        public SteamVR_Action_Boolean launchAction;
        public Hand hand;

        public GameObject prefabToLaunch;
        private Transform target;
        public GameObject trajectoryPointPrefab;
        public LayerMask clickMask;
        public GameObject launcher;

        private void OnEnable()
        {
            if (hand == null)
                hand = this.GetComponent<Hand>();

            if (launchAction == null)
            {
            Debug.LogError("<b>[SteamVR Interaction]</b> No plant action assigned");
            return;
            }

            launchAction.AddOnChangeListener(OnLaunchActionChange, hand.handType);
        }

        private void OnDisable()
        {
            if (launchAction != null)
                launchAction.RemoveOnChangeListener(OnLaunchActionChange, hand.handType);
        }

        private void OnLaunchActionChange(SteamVR_Action_Boolean actionIn, SteamVR_Input_Sources inputSource, bool newValue)
        {
            if (newValue)
            {
                Aim();
            }
        }

        void Aim()
        {
            //launcher.GetComponent(LaunchProcess());
        }
    }
}
