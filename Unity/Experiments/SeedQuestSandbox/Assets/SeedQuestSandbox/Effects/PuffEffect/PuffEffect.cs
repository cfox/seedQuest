﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuffEffect : MonoBehaviour {

    public GameObject transformObject = null;

    private bool isNotUsed = true;
    private ParticleSystem effect;

    // Use this for initialization
    void Start () {
        effect = gameObject.GetComponentInChildren<ParticleSystem>();
        effect.Stop();
	}
	
	// Update is called once per frame
	void Update () {
		
        if(Input.GetMouseButtonDown(0) && isNotUsed) {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray, out hit, 100.0f)) {
                effect.Play();
                tranformEffect();
                transform.GetComponent<MeshRenderer>().enabled = false;
            }
        }

	}

    void tranformEffect() {
        GameObject obj = Instantiate(transformObject, transform);

    }
}
