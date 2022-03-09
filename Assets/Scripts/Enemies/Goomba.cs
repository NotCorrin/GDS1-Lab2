using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goomba : Enemy
{
	public Sprite squishedSprite;

	private bool squish = false;

	protected override void OnHit() {
		// ADD SCORE HERE

		if (squish) {
			Squish();
			return;
		}
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

	void Squish() {
		spriteRend.sprite = squishedSprite;
		
		isDead = true;
		rb.velocity = Vector2.zero;

		Destroy(gameObject, 1f);
	}

	void OnCollisionEnter2D(Collision2D other) {
		// Checks if the player is squishing the goomba
		if (other.transform.tag == "Player") {
			if (IsAbove(other.transform)) {
				OnHit();
			} else {
				GameManager.PlayerHurt();
			}
		}

		// | ADD detection for when the block underneath gets hit
		// | If "Fireball is wrong pls change"
		// V 
		if (other.transform.tag == "Fireball" || other.transform.tag == "Shell") {
			OnHit();
		}
	}

	bool IsAbove(Transform player) {
		// Check if the player above...
		if(player.position.y > transform.position.y + 0.4f && 
			player.position.x > transform.position.x - 0.5f &&
			player.position.x < transform.position.x + 0.5f) {
			squish = true;
			return true;
		}
		return false;
	}
}
