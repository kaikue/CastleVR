using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapDropScript : MonoBehaviour
{
    public GameObject insertBall;

    private void OnTriggerEnter(Collider other)
    {
        if (other == insertBall)
        {
            TurnOffScript();
        }
    }

    void TurnOffScript()
    {
        insertBall.GetComponent<Interactable>().enabled = false;
    }
}
