using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public static class PlayerData{

    private static JsonModel master;

    public static void Load()
    {
        TextAsset masterJson = Resources.Load<TextAsset>("Master/Player");
        master = JsonUtility.FromJson<JsonModel>(masterJson.ToString());
        Debug.Log(String.Format("Load Success Player Data : {0}",master.list[0].name));
    }    

    public static UserModel GetRandom()
    {
        return master.list[Random.Range(0, master.list.Length-1)];
    }
    
    class JsonModel
    {
        public UserModel[] list;
    }
}
