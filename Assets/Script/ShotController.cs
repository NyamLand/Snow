using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotController : MonoBehaviour {

    //  定数
    private const float SHOT_POWER = 30.0f;
    private const float SHOT_INTERVAL = 0.5f;

    //  コンポーネント
    private PlayerController playerController;
    private GameObject bulletPrefab;

    //  変数
    private float timer = 0.0f;
    private bool shotState = false;

	// Use this for initialization
	void Start ()
    {
        playerController = transform.root.gameObject.GetComponent<PlayerController>();
        bulletPrefab = ( GameObject )Resources.Load( "Bullet" );
    }
	
	// Update is called once per frame
	void Update () {
        Shot();
	}

    private void Shot()
    {
        if( Input.GetKeyDown( KeyCode.Joystick1Button5 ) )
        {
            //  フラグを戻す
            shotState = true;
            timer = 0.0f;
        }

        if( Input.GetKey( KeyCode.Joystick1Button5 ) )
        {
            if( shotState )
            {
                //  発射
                GameObject bullet = Instantiate( bulletPrefab, transform.position, Quaternion.identity );
                bullet.transform.GetComponent<Rigidbody>().velocity =
                    ( playerController.targetPos - transform.position ).normalized * SHOT_POWER;

                //  フラグを戻す
                shotState = false;
                timer = 0.0f;
            }
            else
            {
                ShotIntervalUpdate();
            }
        }
    }

    private void ShotIntervalUpdate()
    {
        //  タイマー加算
        timer += Time.deltaTime;

        //  射撃可能
        if ( timer >= SHOT_INTERVAL )
        {
            shotState = true;
        }
    }
}
