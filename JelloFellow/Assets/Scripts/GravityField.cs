﻿using System.Collections.Generic;
using UnityEngine;

/// <inheritdoc/>
/// <summary>
/// Creates a gravity field around a gameobject. Used to manipulate gravity
/// of objects within the field.
/// </summary>
public class GravityField : GravityPlayer {
	private const float GravityFieldRadius = 1f;
	private CircleCollider2D gravity_field;
	private HashSet<GameObject> in_field;
	private object _lock;
	
	protected override void Awake() {
		base.Awake();
		
		in_field = new HashSet<GameObject>();
		gravity_field = gameObject.AddComponent<CircleCollider2D>();
		gravity_field.isTrigger = true;
		gravity_field.radius = GravityFieldRadius;
	}

	private void OnTriggerEnter2D(Collider2D other) {
		/* let the gravity object know its in our field */
		Gravity grav = other.gameObject.GetComponent<Gravity>();
		if (grav != null) {
			in_field.Add(other.gameObject);
			grav.SetCustomGravity(GetGravity());
			grav.InGravityField();
		}
	}

	private void OnTriggerExit2D(Collider2D other) {
		/* let the gravity object know its leaving our field */
		Gravity grav = other.gameObject.GetComponent<Gravity>();
		if (grav != null) {
			in_field.Remove(other.gameObject);
			grav.OutsideGravityField();
		}
	}

	protected override void SetGravity(Vector2 _gravity) {
		foreach (GameObject gameObj in in_field) {
			Gravity grav = gameObj.gameObject.GetComponent<Gravity>();
			grav.SetCustomGravity(_gravity);
		}
		
		base.SetGravity(_gravity);
	}

	/// <summary>
	/// Change the radius of the gravity field.
	/// </summary>
	/// <param name="radius">The radius to change the gravity field to.</param>
	public void SetFieldRadius(float radius) {
		gravity_field.radius = radius;
	}

	private void OnDrawGizmos() {
		Gizmos.color = new Color(1f, 0f, 0f, 0.2f);
		Gizmos.DrawSphere(transform.position, GravityFieldRadius);
	}
}
