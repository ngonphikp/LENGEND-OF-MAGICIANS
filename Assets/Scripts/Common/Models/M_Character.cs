using Sfs2X.Entities.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class M_Character
{
    public int id = -1;
    public string id_cfg;
    public int lv;
    public int idx = -1;

    public int def = 1;
    public int hp = 1;
    public int atk = 1;
    public float crit = 1.0f;
    public float dodge = 1.0f;

    public string name;
    public string element;
    public int star;

    public List<string> skills = new List<string>();

    public bool isDie = false;

    [SerializeField]
    private int current_ep = 0;
    public int max_ep = 1;
    [SerializeField]
    private int current_hp = 0;
    public int max_hp = 1;

    public int Current_ep
    {
        get => current_ep; set
        {
            if (value < 0) value = 0; if (value > max_ep) value = max_ep;
            current_ep = value;
        }
    }
    public int Current_hp
    {
        get => current_hp; set
        { 
            if (value < 0) value = 0; if (value > max_hp) value = max_hp; 
            current_hp = value;
        }
    }

    public C_Enum.CharacterType type = C_Enum.CharacterType.Hero;

    public int team = 0;

    public M_Character()
    {

    }

    public M_Character(M_Character character)
    {
        this.id = character.id;
        this.idx = character.idx;
        this.id_cfg = character.id_cfg;
        this.name = character.name;
        this.element = character.element;
        this.star = character.star;
        this.def = character.def;
        this.hp = character.hp;
        this.atk = character.atk;
        this.crit = character.crit;
        this.dodge = character.dodge;
        this.team = character.team;
        this.lv = character.lv;
        this.type = character.type;
        
        this.max_ep = character.max_ep;
        this.current_ep = character.current_ep;
        this.max_hp = character.max_hp;
        this.current_hp = character.current_hp;        
    }

    public void UpdateLevel()
    {
        for (int i = 1; i < lv; i++) UpLevel();
    }

    public void UpLevel()
    {
        atk = Mathf.RoundToInt(atk * C_Params.coeUpLv);
        def = Mathf.RoundToInt(def * C_Params.coeUpLv);
        hp = Mathf.RoundToInt(hp * C_Params.coeUpLv);        
        crit = crit * C_Params.coeUpLv;
        dodge = dodge * C_Params.coeUpLv;
    }

    public M_Character(int id, string id_cfg, int lv, int idx)
    {
        this.id = id;
        this.id_cfg = id_cfg;
        this.lv = lv;
        this.idx = idx;
    }

    public M_Character(ISFSObject obj, C_Enum.ReadType type)
    {
        if (obj == null) return;

        switch (type)
        {
            case C_Enum.ReadType.SERVER:
                this.id = obj.GetInt(CmdDefine.ModuleCharacter.ID);
                this.id_cfg = obj.GetUtfString(CmdDefine.ModuleCharacter.ID_CFG);
                this.lv = obj.GetInt(CmdDefine.ModuleCharacter.LV);
                this.idx = obj.GetInt(CmdDefine.ModuleCharacter.IDX);
                break;
            case C_Enum.ReadType.CONFIG:
                this.id_cfg = obj.GetText("id");
                this.name = obj.GetText("name");
                this.element = obj.GetText("element");
                this.star = obj.GetInt("star");
                this.def = obj.GetInt("def");
                this.hp = obj.GetInt("hp");
                this.atk = obj.GetInt("atk");
                this.crit = (float)obj.GetDouble("crit");
                this.dodge = (float)obj.GetDouble("dodge");

                this.skills.Clear();
                ISFSArray skills = obj.GetSFSArray("skill");
                for(int i = 0; i < skills.Size(); i++)
                    this.skills.Add(skills.GetUtfString(i));

                break;
            default:
                break;
        }
    }

    public ISFSObject parse()
    {
        ISFSObject obj = new SFSObject();

        obj.PutInt(CmdDefine.ModuleCharacter.ID, this.id);
        obj.PutUtfString(CmdDefine.ModuleCharacter.ID_CFG, this.id_cfg);
        obj.PutInt(CmdDefine.ModuleCharacter.LV, this.lv);
        obj.PutInt(CmdDefine.ModuleCharacter.IDX, this.idx);

        return obj;
    }

    public void UpdateById(string id_cfg = null)
    {
        if (id_cfg == null) id_cfg = this.id_cfg;
        else this.id_cfg = id_cfg;

        M_Character nvInCfg = new M_Character();

        switch (type)
        {
            case C_Enum.CharacterType.Hero:
                nvInCfg = GameManager.instance.herosDic[id_cfg];
                break;
            case C_Enum.CharacterType.Creep:
                nvInCfg = GameManager.instance.creepsDic[id_cfg];
                break;
            case C_Enum.CharacterType.Boss:
                nvInCfg = GameManager.instance.bossesDic[id_cfg];
                break;
        }

        this.name = nvInCfg.name;
        this.element = nvInCfg.element;
        this.def = nvInCfg.def;
        this.hp = nvInCfg.hp;
        this.crit = nvInCfg.crit;
        this.dodge = nvInCfg.dodge;
        this.atk = nvInCfg.atk;
        this.star = nvInCfg.star;
    }
}
