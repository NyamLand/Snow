using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetUI : MonoBehaviour {

    PlayerController playerController;
    RectTransform canvasRect;

	// Use this for initialization
	void Start () {
        canvasRect = GetComponent<RectTransform>();
        playerController = GameObject.Find( "Player(Clone)" ).GetComponent<PlayerController>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        canvasRect.position = RectTransformUtility.WorldToScreenPoint( Camera.main, playerController.targetPos );
	}
}
