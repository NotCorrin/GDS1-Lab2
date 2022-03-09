using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePower : MonoBehaviour
{
	public GameObject projectile;
	public Vector2 velocity;
	bool canShoot = true;
	public Vector2 offset = new Vector2(0.4f, 0.1f);
	public float cooldown = 1f;
	private bool flipped;
	[SerializeField]
	private SpriteRenderer rend;
	private float flippedModifier;


	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		flipped = rend.flipX;

		if (flipped)
		{
			flippedModifier = -1;
		}
		else flippedModifier = 1;

		if (GameManager.CurrentPlayerState == GameManager.PlayerState.fireflower)
        {
			if (Input.GetKeyDown(KeyCode.LeftShift) && canShoot)
			{
				//play fireball sound

				GameObject go = (GameObject)Instantiate(projectile, (Vector2)transform.position + offset * transform.localScale.x *flippedModifier, Quaternion.identity);

				go.GetComponent<Rigidbody2D>().velocity = new Vector2(velocity.x * transform.localScale.x * flippedModifier, velocity.y);


				StartCoroutine(CanShoot());

				//GetComponent<Animator>().SetTrigger("shoot");

			}
		}
	}


	IEnumerator CanShoot()
	{
		canShoot = false;
		yield return new WaitForSeconds(cooldown);
		canShoot = true;
	}
}