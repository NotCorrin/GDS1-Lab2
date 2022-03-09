using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipes : MonoBehaviour
{
    public GameObject image;
    public bool triggerEntered;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S) && triggerEntered == true && gameObject.tag == "Pipe1")
        {
            triggerEntered = false;

            StartCoroutine(FadeScreenPipe1());
        }
        else if (triggerEntered == true && gameObject.tag == "Pipe2")
        {
            triggerEntered = false;

            StartCoroutine(FadeScreenPipe2());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        triggerEntered = true;
    }

    IEnumerator FadeScreenPipe1()
    {
        image.GetComponent<FadeScreen>().FadeOut();

        yield return new WaitForSeconds(1.4f);

        GameObject.Find("Mario").transform.position = new Vector3(334, 13, 0);
        GameObject.Find("Mario").GetComponent<Rigidbody2D>().gravityScale = 80;

        yield return new WaitForSeconds(1);

        image.GetComponent<FadeScreen>().FadeIn();

        GameObject.Find("Mario").GetComponent<Rigidbody2D>().gravityScale = 2;
    }

    IEnumerator FadeScreenPipe2()
    {
        image.GetComponent<FadeScreen>().FadeOut();

        yield return new WaitForSeconds(1.4f);

        GameObject.Find("Mario").transform.position = new Vector3(159, 4, 0);

        yield return new WaitForSeconds(1);

        image.GetComponent<FadeScreen>().FadeIn();
    }
}
