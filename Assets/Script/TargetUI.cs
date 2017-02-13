using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TargetUI : MonoBehaviour {

    ShotController shotController;
    RectTransform canvasRect;

	// Use this for initialization
	void Start () {
        canvasRect = GetComponent<RectTransform>();
        shotController = GameObject.Find( "Player" ).GetComponent<ShotController>();
	}
	
	// Update is called once per frame
	void Update () {
        canvasRect.position = RectTransformUtility.WorldToScreenPoint( Camera.main, shotController.hitPos );
    }
}
