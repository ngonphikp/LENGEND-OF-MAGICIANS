using Sfs2X.Core;
using Sfs2X.Entities.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleExtension 
{
    public static void OnExtensionResponse(BaseEvent evt)
    {
        string cmd = (string)evt.Params["cmd"];
        SFSObject dataObject = (SFSObject)evt.Params["params"];

        Debug.Log(dataObject.GetDump());

        switch (cmd)
        {
            case CmdDefine.MODULE_USER:
                HandleUser.OnMessage(dataObject);
                break;
            case CmdDefine.MODULE_CHARACTER:
                HandleChacracter.OnMessage(dataObject);
                break;
            case CmdDefine.MODULE_TICKMILESTONE:
                HandleTickMilestone.OnMessage(dataObject);
                break;
            case CmdDefine.MODULE_GUILD:
                HandleGuild.OnMessage(dataObject);
                break;
            default:

                break;
        }        
    }
}
