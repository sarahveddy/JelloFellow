﻿using UnityEngine;

public abstract class Gravity : MonoBehaviour {
  private float gravity_force;

  protected virtual void Awake() {
    MainScript main_instance = MainScript.instance;
    /* get the initial gravity */
    gravity_force = main_instance.GravityForce();
    /* subscribe to force changes */
    main_instance.OnGravityForceChange += UpdateGravityForce;
  }

  private void UpdateGravityForce(float _gravity_force) {
    gravity_force = _gravity_force;
  }

  /* Force of the gravity to apply in which ever direction */
  protected float GravityForce() {
    return gravity_force;
  }

  /* Default gravity set when not in gravitation field (direction of gravity) */
  protected Vector2 DefaultGravity() {
    return new Vector2(0f, -1f);
  }

  /// <summary>
  /// Set custom gravity to the object.
  /// </summary>
  /// <param name="_custom_gravity">Gravity to effect the object.</param>
  public abstract void SetCustomGravity(Vector2 _custom_gravity);

  /// <summary>
  /// This object is in a gravity field.
  /// </summary>
  public abstract void InGravityField();

  /// <summary>
  /// This object has just left a gravity field.
  /// </summary>
  public abstract void OutsideGravityField();

  /// <summary>
  /// Light restrictions when entering a light.
  /// The restrictions vector is multiplied to the gravity of the object.
  /// Restrictions (0,1) would have gravity.x be zero (removing gravity x component).
  /// </summary>
  /// <param name="_restrictions">Restrictions to apply to the gravity.</param>
  public abstract void SetGravityLightRestrictions(Vector2 _restrictions);
}