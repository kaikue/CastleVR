using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class HandLauncher : MonoBehaviour
{
    public SteamVR_Action_Boolean launchAction;

    public Hand hand;

    public GameObject prefabToLaunch;

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
        Vector3 clickPosition = -Vector3.one;
        Ray ray = Camera.main.ScreenPointToRay(Input.handPosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100f, clickMask))
        {
            clickPosition = hit.point;
        }
        GameObject targetPoint = Instantiate(trajectoryPointPrefab, clickPosition, transform.rotation);
        target = targetPoint.GetComponent<Transform>();
        print(clickPosition);
    }
}
