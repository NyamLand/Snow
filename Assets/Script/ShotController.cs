using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotController : MonoBehaviour {

    public Vector3 hitPos;
    private float rayLength = 10.0f;
    private float HEIGHT = 0.5f;
    private float SHOT_POWER = 10.0f;
    GameObject prefab;


    // Use this for initialization
    void Start ()
    {
        hitPos = Vector3.zero;
        prefab = ( GameObject )Resources.Load( "Bullet" );
	}
	
	// Update is called once per frame
	void Update ()
    {
        TargetSet();

        if( Input.GetMouseButtonDown( 0 ) )     Shot();
	}

    //  ターゲット指定
    private void TargetSet()
    {
        RaycastHit hit;
        int layerMask = ( 1 << 9 );

        if( Physics.Raycast( transform.position + new Vector3( 0.0f, HEIGHT, 0.0f ), Camera.main.transform.forward, out hit, rayLength, layerMask ) )
        {
            hitPos = hit.point;
        }
        else
        {
            hitPos = ( transform.position + new Vector3( 0.0f, HEIGHT, 0.0f ) ) + Camera.main.transform.forward * rayLength;
        }
    }

    //  撃つ
    private void Shot()
    {
        GameObject bullet = Instantiate( prefab, transform.position + new Vector3( 0.0f, HEIGHT, 0.0f ), Quaternion.identity );
        bullet.transform.GetComponent<Rigidbody>().velocity = ( hitPos - transform.position ).normalized * SHOT_POWER;
    }
}
