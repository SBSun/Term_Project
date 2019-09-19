using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Data;
using Mono.Data.SqliteClient;
using System.IO;
using UnityEngine.Networking;

public class PlayerInformation
{
    public int currentHp;
}

public class SaveLoad : MonoBehaviour
{
    IDbConnection       dbConnection;
    IDbCommand          dbCommand;
    IDataReader         dataReader;

    void Start()
    {
        DBCreate();
        DBConnectionCheck();
    }

    public void DBCreate()
    {
        string filePath = string.Empty;

        filePath = Application.dataPath + "/TEST.db"; //경로 설정

        if(!File.Exists(filePath))
        {
            File.Copy( Application.streamingAssetsPath + "/TEST.db", filePath );
        }

        Debug.Log( "DB 생성 완료" );
    }

    public string GetDBFilePath()
    {
        string str = string.Empty;

        str = "URI=file:" + Application.dataPath + "/TEST.db";

        return str;
    }
    
    public void DBConnectionCheck()
    {
        dbConnection = new SqliteConnection( GetDBFilePath ());
        dbConnection.Open();

        if (dbConnection.State == ConnectionState.Open)
            Debug.Log( "DB 연결 성공" );
        else
            Debug.Log( "DB 연결 실패" );
    }

    public void DBRead(string _query = "Select * from TEST")
    {
        dbConnection = new SqliteConnection( GetDBFilePath() );
        dbConnection.Open();

        dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandText = _query;

        dataReader = dbCommand.ExecuteReader();

        while (dataReader.Read())
        {
            Debug.Log( dataReader.GetInt32( 0 ) );
            GameManager.instance.player.curLife = dataReader.GetInt32( 0 );
        }
        
        
        dataReader.Dispose();
        dataReader = null;

        dbCommand.Dispose();
        dbCommand = null;

        dbConnection.Dispose();
        dbConnection = null;
    }

    public void DBInsert(string _quary)
    {
        dbConnection = new SqliteConnection( GetDBFilePath());
        dbConnection.Open();

        dbCommand = dbConnection.CreateCommand();

        dbCommand.CommandText = _quary;
        dbCommand.ExecuteNonQuery();

        dbCommand.Dispose();
        dbCommand = null;

        dbConnection.Dispose();
        dbConnection = null;
    }

    public void DBUpdate(string _quary)
    {
        dbConnection = new SqliteConnection( GetDBFilePath() );
        dbConnection.Open();

        dbCommand = dbConnection.CreateCommand();

        dbCommand.CommandText = _quary;

        dbCommand.ExecuteNonQuery();

        dbCommand.Dispose();
        dbCommand = null;

        dbConnection.Dispose();
        dbConnection = null;

        DBRead();
    }
}
