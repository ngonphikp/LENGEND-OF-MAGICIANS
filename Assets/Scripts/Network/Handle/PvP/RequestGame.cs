using Sfs2X.Entities.Data;
using Sfs2X.Requests;
using System.Collections.Generic;
using UnityEngine;

public class RequestGame 
{
    private static string MODULE = CmdDefine.Module.MODULE_GAME;

    public static void StartPvP()
    {
        Debug.Log("=========================== START PVP");
        ISFSObject isFSObject = new SFSObject();
        isFSObject.PutInt(CmdDefine.CMD_ID, CmdDefine.CMD.START_PVP);

        var packet = new ExtensionRequest(MODULE, isFSObject);
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
