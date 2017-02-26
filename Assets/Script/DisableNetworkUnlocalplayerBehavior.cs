using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DisableNetworkUnlocalplayerBehavior : NetworkBehaviour {

    [SerializeField]
    Behaviour[] behaviors;

	// Use this for initialization
	void Start () {
		
        if( !isLocalPlayer )
        {
            foreach ( var behavior in behaviors )
            {
                behavior.enabled = false;
            }
        }
	}

    private void OnApplicationFocus( bool focus )
    {
        if( isLocalPlayer )
        {
            foreach ( var behavior in behaviors )
            {
                behavior.enabled = focus;
            }
        }
    }
}
