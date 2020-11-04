using Sfs2X.Entities.Data;

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

        this.id = obj.GetInt(CmdDefine.ModuleUser.ID);
        this.usename = obj.GetUtfString(CmdDefine.ModuleUser.USERNAME);
        this.password = obj.GetUtfString(CmdDefine.ModuleUser.PASSWORD);
        this.name = obj.GetUtfString(CmdDefine.ModuleUser.NAME);
    }
}
