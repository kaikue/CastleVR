/**
 * TowerObject.cs
 * Project: CastleVR
 * Author: Margot Stewart
 * 
 * The component to be attached to an interactable object to signify it as a tower game piece.  Towers should have some
 * similar behavior to a standard Throwable, but they should not be able to be thrown, and should only be able to be
 * placed down at tower nodes.
 **/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Valve.VR.InteractionSystem
{
    [RequireComponent(typeof(Interactable))]
    [RequireComponent(typeof(Rigidbody))]
    public class TowerObject : MonoBehaviour
    {
        /** Public members **/
        [EnumFlags]
        [Tooltip("The flags used to attach this object to the hand.")]
        public Hand.AttachmentFlags attachmentFlags = Hand.AttachmentFlags.ParentToHand | Hand.AttachmentFlags.DetachFromOtherHand | Hand.AttachmentFlags.TurnOnKinematic;

        [Tooltip("The local point which acts as a positional and rotational offset to use while held.")]
        public Transform attachentOffset;

        [Tooltip("The prefab to spawn at a node when this tower is placed.")]
        public GameObject towerFab;

        public bool showGrabHint;

        /** PRIVATE MEMBERS **/
        // The Interactable component attached to the object
        private Interactable interactable;
        // The tower node that the tower is currently intersecting when attached to the hand.  Operates on a first-
        // come, first-serve basis
        private TowerNode intersectedNode = null;
        // The position and rotation from when the tower object was first picked up.  If we drop it anywhere that's 
        // not a node, we restore it to this config
        private Vector3 oldPosition;
        private Quaternion oldRotation;

        /** UNITY SYSTEM ROUTINES **/

        void Awake()
        {
            // Fix the Interactable component for future use
            interactable = this.GetComponent<Interactable>();
        }

        /** INTERACTION SYSTEM ROUTINES **/

        protected virtual void OnHandHoverBegin(Hand hand)
        {
            if (showGrabHint)
            {
                hand.ShowGrabHint();
            }
        }

        protected virtual void OnHandHoverEnd(Hand hand)
        {
            hand.HideGrabHint();
        }

        protected virtual void HandHoverUpdate(Hand hand)
        {
            // Capture current grab information from the hand
            GrabTypes startingGrabType = hand.GetGrabStarting();

            // Check if we are not currently attached and are being grabbed
            if (interactable.attachedToHand == null && startingGrabType != GrabTypes.None)
            {
                // Store the position and rotation in case we need to restore it
                oldPosition = transform.position;
                oldRotation = transform.rotation;
                // Attach to the hand
                hand.AttachObject(gameObject, startingGrabType, attachmentFlags, attachentOffset);
                hand.HideGrabHint();
            }

        }

        protected virtual void HandAttachedUpdate(Hand hand)
        {
            // Check if we are trying to drop the object, only allowing this if we are hovering over a tower node
            if (hand.IsGrabEnding(this.gameObject))
            {
                if (intersectedNode != null)
                {
                    // Attempt to attach the tower at the node
                    if (intersectedNode.IsOpen())
                    {
                        hand.DetachObject(gameObject, false);
                        intersectedNode.AttachTower(this);
                    }
                }        
                else
                {
                    // Restore the tower object to its original position
                    hand.DetachObject(gameObject, false);
                    transform.SetPositionAndRotation(oldPosition, oldRotation);
                }
            }
        }


        /** PHYSICS ROUTINES **/
        
        void OnTriggerEnter(Collider col)
        {
            if (intersectedNode == null && col.CompareTag("TowerNode"))
            {
                intersectedNode = col.gameObject.GetComponent<TowerNode>();
            }
        }


        void OnTriggerExit(Collider col)
        {
            if (intersectedNode != null && col.CompareTag("TowerNode"))
            {
                if (col.gameObject.GetComponent<TowerNode>() == intersectedNode)
                {
                    intersectedNode = null;
                }
            }
        }


        /** UNIQUE ROUTINES **/

        protected void SpawnTowerComponents()
        {
            // TODO
            // We're gonna want to find some way to make this routine flexible
        }
    }
}

