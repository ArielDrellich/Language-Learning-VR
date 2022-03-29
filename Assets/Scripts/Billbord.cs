using UnityEngine;

public class Billbord : MonoBehaviour
{
    Vector3 cameraForward;
    void LateUpdate()
    {
        cameraForward = Camera.main.transform.forward;

        // keeps object looking at camera
        this.transform.forward = new Vector3(cameraForward.x, 0, cameraForward.z);
        // this.transform.forward = new Vector3(cameraForward.x, transform.forward.y, cameraForward.z);
    }
}
