using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarBlock : Block
{
    [SerializeField] GameObject star;
    [SerializeField] GameObject starToSpawn;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    protected override void ActivateBlock()
    {
        MakeItemRise(star.GetComponent<Animator>());
        StartCoroutine(SpawnNewStar());

        IsActive = false;
    }

    protected void MakeItemRise(Animator animator)
    {
        animator.SetTrigger("rise");
    }

    protected override bool IsValid()
    {
        return IsActive;
    }

    IEnumerator SpawnNewStar()
	{
        yield return new WaitForSeconds(1);
        Instantiate(starToSpawn, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), Quaternion.Euler(Vector3.zero));
	}

    // Update is called once per frame
}
