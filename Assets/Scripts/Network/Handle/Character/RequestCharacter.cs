using Sfs2X.Entities.Data;
using Sfs2X.Requests;
using System.Collections.Generic;
using UnityEngine;

public class RequestCharacter 
{
    private static string MODULE = CmdDefine.Module.MODULE_CHARACTER;

    public static void Uplevel(int id_nv)
    {
        Debug.Log("=========================== Up Level");
        ISFSObject isFSObject = new SFSObject();
        isFSObject.PutInt(CmdDefine.CMD_ID, CmdDefine.CMD.UPLEVEL);

        isFSObject.PutInt("id_nv", id_nv);
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

    public static void Arrange(List<M_Character> nhanVats)
    {
        Debug.Log("=========================== Arrange");
        ISFSObject isFSObject = new SFSObject();
        isFSObject.PutInt(CmdDefine.CMD_ID, CmdDefine.CMD.ARRANGE);

        ISFSArray nvObjs = new SFSArray();
        for (int i = 0; i < nhanVats.Count; i++)
        {
            nvObjs.AddSFSObject(nhanVats[i].parse());
        }

        isFSObject.PutSFSArray("nhanvats", nvObjs);
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
