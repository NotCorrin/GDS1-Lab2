using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KoopaTroopa : Enemy
{

	public Sprite shellSprite;
	public float shellSpeed = 3;
	public float timeInShell = 5.2f;

	private bool inShell = false;
	private bool movingShell = false;

	private Sprite normalSprite;
	private float counter;
	
	private void Awake() {
		counter = timeInShell;
		normalSprite = spriteRend.sprite;
	}

	protected override void Move() {
		if (movingShell) {
			base.Move();
			return;
		}

		if (inShell) {
			// Turn into shell
			spriteRend.sprite = shellSprite;
			counter -= Time.deltaTime;
			if (counter <= 0 && movingShell == false) {
				// Return back to normal
				inShell = false;
				counter = timeInShell;
				spriteRend.sprite = normalSprite;
			}
			return;
		}
		base.Move();
	}

	protected override void OnHit() {
		FallOffScreen();
	}

	void FallOffScreen() {
		isDead = true;

		rb.velocity = Vector2.zero;
		rb.velocity = new Vector2(2.5f, 6f);

		col.enabled = false;
		spriteRend.flipX = true;

		Destroy(gameObject, 2f);
	}

	void OnCollisionEnter2D(Collision2D other) {
		if (inShell && other.transform.tag == "Player") {
			MoveShell(other.transform);
		}
		// Checks if the player is squishing the goomba
		if (other.transform.tag == "Player") {
			if (IsAbove(other.transform)) {
				inShell = true;
			} else {
				GameManager.PlayerHurt();
			}
		}

		// | ADD detection for when the block underneath gets hit
		// | If "Fireball is wrong pls change"
		// V 
		if (movingShell && other.transform.tag == "Fireball") {
			OnHit();
		}
	}

	void MoveShell(Transform player) {
		gameObject.tag = "Shell";
		movingShell = true;

		if (IsLeft(player)) {
			if (facingLeft) {
				Flip();
				moveSpeed = shellSpeed;
			} else {
				moveSpeed = shellSpeed;
			}
		} else {
			if (facingLeft) {
				moveSpeed = -shellSpeed;
			} else {
				Flip();
				moveSpeed = -shellSpeed;
			}
		}

	}

	bool IsAbove(Transform player) {
		// Check if the player above...
		if (player.position.y > transform.position.y + 0.15f &&
			player.position.x > transform.position.x - 0.5f &&
			player.position.x < transform.position.x + 0.5f) {
			return true;
		}
		return false;
	}

	bool IsLeft(Transform player) {
		if(player.transform.position.x < transform.position.x) {
			Debug.Log("LEFT");
			return true;
		}
		Debug.Log("RIGHT");
		return false;
	}
}
