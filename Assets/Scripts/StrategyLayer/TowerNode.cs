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

        /** PRIVATE MEMBERS **/
        // The tower object attached at this node, if there is one
        private TowerObject attachedTower = null;


        /** UNITY SYSTEM ROUTINES **/
        void Awake()
        {
            GetComponent<Renderer>().material = openMat;
        }


        /** PHYSICS ROUTINES **/
        void OnTriggerEnter(Collider col)
        {
            if (col.gameObject.CompareTag("TowerObject"))
            {
                if (attachedTower == null)
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
        

        public bool AttachTower(TowerObject tower)
        {
            // If we already have a tower here reject the attempt
            // TODO: Probably make it so we can switch out the tower in hand with the tower at the node
            if (attachedTower != null) return false;

            // Fix the tower to the position and rotation of the node
            Vector3 newPos = new Vector3(transform.position.x, tower.transform.position.y, transform.position.z);
            tower.transform.SetPositionAndRotation(newPos, transform.rotation);
            tower.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;

            // Change the material to indicate that this position is now closed
            GetComponent<Renderer>().material = closedMat;

            // Store a reference to the attached tower
            attachedTower = tower;

            return true;
        }


        public bool RemoveTower()
        {
            // We can't do anything if there's nothing to remove
            if (attachedTower == null) return false;

            // Free the position and rotation of the tower
            attachedTower.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;

            // Change the material to indicate that this position is now open
            GetComponent<Renderer>().material = openHoverMat;

            // Clear the reference to the attached tower;
            attachedTower = null;

            return true;
        }
    }
}

