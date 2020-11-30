using Sfs2X.Entities.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class M_Skill
{
    public string id_cfg;
    public string name;
    public string describe;
    public int type;

    public M_Skill()
    {

    }

    public M_Skill(ISFSObject obj)
    {
        id_cfg = obj.GetUtfString("id");
        name = obj.GetUtfString("name");
        describe = obj.GetUtfString("describe");
        type = obj.GetInt("type");
    }
}
