using Sfs2X.Core;
using Sfs2X.Entities;
using Sfs2X.Entities.Data;
using UnityEngine;

public class HandleRoom 
{
    public static void OnRoomJoin(BaseEvent evt)
    {
        Debug.Log("On Room Join: \n " + evt.Params["room"].ToString());
    }

    public static void OnRoomJoinError(BaseEvent evt)
    {
        Debug.LogWarning("On Room Join Error: \n" + evt.Params["room"].ToString());
    }

    public static void OnUserExitRoom(BaseEvent evt)
    {
        Debug.Log("On User Exit Room: \n" + evt.Params["room"].ToString());
        Debug.Log("On User Enter Room: \n" + evt.Params["user"].ToString());
    }

    public static void OnUserEnterRoom(BaseEvent evt)
    {
        Debug.Log("On User Enter Room: \n" + evt.Params["room"].ToString());
        Debug.Log("On User Enter Room: \n" + evt.Params["user"].ToString());
    }
}
