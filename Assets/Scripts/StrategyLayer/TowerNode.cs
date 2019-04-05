/**
 * TowerNode.cs
 * Project: CastleVR
 * Author: Margot Stewart
 * 
 * A component for a non-interactable site in the world at which a tower can be placed.  Holds one tower at a time and
 * spawns its corresponding prefab into the world.
 **/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Valve.VR.InteractionSystem
{
    [RequireComponent(typeof(Interactable))]
    public class TowerNode : MonoBehaviour
    {
        /** PUBLIC MEMBERS **/
        // The various materials to set for the different states of the node
        public Material openMat; // When there is no tower at the node
        public Material openHoverMat; // When there is no tower and a tower is held over the node
        public Material closedMat; // When there is a tower at the node
        public Material closedHoverMat; // When there is a tower and a tower is held over the node

        [Tooltip("The dive agent to respond to commands from")]
        public DiveAgent da;

        [Tooltip("Whether or not this node should allow towers to be swapped out between the node and the hand")]
        public bool allowsSwapping = true;

        [Tooltip("The interface text indicating this node")]
        public Text hudText;

        /** PRIVATE MEMBERS **/
        // The tower object attached at this node, if there is one
        private TowerObject attachedTower = null;
        // Stored attributes of the tower rigidbody
        private bool towerRigidWasColliding;
        private bool towerRigidWasKinematic;


        /** UNITY SYSTEM ROUTINES **/
        void Awake()
        {
            GetComponent<Renderer>().material = openMat;
        }


        /** STEAMVR SYSTEM ROUTINES **/
        protected virtual void HandHoverUpdate(Hand hand)
        {
            // We want to handle potentially picking up a tower that has been placed at this node
            // Capture current grab information from the hand
            GrabTypes startingGrabType = hand.GetGrabStarting();

            // If the hand is grabbing and there is a tower here to be grabbed
            if (attachedTower != null && startingGrabType != GrabTypes.None && !da.isDived())
            {
                // Remove the tower and attach it to the hand
                TowerObject temp = attachedTower;
                RemoveTower();
                hand.AttachObject(temp.gameObject, startingGrabType, temp.attachmentFlags, temp.attachentOffset);
                hand.HideGrabHint();
            }
        }

        /** PHYSICS ROUTINES **/
        void OnTriggerEnter(Collider col)
        {
            if (col.gameObject.CompareTag("TowerObject"))
            {
                if (attachedTower == null || allowsSwapping)
                {
                    GetComponent<Renderer>().material = openHoverMat;
                } 
                else
                {
                    GetComponent<Renderer>().material = closedHoverMat;
                }
            }
        }

        void OnTriggerExit(Collider col)
        {
            if (col.gameObject.CompareTag("TowerObject"))
            {
                if (attachedTower == null)
                {
                    GetComponent<Renderer>().material = openMat;
                }
                else
                {
                    GetComponent<Renderer>().material = closedMat;
                }
            }
        }


        /** UNIQUE ROUTINES **/
        public bool IsOpen()
        {
            return attachedTower == null;
        }
        

        public DiveTarget GetDiveTarget()
        {
            if (attachedTower != null)
            {
                return attachedTower.GetDiveTarget();
            } 
            else
            {
                return null;
            }
        }


        public bool TryAttachTower(TowerObject tower, Vector3 positionOffset, Vector3 rotationOffset, Hand hand)
        {
            TowerObject temp = attachedTower;
            if (temp != null)
            {
                if (allowsSwapping)
                {
                    RemoveTower();
                    hand.AttachObject(temp.gameObject, GrabTypes.Grip, temp.attachmentFlags, temp.attachentOffset);
                    AttachTower(tower, positionOffset, rotationOffset);

                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                AttachTower(tower, positionOffset, rotationOffset);

                return true;
            }
        }


        public void AttachTower(TowerObject tower, Vector3 positionOFfset, Vector3 rotationOffset)
        {
            // Fix the tower to the position and rotation of the node
            Vector3 newPos = transform.position + positionOFfset;
            Quaternion newRot = transform.rotation * Quaternion.Euler(rotationOffset);
            tower.transform.SetPositionAndRotation(newPos, newRot);
            towerRigidWasColliding = tower.GetComponent<Rigidbody>().detectCollisions;
            towerRigidWasKinematic = tower.GetComponent<Rigidbody>().isKinematic;
            tower.GetComponent<Rigidbody>().detectCollisions = false;
            tower.GetComponent<Rigidbody>().isKinematic = true;

            // Change the material to indicate that this position is now closed
            GetComponent<Renderer>().material = closedMat;

            // Set the HUD text
            if (hudText != null)
            {
                hudText.text = tower.towerName;
            }

            // Store a reference to the attached tower
            attachedTower = tower;

            tower.SpawnTowerComponents();
            tower.HideTowerObject();
        }


        public bool RemoveTower()
        {
            // We can't do anything if there's nothing to remove
            if (attachedTower == null) return false;

            // Free the position and rotation of the tower
            attachedTower.GetComponent<Rigidbody>().detectCollisions = towerRigidWasColliding;
            attachedTower.GetComponent<Rigidbody>().isKinematic = towerRigidWasKinematic;

            // Change the material to indicate that this position is now open
            GetComponent<Renderer>().material = openHoverMat;

            // Clear the HUD text
            if (hudText != null)
            {
                hudText.text = "";
            }

            // Restore the tower object and destroy the tower dive components
            attachedTower.DestroyTowerComponents();
            attachedTower.ShowTowerObject();

            // Clear the reference to the attached tower;
            attachedTower = null;

            return true;
        }
    }
}

