using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    [SerializeField] private Camera camera;
    public float sensitivity = 0.5f;

    public Transform target;
    public float distance = 15f;
    public Vector2 PlayerTargetRotation = Vector2.zero;

    private float pitch, yaw;

    void Start()
    {
        camera = this.gameObject.GetComponentInChildren<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        getInput();
        Quaternion yawRotation = Quaternion.Euler(pitch, yaw, 0f);
        RotateCam(yawRotation);
    }


    public void getInput()
    {
        Vector2 input = Vector2.zero;
        if (Input.GetMouseButton(0))
        {
            input = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        }

        yaw += input.x * sensitivity * Time.deltaTime;
        pitch -= input.y * sensitivity * Time.deltaTime;
    }

    public void RotateCam(Quaternion rotation)
    {
        Vector3 offset = rotation * new Vector3(0,0, distance);
        transform.position = target.position + offset;
        transform.rotation = rotation;
    }
}
