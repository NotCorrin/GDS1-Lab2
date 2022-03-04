using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Alpha1)) transform.position += Vector3.left * 3 * Time.deltaTime;
        if(Input.GetKey(KeyCode.Alpha2)) transform.position += Vector3.right * 3 * Time.deltaTime;
    }
}
