using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBlock : Block
{
    [SerializeField] GameObject mushroom;
    [SerializeField] GameObject fireFlower;
    [SerializeField] GameObject mushroomToSpawn;
    bool PlayerIsBig;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    protected override void ActivateBlock()
    {
        if (GameManager.CurrentPlayerState != GameManager.PlayerState.normal)
        {
            Destroy(mushroom);
            MakeItemRise(fireFlower.GetComponent<Animator>());
            fireFlower.GetComponent<Collider2D>().enabled = true;
        }
        else
        {
            Destroy(fireFlower);
            MakeItemRise(mushroom.GetComponent<Animator>());
            StartCoroutine(SpawnNewMushroom());
            
        }

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

    IEnumerator SpawnNewMushroom()
	{
        yield return new WaitForSeconds(1);
        Instantiate(mushroomToSpawn, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), Quaternion.Euler(Vector3.zero));
	}

    // Update is called once per frame
}
