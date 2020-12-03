using Sfs2X.Entities.Data;
using System.Collections.Generic;

[System.Serializable]
public class M_Milestone
{
    public int id;
    public string name;
    public int maxTurn = 20;
    public List<M_Character> lstCharacter = new List<M_Character>();

    public M_Milestone()
    {

    }

    public M_Milestone(ISFSObject obj, C_Enum.CharacterType type)
    {
        id = obj.GetInt("id");
        name = obj.GetText("name");

        lstCharacter.Clear();

        ISFSArray cArr = obj.GetSFSArray("lstCharacter");
        for (int j = 0; j < cArr.Size(); j++)
        {
            ISFSObject cObj = cArr.GetSFSObject(j);
            //Debug.Log(cObj.GetDump());

            M_Character character = new M_Character();
            character.id_cfg = cObj.GetText("id");
            character.lv = cObj.GetInt("lv");
            character.idx = cObj.GetInt("idx");

            character.type = type;

            character.UpdateById();
            character.UpdateLevel();

            lstCharacter.Add(character);
        }
    }
}
