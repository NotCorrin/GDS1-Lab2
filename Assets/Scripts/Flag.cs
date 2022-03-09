using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag : MonoBehaviour
{
    public float flagTop, flagBottom;
    public PlayerMove target;
    public int[] scoreValues;
    private float offset;
    public Transform child;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void Update()
    {
        if(target)
        {
            if(!target.runaway) 
            {
                target.rb.velocity = Vector2.down * 4;
                child.position = new Vector3(child.position.x, target.transform.position.y + offset);
            }
        }
    }
    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.name == "Mario")
        {
            PlayerMove pm = other.GetComponent<PlayerMove>();
            pm.isLocked = true;
            target = pm;
            offset = child.position.y - other.transform.position.y;
            GameManager.Score += scoreValues[Mathf.FloorToInt(((other.transform.position.y - flagBottom) / flagTop) * scoreValues.Length)];  
            pm.rb.velocity = Vector2.zero; 
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.DrawLine(new Vector3(transform.position.x, flagTop), new Vector3(transform.position.x, flagBottom));
    }
}
