using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_barrasHP : MonoBehaviour {

    Camera camera;
	// Use this for initialization
	void Start () {
        camera = Camera.main;
	}
	
	// Update is called once per frame
	void Update () {

        transform.eulerAngles = new Vector3(-30,0,0);
		
	}
}
