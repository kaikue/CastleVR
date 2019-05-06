using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Valve.VR.InteractionSystem
{
    public class DiveAgent : MonoBehaviour
    {
        /** PUBLIC MEMBERS **/
        [Tooltip("The player component this component is associated with")]
        public Player player;

        [Tooltip("The gameobject to which transforms will be applied")]
        public GameObject playerRoot;

        [Tooltip("The target this agent should dive out to by default")]
        public DiveTarget diveOutTarget;

        [Header("Input")]
        [Tooltip("The input action to initiate dive-in attempts")]
        public SteamVR_Action_Boolean diveInAction = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("DiveIn");

        [Tooltip("The input action to initiate dive-in attempts")]
        public SteamVR_Action_Boolean diveOutAction = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("DiveOut");

        [Tooltip("The left hand of the agent")]
        public Hand leftHand;

        [Tooltip("The right hand of the agent")]
        public Hand rightHand;

        [Tooltip("The fallback hand to be used when there is no VR setup")]
        public Hand fallBackHand;

        /** PRIVATE MEMBERS **/
        // The dive target the agent is currently tied to
        private DiveTarget currentDT;
        // The dive target the agent is currently diving to
        private DiveTarget nextDT;
        // Whether or not the agent is currently diving
        private bool diving = false;
        // The position where a dive was initiated from (and thus where we should dive out to)
        private Vector3 diveOutPos;

        private Quaternion diveOutRot;

        // Parameters to control the fade effect
        private float currentFadeTime = 0.5f;


        /** UNITY SYSTEM ROUTINES **/
        void Update()
        {
            bool diveIn = (leftHand != null && diveInAction.GetStateDown(leftHand.handType)) ||
                          (rightHand != null && diveInAction.GetStateDown(rightHand.handType)) ||
                          Input.GetKeyDown(KeyCode.E) &&
                          !diving && currentDT == null;
            if (diveIn)
            {
                print("Yeet me");
                TowerNode tn = null;
				Hand divingHand = rightHand;
				if (leftHand.hoveringInteractable != null &&
                    leftHand.hoveringInteractable.GetComponent<TowerNode>() != null)
                {
                    tn = leftHand.hoveringInteractable.GetComponent<TowerNode>();
					divingHand = leftHand;
                }
                else if (rightHand.hoveringInteractable != null &&
                         rightHand.hoveringInteractable.GetComponent<TowerNode>() != null)
                {
                    tn = rightHand.hoveringInteractable.GetComponent<TowerNode>();
					divingHand = rightHand;
				}
                else if (fallBackHand != null &&
                         fallBackHand.hoveringInteractable != null &&
                         fallBackHand.hoveringInteractable.GetComponent<TowerNode>() != null)
                {
                    tn = fallBackHand.hoveringInteractable.GetComponent<TowerNode>();
					divingHand = fallBackHand;
				}

                if (tn != null)
                {
                    print("Yeet you");
                }

                if (tn != null && tn.GetDiveTarget() != null)
                {
                    print("Yeet us");
                    TryDiveIn(tn.GetDiveTarget(), divingHand);
                }
            }

            bool diveOut = (leftHand != null && diveOutAction.GetStateDown(leftHand.handType)) ||
                           (rightHand != null && diveOutAction.GetStateDown(rightHand.handType)) ||
                           Input.GetKeyDown(KeyCode.E) &&
                           !diving && currentDT != null;

            if (diveOut)
            {
                print("Yeet them");
                TryDiveOut();
            }
        }


        /** STEAMVR SYSTEM ROUTINES **/


        /** UNIQUE ROUTINES **/
        public bool TryDiveIn(DiveTarget dt, Hand divingHand)
        {
            // Prevent aimless and redundant dives
            if (dt == null || currentDT != null || diving) return false;

            diving = true;
            nextDT = dt;
            InitiateDiveInFade(divingHand);
            return true;
        }

        private void InitiateDiveInFade(Hand divingHand)
        {
            SteamVR_Fade.Start(Color.clear, 0);
            SteamVR_Fade.Start(Color.black, currentFadeTime);

			StartCoroutine(DiveIn(divingHand, currentFadeTime));
        }

        private IEnumerator DiveIn(Hand divingHand, float fadeTime)
        {
			yield return new WaitForSeconds(fadeTime);

            // Fade the screen back in
            SteamVR_Fade.Start(Color.clear, currentFadeTime);

            // Fix the current position
            diveOutPos = player.trackingOriginTransform.position;
            diveOutRot = player.trackingOriginTransform.rotation;

            // Scale down the agent using the inverse of the scaling factor
            player.transform.localScale *= (1f / nextDT.scaleFactor);

            // Update the position of the agent
            //Vector3 feetOffset = player.trackingOriginTransform.position - player.feetPositionGuess;
            player.trackingOriginTransform.position = nextDT.divePos.position;// + feetOffset;

            //update the rotation to match divePos
            player.trackingOriginTransform.rotation = nextDT.divePos.rotation;

            // Spawn the sword if necessary
            if (nextDT.swordTarget)
            {
                divingHand.Activate_sword();
            }
            //Activate the bow if necessary
            if (nextDT.archeryTarget)
            {
                nextDT.ActivateBow();
            }

            // Finish the dive process
            currentDT = nextDT;
            nextDT = null;
            diving = false;
            //Start Battle Music
            SoundManagerScript.S.StopPlayroomSound();
            SoundManagerScript.S.MakeBattleSound();
        }

        public bool TryDiveOut()
        {
            // Prevent invalid dive-outs
            if (currentDT == null || diving) return false;

            diving = true;
            InitiateDiveOutFade();
            return true;
        }

        private void InitiateDiveOutFade()
        {
            SteamVR_Fade.Start(Color.clear, 0);
            SteamVR_Fade.Start(Color.black, currentFadeTime);
            print("F A D E B O I S");

            Invoke("DiveOut", currentFadeTime);
        }

        private void DiveOut()
        {
            // Fade the screen back in
            SteamVR_Fade.Start(Color.clear, currentFadeTime);

            // Scale up the agent using the scaling factor
            playerRoot.transform.localScale *= currentDT.scaleFactor;

            // Update the position of the agent
            //Vector3 feetOffset = player.trackingOriginTransform.position - player.feetPositionGuess;
            player.trackingOriginTransform.position = diveOutPos; //+ feetOffset;
            player.trackingOriginTransform.rotation = diveOutRot;

            // Free the agent's hands
            leftHand.Deactivate_sword();
            rightHand.Deactivate_sword();
            leftHand.DetachObject(leftHand.currentAttachedObject);
            rightHand.DetachObject(rightHand.currentAttachedObject);

            currentDT.DeactivateBow();

            // Finish the dive process
            currentDT = null;
            diving = false;
            //Make Playroom Sound play
            SoundManagerScript.S.MakePlayroomSound();
            SoundManagerScript.S.StopBattleSound();
        }

        public bool isDived()
        {
            return currentDT != null || diving;
        }

    }
}


