using Sfs2X.Core;
using UnityEngine;

public class HandleRoom 
{
    public static void OnRoomJoin(BaseEvent evt)
    {
        Debug.Log("Vào room thành công!");
    }

    public static void OnRoomJoinError(BaseEvent evt)
    {
        Debug.LogWarning("Vào room không thành công!");
    }

    public static void OnUserExitRoom(BaseEvent evt)
    {
        Debug.Log("Người chơi thoát room!");
    }

    public static void OnUserEnterRoom(BaseEvent evt)
    {
        Debug.Log("Người chơi vào room!");
    }
}
