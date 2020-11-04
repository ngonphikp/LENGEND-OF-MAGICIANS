using Sfs2X.Entities.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class M_Milestone
{
    public int id;
    public string name;
    public int star = 0;
    public int maxTurn = 20;
    public List<M_Character> listCreep = new List<M_Character>();

    public M_Milestone()
    {

    }

    public M_Milestone(ISFSObject obj)
    {
        if (obj == null) return;

        this.id = obj.GetInt(CmdDefine.ModuleMilestone.ID_ML);
        this.star = obj.GetInt(CmdDefine.ModuleMilestone.STAR);
    }

    public M_Milestone(int id, int star)
    {
        this.id = id;
        this.star = star;
    }
}
