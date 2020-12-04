using Sfs2X.Entities.Data;
using Sfs2X.Requests;
using System.Collections.Generic;
using UnityEngine;

public class RequestGame 
{
    private static string MODULE = CmdDefine.Module.MODULE_GAME;

    public static void OutRoomGame()
    {
        Debug.Log("=========================== OUT ROOM GAME");
        ISFSObject isFSObject = new SFSObject();
        isFSObject.PutInt(CmdDefine.CMD_ID, CmdDefine.CMD.OUT_ROOM_GAME);

        var packet = new ExtensionRequest(MODULE, isFSObject, SmartFoxConnection.Sfs.LastJoinedRoom);
        if (SmartFoxConnection.isAlready())
        {
            SmartFoxConnection.send(packet);
        }
        else
        {
            SmartFoxConnection.Init();
            SmartFoxConnection.send(packet);
        }
    }

    public static void UnActive(int id)
    {
        Debug.Log("=========================== UN ACTIVE");
        ISFSObject isFSObject = new SFSObject();
        isFSObject.PutInt(CmdDefine.CMD_ID, CmdDefine.CMD.UN_ACTIVE_CHAR);

        isFSObject.PutInt(CmdDefine.ModuleCharacter.ID, id);

        var packet = new ExtensionRequest(MODULE, isFSObject, SmartFoxConnection.Sfs.LastJoinedRoom);
        if (SmartFoxConnection.isAlready())
        {
            SmartFoxConnection.send(packet);
        }
        else
        {
            SmartFoxConnection.Init();
            SmartFoxConnection.send(packet);
        }
    }

    public static void Active(int id, int idx)
    {
        Debug.Log("=========================== ACTIVE");
        ISFSObject isFSObject = new SFSObject();
        isFSObject.PutInt(CmdDefine.CMD_ID, CmdDefine.CMD.ACTIVE_CHAR);

        isFSObject.PutInt(CmdDefine.ModuleCharacter.ID, id);
        isFSObject.PutInt(CmdDefine.ModuleCharacter.IDX, idx);

        var packet = new ExtensionRequest(MODULE, isFSObject, SmartFoxConnection.Sfs.LastJoinedRoom);
        if (SmartFoxConnection.isAlready())
        {
            SmartFoxConnection.send(packet);
        }
        else
        {
            SmartFoxConnection.Init();
            SmartFoxConnection.send(packet);
        }
    }

    public static void Change(int to, int from)
    {
        Debug.Log("=========================== CHANGE");
        ISFSObject isFSObject = new SFSObject();
        isFSObject.PutInt(CmdDefine.CMD_ID, CmdDefine.CMD.CHANGE_CHAR);

        isFSObject.PutInt(CmdDefine.ModuleGame.TO, to);
        isFSObject.PutInt(CmdDefine.ModuleGame.FROM, from);

        var packet = new ExtensionRequest(MODULE, isFSObject, SmartFoxConnection.Sfs.LastJoinedRoom);
        if (SmartFoxConnection.isAlready())
        {
            SmartFoxConnection.send(packet);
        }
        else
        {
            SmartFoxConnection.Init();
            SmartFoxConnection.send(packet);
        }
    }

    public static void Lock(List<M_Character> characters)
    {
        Debug.Log("=========================== LOCK");
        ISFSObject isFSObject = new SFSObject();
        isFSObject.PutInt(CmdDefine.CMD_ID, CmdDefine.CMD.LOCK_ARRANGE);

        ISFSArray objs = new SFSArray();
        for (int i = 0; i < characters.Count; i++)
        {
            objs.AddSFSObject(characters[i].parse());
        }

        isFSObject.PutSFSArray(CmdDefine.ModuleAccount.CHARACTERS, objs);

        var packet = new ExtensionRequest(MODULE, isFSObject, SmartFoxConnection.Sfs.LastJoinedRoom);
        if (SmartFoxConnection.isAlready())
        {
            SmartFoxConnection.send(packet);
        }
        else
        {
            SmartFoxConnection.Init();
            SmartFoxConnection.send(packet);
        }
    }
}
