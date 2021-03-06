﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class ApplyDmg : MonoBehaviour {
	private void OnCollisionEnter2D(Collision2D other) {
		if (other.gameObject.CompareTag("Player")) {
			other.transform.parent.GetComponentInChildren<GenericPlayer>().Damage(1);			
		}
		//Kill enemy
		else if (other.gameObject.CompareTag("Enemy")) {
			JellySpriteReferencePoint jelly_enemy = other.gameObject.GetComponent<JellySpriteReferencePoint>();
			if (jelly_enemy) {
				jelly_enemy.ParentJellySprite.gameObject.AddComponent<DeathEffect>();
			}
		}

		if (gameObject.CompareTag("SpikerProjectile")) {
			Destroy(gameObject);
		}
	}
	
	private void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.CompareTag("Player")) {
			other.transform.parent.GetComponentInChildren<GenericPlayer>().Damage(1);
		}
		//Kill enemy
		else if (other.gameObject.CompareTag("Enemy")) {
			JellySpriteReferencePoint jelly_enemy = other.gameObject.GetComponent<JellySpriteReferencePoint>();
			if (jelly_enemy) {
				jelly_enemy.ParentJellySprite.gameObject.AddComponent<DeathEffect>();
			}
		}
	}
}
