using UnityEngine;


public class ParallaxBackground : MonoBehaviour
{
    private float length, startpos;

    // set and get in editor
    [SerializeField] public Camera cam;
    [SerializeField] public float parallaxEffect;


    void Start()
    {
        // get width and height of the camera
        float height = 2f * cam.orthographicSize;
        float width = height * cam.aspect;

        // get start position of background element and the total length
        startpos = transform.position.x;
        length = width;
        // get length of background element
            //GetComponent<SpriteRenderer>().bounds.size.x;
    }


    void Update()
    {
        // make a temp float to keep track of the current position and the distance float to determine how fast it should move
        float temp = (cam.transform.position.x * (1 - parallaxEffect));
        float dist = (cam.transform.position.x * parallaxEffect);

        // set new position according to the startposition, distance and the parallax effect
        transform.position = new Vector3(startpos + dist, transform.position.y, transform.position.z);

        // when the element will get out of bounds, reset position
        if (temp > startpos + length)
        {
            startpos += length;
        }
        else if (temp < startpos - length)
        {
            startpos -= length;
        }
    }


}