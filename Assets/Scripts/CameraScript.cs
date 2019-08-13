using UnityEngine;


public class CameraScript : MonoBehaviour
{
    
    // Load targets in inspector
    [SerializeField] private Transform Target1;
    [SerializeField] private Transform Target2;


    private Camera mainCam;
    private Vector3 velocity = Vector3.zero;
    private float DampTime = 0.15f;
    

    private void Start()
    {
        mainCam = GetComponent<Camera>();
    }


    private void Update()
    {
        // If there is a target, smoothly follow this target
        if (Target1 != null && Target2 != null)
        {
            Vector3 midPos = ((Target1.position + Target2.position) / 2f);
            Vector3 point = mainCam.WorldToViewportPoint(midPos);
            Vector3 delta = midPos - mainCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z));
            Vector3 destination = transform.position + delta;
            transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, DampTime);
        }
    }


}