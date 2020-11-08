using Sfs2X.Entities.Data;
using Sfs2X.Requests;
using System.Collections.Generic;
using UnityEngine;

public class RequestCharacter 
{
    private static string MODULE = CmdDefine.Module.MODULE_CHARACTER;

    public static void Uplevel(int id)
    {
        Debug.Log("=========================== Up Level");
        ISFSObject isFSObject = new SFSObject();
        isFSObject.PutInt(CmdDefine.CMD_ID, CmdDefine.CMD.UPLEVEL);

        isFSObject.PutInt(CmdDefine.ModuleCharacter.ID, id);
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

    public static void Arrange(List<M_Character> characters)
    {
        Debug.Log("=========================== Arrange");
        ISFSObject isFSObject = new SFSObject();
        isFSObject.PutInt(CmdDefine.CMD_ID, CmdDefine.CMD.ARRANGE);

        ISFSArray objs = new SFSArray();
        for (int i = 0; i < characters.Count; i++)
        {
            objs.AddSFSObject(characters[i].parse());
        }

        isFSObject.PutSFSArray(CmdDefine.ModuleAccount.CHARACTERS, objs);
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
