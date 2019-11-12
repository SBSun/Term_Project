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
    IDbConnection dbConnection;
    IDbCommand dbCommand;
    IDataReader dataReader;
    IDataAdapter dataAdapter;

    public static SaveLoad instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<SaveLoad>();
            }
            return m_instance;
        }
    }
    private static SaveLoad m_instance; //싱글톤이 할당될 변수

    void Start()
    {
        DBCreate();
        DBConnectionCheck();
    }

    public void DBCreate()
    {
        string filePath = string.Empty;

        filePath = Application.dataPath + "/SuperAlien.db"; //경로 설정

        if(!File.Exists(filePath))
        {
            File.Copy( Application.streamingAssetsPath + "/SuperAlien.db", filePath );
            Debug.Log("DB 카피됨");
            return;
        }
        Debug.Log("DB카피안했음");
    }

    public string GetDBFilePath()
    {
        string str = string.Empty;

        str = "URI=file:" + Application.dataPath + "/SuperAlien.db";

        return str;
    }

    public void DBConnectionCheck()
    {
        dbConnection = new SqliteConnection(GetDBFilePath());
        dbConnection.Open();

        if (dbConnection.State == ConnectionState.Open)
            Debug.Log("DB 연결 성공");
        else
            Debug.Log("DB 연결 실패");
    }

    public void DBRead(string _query = "Select * from TEST")
    {
        dbConnection = new SqliteConnection(GetDBFilePath());
        dbConnection.Open();

        dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandText = _query;

        dataReader = dbCommand.ExecuteReader();

        while (dataReader.Read())
        {
            Debug.Log(dataReader.GetInt32(0));
            GameManager.instance.player.curLife = dataReader.GetInt32(0);
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
        dbConnection = new SqliteConnection(GetDBFilePath());
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
        dbConnection = new SqliteConnection(GetDBFilePath());
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

    //DataSet을 사용하려면 using System.Data;추가
    public DataSet DBReadByAdapter(string _query)
    {
        DataSet ds = new DataSet();

        dbConnection = new SqliteConnection(GetDBFilePath());
        dbConnection.Open();

        var adpt = new SqliteDataAdapter(_query, GetDBFilePath());
        adpt.Fill(ds);

        dbConnection.Dispose();
        dbConnection = null;

        return ds;
    }
}
