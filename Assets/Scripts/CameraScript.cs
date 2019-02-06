using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour
{
    public float DampTime = 0.15f;
    public Transform Target1;
    public Transform Target2;

    private Camera mainCam;
    private Vector3 velocity = Vector3.zero;

    // Use this for initialization
    void Start()
    {
        mainCam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        // If there is a target, smoothly follow this
        if (Target1 != null && Target2 != null)
        {
            Vector3 midPos = ((Target1.position+Target2.position) / 2f);
            Vector3 point = mainCam.WorldToViewportPoint(midPos);
            Vector3 delta = midPos - mainCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z));
            Vector3 destination = transform.position + delta;
            transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, DampTime);
        }
    }
}