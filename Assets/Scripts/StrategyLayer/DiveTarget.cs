using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class DiveTarget : MonoBehaviour
{
    /** PUBLIC MEMBERS **/
    [Tooltip("The position at which to place the player upon diving.  Should include position and rotation")]
    public Transform divePos;

    [Tooltip("The uniform scaling constant to be applied to the player upon diving")]
    public float scaleFactor = 1f;

    [Tooltip("Whether or not the sword apparatus should be spawned on the agent's hands")]
    public bool swordTarget = false;

    [Tooltip("Whether or not the bow should be made interactable")]
    public bool archeryTarget = false;

    [Tooltip("If this is an archery tower, the bow object to enable/disable")]
    public GameObject bow;

    public void ActivateBow()
    {
        if (bow != null)
        {
            bow.SetActive(true);
        }
    }
    public void DeactivateBow()
    {
        if (bow != null)
        {
            bow.SetActive(false);
        }
    }
}
