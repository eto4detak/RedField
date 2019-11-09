using UnityEngine;
using System.Collections;

public class CanvasTripod : MonoBehaviour
{
    private GameObject cam;
    private float objectScale = 0.1f;
    private bool orientate = true;
    private bool scale = false;
    private Vector3 initialScale;
    // Use this for initialization
    void Start()
    {
        initialScale = transform.localScale;
        cam = Camera.main.gameObject;
    }
    void Update()
    {
        //billboarding the canvas
        if (orientate)
        {
            transform.LookAt(transform.position + cam.transform.rotation * Vector3.back, cam.transform.rotation * Vector3.up);
            this.transform.Rotate(0, 180, 0);
        }
        //making it properly scaled
        if (scale)
        {
            Plane plane = new Plane(cam.transform.forward, cam.transform.position);
            float dist = plane.GetDistanceToPoint(transform.position);
            transform.localScale = initialScale * dist * objectScale;
        }
    }
    // Update is called once per frame
    void LateUpdate()
    {


    }
}
