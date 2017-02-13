using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MyNetworkManager : MonoBehaviour {

    public bool isAtStartUp = false;

    NetworkClient myClient;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if( isAtStartUp )	return;

        //  CreateServer
        if (Input.GetKeyDown( KeyCode.Z ) )
        {
            SetUpServer();
        }

        //  CreateClient
        if( Input.GetKeyDown( KeyCode.X ) )
        {
            SetUpClient();
        }

        //  CreateLocalClient
        if( Input.GetKeyDown( KeyCode.C ) )
        {
            SetUpServer();
            SetUpLocalClient();
        }
	}

    //  GUI設定
    void    OnGUI()
    {
        if( isAtStartUp )   return;

        GUI.Label( new Rect( 2, 10, 150, 100 ), "Press Z server" );
        GUI.Label( new Rect( 2, 30, 150, 100 ), "Press X client" );
        GUI.Label( new Rect( 2, 50, 150, 100 ), "Press C localClient" );
    }

    //--------------------------------------------------------------------------------
    //  NetworkSetup
    //--------------------------------------------------------------------------------

    //  CreateServer
    public void SetUpServer()
    {
        NetworkServer.Listen( 9000 );
        isAtStartUp = true;
    }

    //  CreateClient
    public void SetUpClient()
    {
        myClient = new NetworkClient();
        myClient.RegisterHandler( MsgType.Connect, OnConnected );
        myClient.Connect( "127.0.0.1", 9000 );
        isAtStartUp = true;
    }

    //  LocalClient
    public void SetUpLocalClient()
    {
        myClient = ClientScene.ConnectLocalServer();
        myClient.RegisterHandler( MsgType.Connect, OnConnected );
        isAtStartUp = true;
    }

    //--------------------------------------------------------------------------------
    //  NetworkMessage
    //--------------------------------------------------------------------------------

    //  Message
    public void OnConnected( NetworkMessage netMsg )
    {
        Debug.Log( "サーバーに接続しました。" );
    }

}
