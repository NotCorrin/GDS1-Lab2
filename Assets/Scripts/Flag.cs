using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag : MonoBehaviour
{
    public float flagTop, flagBottom;
    public int[] scoreValues;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.name == "Mario")
        {
            other.GetComponent<PlayerMove>().isLocked = true;
            GameManager.Score += scoreValues[Mathf.FloorToInt((other.transform.position.y - flagBottom / flagTop) * scoreValues.Length)];
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.DrawLine(Vector3.up * flagTop + transform.position, Vector3.up * flagBottom + transform.position);
    }
}
