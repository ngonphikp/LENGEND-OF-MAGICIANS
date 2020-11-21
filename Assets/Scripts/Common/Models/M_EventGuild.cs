using Sfs2X.Entities.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class M_EventGuild
{
    public int id;
    public long time;
    public string content;

    public M_EventGuild()
    {

    }

    public M_EventGuild(ISFSObject obj)
    {
        if (obj == null) return;

        id = obj.GetInt(CmdDefine.ModuleEventGuild.ID);
        time = obj.GetLong(CmdDefine.ModuleEventGuild.TIME);
        content = obj.GetUtfString(CmdDefine.ModuleEventGuild.CONTENT);
    }

    public ISFSObject parse()
    {
        ISFSObject obj = new SFSObject();
        obj.PutInt(CmdDefine.ModuleEventGuild.ID, id);
        obj.PutLong(CmdDefine.ModuleEventGuild.TIME, time);
        obj.PutUtfString(CmdDefine.ModuleEventGuild.CONTENT, content);
        return obj;
    }
}
