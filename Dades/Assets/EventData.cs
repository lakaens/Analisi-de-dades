using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventData 
{
    uint EventID;

    public string GetJSON()
    {
        string json = JsonUtility.ToJson(this);
        return json;
    }
    
}

public class KillEvent : EventData
{
    Vector3 Position;
    Vector3 EulerAngles;
   
}

public class RegisterEvent : EventData
{
    string UserName;

} 