using System.Collections;
using UnityEngine;
using UnityEngine.XR;

public class controllerDebug : MonoBehaviour {
    [SerializeField]
    private MeshRenderer indicator;
    [SerializeField]
    private MeshRenderer indicator2;
    [SerializeField]
    private GameObject leftHand;
    [SerializeField]
    private GameObject rightHand;
    // Update is called once per frame
    IEnumerator Start()
    {
        UnityEngine.XR.WSA.Input.InteractionManager.OnSourceDetected += InteractionManager_OnSourceDetected;
        UnityEngine.XR.WSA.Input.InteractionManager.OnSourceUpdated += InteractionManager_OnSourceUpdated;
        indicator.material.color = Color.red;
        while (true)
        {
            //Debug.Log(UnityEngine.XR.WSA.Input.InteractionManager.GetCurrentReading().Length);
            if (UnityEngine.XR.WSA.Input.InteractionManager.GetCurrentReading().Length == 1)
            {
                indicator.material.color = Color.green;
                //Debug.Log(UnityEngine.XR.WSA.Input.InteractionManager.GetCurrentReading()[0].anyPressed);
            }
            if (UnityEngine.XR.WSA.Input.InteractionManager.GetCurrentReading().Length == 2)
            {
                if (UnityEngine.XR.WSA.Input.InteractionManager.GetCurrentReading()[0].anyPressed ||
                    UnityEngine.XR.WSA.Input.InteractionManager.GetCurrentReading()[1].anyPressed)
                {
                    indicator2.material.color = Color.green;
                }
                else
                {
                    indicator2.material.color = Color.red;
                }
                indicator.material.color = Color.black;
                
                //Debug.Log(UnityEngine.XR.WSA.Input.InteractionManager.GetCurrentReading()[0].anyPressed);
            }
            //Debug.Log("Left: " + UnityEngine.XR.InputTracking.GetLocalPosition(XRNode.LeftHand));
            //Debug.Log("Right: " + InputTracking.GetLocalPosition(XRNode.RightHand));

            leftHand.transform.position = UnityEngine.XR.InputTracking.GetLocalPosition(XRNode.LeftHand);
            rightHand.transform.position = UnityEngine.XR.InputTracking.GetLocalPosition(XRNode.RightHand);

            yield return new WaitForSeconds(0.1f);
        }
    }

    private void InteractionManager_OnSourceUpdated(UnityEngine.XR.WSA.Input.SourceUpdatedEventArgs obj)
    {
        Vector3 pos;
        Quaternion rot;
        if (obj.state.properties.location.pointer.TryGetPosition(out pos))
        {
            leftHand.transform.position = pos;
        }
        if (obj.state.properties.location.pointer.TryGetRotation(out rot))
        {
            leftHand.transform.rotation = rot;
        }
    }

    private void InteractionManager_OnSourceDetected(UnityEngine.XR.WSA.Input.SourceDetectedEventArgs state)
    {
        Vector3 pos;
        if (state.state.properties.location.pointer.TryGetPosition(out pos))
        {
           rightHand.transform.position = pos;
        }
    }

    void Update () {
    }
}
