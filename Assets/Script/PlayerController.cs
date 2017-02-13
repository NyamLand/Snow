using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private CharacterController charaController;
    private Vector3 velocity;
    private Vector3 viewForword;
    private Vector3 move;
    private const float JAMP_POWER = 10.0f;    //  跳躍力
    public float moveSpeed = 5.0f;      //  移動速度

	// Use this for initialization
	void Start ()
    {
        charaController = GetComponent<CharacterController>();
        velocity = Vector3.zero;
        move = Vector3.zero;
        viewForword = Vector3.zero;
	}
	
	// Update is called once per frame
	void Update ()
    {
        //  ジャンプ
        Jump();

        //  重力計算
        CulcGravity();

        //  移動
        Move();
    }

    //  移動
    private void Move()
    {
        //  カメラの前方取得
        viewForword = 
            Vector3.Scale( Camera.main.transform.forward, new Vector3( 1.0f, 0.0f, 1.0f ) ).normalized;

        //  移動値計算
        move = viewForword * Input.GetAxis( "Vertical" ) + Camera.main.transform.right * Input.GetAxis( "Horizontal" );

        //  移動速度
        move *= moveSpeed;

        //  移動値反映
        charaController.Move( move * Time.deltaTime );  //  キャラ移動
    }
    
    //  重力計算
    private void CulcGravity()
    {      
        //  重力をy方向の速さに足していく
        velocity.y += Physics.gravity.y * Time.deltaTime;
        
        //  １秒間あたりの移動距離を考慮して移動
        charaController.Move( velocity * Time.deltaTime );
    }

    //  ジャンプ
    private void Jump()
    {
       if( Input.GetKeyDown( KeyCode.Space ) )
       {
           velocity.y = JAMP_POWER;
       }
    }
}
