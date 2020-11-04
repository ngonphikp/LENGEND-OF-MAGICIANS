using Sfs2X.Entities.Data;
using Sfs2X.Requests;
using UnityEngine;

public class RequestMilestone 
{
    private static string MODULE = CmdDefine.Module.MODULE_MILESTONE;

    public static void EndGame(int id_ml, int id_tk, int star, bool isSave = true)
    {
        Debug.Log("=========================== End Game: Save: " + isSave);
        ISFSObject isFSObject = new SFSObject();
        isFSObject.PutInt(CmdDefine.CMD_ID, CmdDefine.CMD.ENDGAME);

        isFSObject.PutInt("id_ml", id_ml);
        isFSObject.PutInt("id_tk", id_tk);
        isFSObject.PutInt("star", star);
        isFSObject.PutBool("is_save", isSave);
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
