using UnityEngine;

public class Billboard : MonoBehaviour
{
    Vector3 cameraForward;
    const float epsilon = 0.00001f;
    void LateUpdate()
    {   
        cameraForward = Camera.main.transform.forward;
        
        // keeps object looking at camera
        this.transform.forward = new Vector3(cameraForward.x + epsilon,
                                             transform.forward.y, cameraForward.z + epsilon);
    }
}
