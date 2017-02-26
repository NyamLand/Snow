using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewController : MonoBehaviour {

    //  定数
    [SerializeField]    private Transform target;
    private float viewDist = 3.0f;
    private float spinSpeed = 1.0f;
    private float TARGET_HEIGHT = 1.5f;
    private float TARGET_DIST = 1.0f;

    Vector3 vec;
    Vector2 stick;

	// Use this for initialization
	void Start ()
    {
        target = transform.root.gameObject.transform;

        //  下を向いてしまうため
        stick.y = 0.5f;
        stick.x = -0.5f;
	}
	
	// Update is called once per frame
	void Update ()
    {
        //  入力情報取得
        stick += new Vector2( Input.GetAxis( "Horizontal2" ), Input.GetAxis( "Vertical2" ) ) * Time.deltaTime * spinSpeed;

        //  制限
        stick.y = Mathf.Clamp( stick.y, -0.3f + 0.5f, 0.3f + 0.5f );

        // 球面座標
        vec.x = Mathf.Sin( stick.y * Mathf.PI ) * Mathf.Cos( stick.x * Mathf.PI );
        vec.y = Mathf.Cos( stick.y * Mathf.PI );
        vec.z = Mathf.Sin( stick.y * Mathf.PI ) * Mathf.Sin( stick.x * Mathf.PI );

        //  正規化
        vec.Normalize();

        //  座標、注視点設定
        transform.position = ( vec * viewDist ) + ( target.position + new Vector3( 0.0f, TARGET_HEIGHT ) );
        transform.LookAt( target.position + new Vector3( 0.0f, TARGET_HEIGHT ) );
    }
}
