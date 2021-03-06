﻿using UnityEngine;
using System.Collections;

public class SpriteHoverScript : MonoBehaviour {

	public AudioClip hoverSound, clickSound;

	public Sprite before;
	public Sprite after;
	
	public bool useScale = false;
	public float scaleAfter = 1.1f;
	
	public bool useMouseDown = false;
	public MonoBehaviour script;
	
	private SpriteRenderer spriteRenderer;
	private float scaleBefore;
	
	private AudioSource audioSource;

	// Use this for initialization
	void Start () {
		spriteRenderer = GetComponent<SpriteRenderer>();
		scaleBefore = gameObject.transform.localScale.x;
		
		audioSource = GameObject.Find("SFX").GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnMouseEnter () {
		if (useScale)
			gameObject.transform.localScale = new Vector3(scaleAfter, scaleAfter, scaleAfter);
		spriteRenderer.sprite = after;
		
		if (audioSource) audioSource.PlayOneShot(hoverSound);
	}
	
	void OnMouseExit () {
		if (useScale)
			gameObject.transform.localScale = new Vector3(scaleBefore, scaleBefore, scaleBefore);
		spriteRenderer.sprite = before;
	}
	
	void OnMouseDown () {
		if (useMouseDown)
			script.SendMessage("OnClick", gameObject.name);
	}
}
