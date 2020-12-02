using Sfs2X.Entities.Data;
using Sfs2X.Requests;
using UnityEngine;

public class RequestCampain
{
    private static string MODULE = CmdDefine.Module.MODULE_CAMPAIN;

    public static void EndGame(int id_ml, int star, bool isSave = true)
    {
        Debug.Log("=========================== End Game: Save: " + isSave);
        ISFSObject isFSObject = new SFSObject();
        isFSObject.PutInt(CmdDefine.CMD_ID, CmdDefine.CMD.END_GAME_CAMPAIN);

        isFSObject.PutInt(CmdDefine.ModuleTickCampain.ID_ML, id_ml);
        isFSObject.PutInt(CmdDefine.ModuleTickCampain.STAR, star);

        isFSObject.PutBool(CmdDefine.ModuleTickCampain.IS_SAVE, isSave);
        var packet = new ExtensionRequest(MODULE, isFSObject);
        SmartFoxConnection.send(packet);
    }
}
