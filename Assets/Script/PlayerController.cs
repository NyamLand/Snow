using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    //  定数
    private const float RAY_DIST = 1.0f;
    private const float CHECK_GROUND_DIST = 0.5f;
    private const float JUMP_POWER = 7.0f;  //  跳躍力
    private const float SHOT_DISTANCE = 10.0f;
    private const float MOVE_SPEED = 5.0f;  //  移動速度

    //  コンポーネント
    private CharacterController characterController;

    //  変数
    [SerializeField] private bool isGround;
    [SerializeField] private bool jumpState;
    [SerializeField] private Vector3 velocity;
    public Vector3 targetPos;

	// Use this for initialization
	void Start ()
    {
        characterController = gameObject.GetComponent<CharacterController>();
        isGround = false;
        jumpState = false;
        velocity = Vector3.zero;

        GameObject canvas = GameObject.Find( "Canvas" );
        canvas.transform.FindChild( "Target" ).gameObject.SetActive( true );
	}
	
	// Update is called once per frame
	void Update ()
    {
        //  移動
        Move();

        //  ターゲット位置チェック
        CheckTargetPos();

        //  接地チェック
        CheckGroundStatus();

        //  移動値加算
        characterController.Move( velocity * Time.fixedDeltaTime );

        characterController.transform.LookAt( targetPos );
	}

    //-----------------------------------------------------------------------------
    //  プレイヤー動作
    //-----------------------------------------------------------------------------
        
    //  接地チェック
    private void CheckGroundStatus()
    {
        //Debug.DrawRay( transform.position + Vector3.up * 0.5f , Vector3.down * RAY_DIST );
    
        RaycastHit hitInfo;
        if ( Physics.Raycast( transform.position + Vector3.up * 0.5f, Vector3.down, out hitInfo, RAY_DIST ) )
        {
            if( transform.position.y - hitInfo.point.y < CHECK_GROUND_DIST )
            {
                isGround = true;
    
                //  ジャンプ
                Jump();
            }
            else
            {
                //  重力加算
                velocity.y += Physics.gravity.y * Time.fixedDeltaTime;
                isGround = false;
            }
        }
        else
        {
            //  重力加算
            velocity.y += Physics.gravity.y * Time.fixedDeltaTime;
            isGround = false;
        }
    }
    
    //  ジャンプ
    private void Jump()
    {           
        //  ジャンプ
        if ( Input.GetKeyDown( KeyCode.Joystick1Button0 ) )
        {
            velocity.y = JUMP_POWER;
            isGround = false;
        }
    }

    //  移動
    private void Move()
    {
        //  カメラの前方取得
        Vector3 viewForword = Vector3.Scale( Camera.main.transform.forward, new Vector3( 1.0f, 0.0f, 1.0f ) ).normalized;

        //  移動値計算
        Vector3 move = viewForword * Input.GetAxis( "Vertical" ) + Camera.main.transform.right * Input.GetAxis( "Horizontal" );

        //  移動速度
        move *= MOVE_SPEED;

        //  移動値反映
        characterController.Move( move * Time.fixedDeltaTime );
    }

    //-----------------------------------------------------------------------------
    //  ショット関連動作
    //-----------------------------------------------------------------------------

    //  ターゲット位置算出
    private void CheckTargetPos()
    {
        RaycastHit hitInfo;
        int layerMask = ( 1 << 9 );

        //Debug.DrawRay( Camera.main.transform. position, Camera.main.transform.forward * SHOT_DISTANCE );

        if ( Physics.Raycast( Camera.main.transform.position, Camera.main.transform.forward, out hitInfo, SHOT_DISTANCE, layerMask ) )
        {
            targetPos = hitInfo.point;
        }
        else
        {
            targetPos = Camera.main.transform.position + ( Camera.main.transform.forward * SHOT_DISTANCE );
        }
    }
}
