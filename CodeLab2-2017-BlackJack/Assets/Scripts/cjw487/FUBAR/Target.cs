using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : Switcheroo
{
	// Use this for initialization
	void Start ()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _spriteColor = _renderer.color;
        ChangeType(_renderer.sprite.name);     
    }

    protected override void Update() { }
}
