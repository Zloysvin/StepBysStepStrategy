
using UnityEngine;

public class SmoothCameraMovement : MonoBehaviour
{
    public GameObject player;
    public Vector3 offset;
    public float smoothnessScale = 0.2f;
    
    Vector3 velocity = Vector3.one;

    void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            offset.y += 0.5f;
            
            
            if (offset.y <= 9.38f)
            {
                offset.z -= 0.7f;
            }
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0 && offset.z <= -2f)
        {
            offset.y -= 0.5f;
            
            if (offset.y <= 9.38f)
            {
                offset.z += 0.7f;
            }
        }
    }

    void LateUpdate()
    {
        Vector3 newCameraPos = player.transform.position + offset;
        Vector3 smoothedCameraPos = Vector3.SmoothDamp(transform.position, newCameraPos, ref velocity, smoothnessScale);

        transform.position = smoothedCameraPos;
    }
}
