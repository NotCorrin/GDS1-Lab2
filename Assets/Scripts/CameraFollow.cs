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
    public float startx = 0;
    public float endx = 100;
    private float midpoint => (endx-startx)/2;
    private PlayerMove player;
    // Start is called before the first frame update
    void Awake()
    {
        player = FindObjectOfType<PlayerMove>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(curPos.x > transform.position.x) transform.position = curPos + cameraOffset;
        else if (curPos.x > transform.position.x - cutOff && curPos.x > lastPos.x) transform.position += Vector3.right * player.curmaxspeed * 0.5f * Time.deltaTime;
        transform.position = Vector3.right * Mathf.Clamp(transform.position.x, startx, endx) + cameraOffset;
        lastPos = curPos;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(Vector3.right * (startx+midpoint), new Vector3(midpoint*2, 8)); //temp value 8
        Gizmos.DrawLine(new Vector3(transform.position.x, 4), new Vector3(transform.position.x, -4));
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(new Vector3(transform.position.x - cutOff, 4), new Vector3(transform.position.x - cutOff, -4));
    }
}
