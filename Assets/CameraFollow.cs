using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    private Vector3 curPos => target.position;
    public Vector3 lastPos;
    private Vector3 cameraOffset = new Vector3(0,0,-10);
    public float cutOff;
    public float speed;
    public float startx, endx;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(curPos.x > transform.position.x) transform.position = curPos + cameraOffset;
        else if (curPos.x > transform.position.x - cutOff && curPos.x > lastPos.x) transform.position += Vector3.right * speed * 0.5f * Time.deltaTime;
        transform.position = Vector3.right * Mathf.Clamp(transform.position.x, startx, endx) + cameraOffset;
        lastPos = curPos;
    }
}
