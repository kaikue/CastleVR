using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordActivator : MonoBehaviour
{
    public GameObject swordPrefab;
    public Transform swordParent;

    private GameObject swordObj;

    public void Activate()
    {
        swordObj = Instantiate(swordPrefab, swordParent);
    }

    public void Deactivate()
    {
        Destroy(swordObj);
    }
}
