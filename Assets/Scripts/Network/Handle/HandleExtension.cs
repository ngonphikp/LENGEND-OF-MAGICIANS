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
            case CmdDefine.Module.MODULE_USER:
                HandleUser.OnResponse(dataObject);
                break;
            case CmdDefine.Module.MODULE_CHARACTER:
                HandleChacracter.OnResponse(dataObject);
                break;
            case CmdDefine.Module.MODULE_TICKMILESTONE:
                HandleTickMilestone.OnResponse(dataObject);
                break;
            case CmdDefine.Module.MODULE_GUILD:
                HandleGuild.OnResponse(dataObject);
                break;
            default:

                break;
        }        
    }
}
