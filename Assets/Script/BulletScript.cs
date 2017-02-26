using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {

    private float timer = 0.0f;
    private const float DELETE_TIME = 3.0f;

	// Use this for initialization
	void Start () {
        //  CollisionWallとの当たり判定を無視
        Physics.IgnoreLayerCollision(
            LayerMask.NameToLayer( "Bullet" ),
            LayerMask.NameToLayer( "Player" ) );
    }
	
	// Update is called once per frame
	void Update () {
        //  タイマー加算
        timer += Time.deltaTime;

        //  消滅
        if ( timer >= DELETE_TIME )
        {
            Destroy( gameObject );
        }
    }

    private void OnCollisionEnter( Collision collision )
    {
        Destroy( gameObject );
    }
}
