﻿using System;
using UnityEngine;

public class EyesController : MonoBehaviour
{
    [Range(0, 1)] public float YOffsetFromCentre;
    [Range(0, 0.2f)] public float SmoothTime;
    [Range(0, 1)] public float Distance;
    private Vector2 _vel;
    private Vector2 _position;
    private GravityPlayer _player;
    //private Vector2 _gravDir = Vector2.down;

    // Update is called once per frame
    private void LateUpdate()
    {
        Input2D _input = InputController.instance.input;
        
        if (!_player)
        {
            _player = gameObject.GetComponentInParent<JellySprite>().CentralPoint.GameObject
                .GetComponent<GravityPlayer>();
        }

        var gravOpp = -_player.GetGravity().normalized;
        transform.up = gravOpp;
        
        var yOffset = YOffsetFromCentre * gameObject.GetComponentInParent<JellySprite>().m_SpriteScale.y * 2;
        var offset = gravOpp * yOffset;
        var dir = new Vector2(_input.GetHorizontalLeftStick(), _input.GetVerticalLeftStick());
        
        var targetPos = offset + dir.normalized * Distance;
        
        _position = Vector2.SmoothDamp(_position, targetPos, ref _vel, SmoothTime, Mathf.Infinity, Time.deltaTime);
        transform.localPosition = _position;

    }
}