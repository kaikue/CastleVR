using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiveTarget : MonoBehaviour
{
    /** PUBLIC MEMBERS **/
    [Tooltip("The position at which to place the player upon diving.  Should include position and rotation")]
    public Transform divePos;

    [Tooltip("The uniform scaling constant to be applied to the player upon diving")]
    public float scaleFactor = 1f;
}
