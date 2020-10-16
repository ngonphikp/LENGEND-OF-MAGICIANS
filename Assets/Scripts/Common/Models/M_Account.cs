using Sfs2X.Entities.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class M_Account
{
    public int id;
    public string usename;
    public string password;
    public string name;

    public int id_guilds = -1;

    public M_Account()
    {

    }

    public M_Account(ISFSObject obj)
    {
        if (obj == null) return;

        this.id = obj.GetInt("id");
        this.usename = obj.GetUtfString("username");
        this.password = obj.GetUtfString("password");
        this.name = obj.GetUtfString("name");
    }
}
