﻿using UnityEngine;
using System.Collections;

public class Destroy_m : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (transform.childCount == 0)
        {
            Destroy(gameObject);
        }
	}
}
