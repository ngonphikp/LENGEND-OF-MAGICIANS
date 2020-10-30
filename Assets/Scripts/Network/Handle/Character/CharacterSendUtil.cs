using Sfs2X.Entities.Data;
using Sfs2X.Requests;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSendUtil 
{
    private static string MODULE = CmdDefine.MODULE_CHARACTER;

    public static void sendUpLevel(int id_nv)
    {
        Debug.Log("=========================== Up Level");
        ISFSObject isFSObject = new SFSObject();
        isFSObject.PutInt(CmdDefine.CMDID, CmdDefine.UPLEVEL);

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

    public static void sendArrange(List<M_Character> nhanVats)
    {
        Debug.Log("=========================== Arrange");
        ISFSObject isFSObject = new SFSObject();
        isFSObject.PutInt(CmdDefine.CMDID, CmdDefine.ARRANGE);

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
