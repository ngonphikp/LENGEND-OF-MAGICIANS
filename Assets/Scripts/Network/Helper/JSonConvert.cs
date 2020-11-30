using Sfs2X.Entities.Data;
using System;
using System.Collections.Generic;
using UnityEngine;
public class JSonConvert
{      
    public void GetConfig_ConnectSFS()
    {
        try
        {
            TextAsset file = Resources.Load<TextAsset>("ConfigJSon/ConfigConnect");
            string jsonString = file.ToString();
            ISFSObject sfsObj = SFSObject.NewFromJsonData(jsonString);

            ConfigConnection.Host = GameManager.instance.host;

            ConfigConnection.TCPPort = sfsObj.GetInt("TCPPort");
            ConfigConnection.WsPort = sfsObj.GetInt("WsPort");
            ConfigConnection.Zone = sfsObj.GetUtfString("Zone");

            //ConfigConnection.Host = sfsObj.GetUtfString("Host");
        }
        catch (Exception e)
        {
            Debug.LogError(e.ToString());
        }
    }

    public IEnumerable<M_Skill> GetListSkill()
    {
        TextAsset file = Resources.Load<TextAsset>("ConfigJSon/Skill");
        string jsonString = file.ToString();
        ISFSObject sfsObj = SFSObject.NewFromJsonData(jsonString);
        ISFSArray arr = sfsObj.GetSFSArray("list");
        for (int i = 0; i < arr.Size(); i++)
        {
            ISFSObject obj = arr.GetSFSObject(i);
            yield return new M_Skill(obj);
        }
    }

    public IEnumerable<M_Character> GetListHero()
    {
        TextAsset file = Resources.Load<TextAsset>("ConfigJSon/Hero");
        string jsonString = file.ToString();
        ISFSObject sfsObj = SFSObject.NewFromJsonData(jsonString);
        ISFSArray arr = sfsObj.GetSFSArray("list");
        for (int i = 0; i < arr.Size(); i++)
        {
            ISFSObject obj = arr.GetSFSObject(i);
            //Debug.Log(obj.GetDump());

            M_Character hero = new M_Character(obj, C_Enum.ReadType.CONFIG);

            hero.type = C_Enum.CharacterType.Hero;

            yield return hero;
        }
    }

    public IEnumerable<M_Character> GetListCreep()
    {
        TextAsset file = Resources.Load<TextAsset>("ConfigJSon/Creep");
        string jsonString = file.ToString();
        ISFSObject sfsObj = SFSObject.NewFromJsonData(jsonString);
        ISFSArray arr = sfsObj.GetSFSArray("list");
        for (int i = 0; i < arr.Size(); i++)
        {
            ISFSObject obj = arr.GetSFSObject(i);
            //Debug.Log(obj.GetDump());

            M_Character creep = new M_Character(obj, C_Enum.ReadType.CONFIG);

            creep.type = C_Enum.CharacterType.Creep;

            yield return creep;
        }
    }

    public IEnumerable<M_Character> GetListBoss()
    {
        TextAsset file = Resources.Load<TextAsset>("ConfigJSon/Boss");
        string jsonString = file.ToString();
        ISFSObject sfsObj = SFSObject.NewFromJsonData(jsonString);
        ISFSArray arr = sfsObj.GetSFSArray("list");
        for (int i = 0; i < arr.Size(); i++)
        {
            ISFSObject obj = arr.GetSFSObject(i);
            //Debug.Log(obj.GetDump());

            M_Character boss = new M_Character(obj, C_Enum.ReadType.CONFIG);

            boss.type = C_Enum.CharacterType.Boss;

            yield return boss;
        }
    }

    public IEnumerable<M_Milestone> GetListMilestone()
    {
        TextAsset file = Resources.Load<TextAsset>("ConfigJSon/Milestone");
        string jsonString = file.ToString();
        ISFSObject sfsObj = SFSObject.NewFromJsonData(jsonString);
        ISFSArray arr = sfsObj.GetSFSArray("list");
        for (int i = 0; i < arr.Size(); i++)
        {
            ISFSObject obj = arr.GetSFSObject(i);
            //Debug.Log(obj.GetDump());

            M_Milestone milestone = new M_Milestone();
            milestone.id = obj.GetInt("id");
            milestone.name = obj.GetText("name");

            milestone.listCreep.Clear();

            ISFSArray cArr = obj.GetSFSArray("listCreep");
            for (int j = 0; j < cArr.Size(); j++)
            {
                ISFSObject cObj = cArr.GetSFSObject(j);
                //Debug.Log(cObj.GetDump());

                M_Character creep = new M_Character();
                creep.id_cfg = cObj.GetText("id");
                creep.lv = cObj.GetInt("lv");
                creep.idx = cObj.GetInt("idx");

                creep.type = C_Enum.CharacterType.Creep;

                creep.UpdateById();

                milestone.listCreep.Add(creep);
            }

            yield return milestone;
        }
    }

    public IEnumerable<M_BossG> GetListBossG()
    {
        TextAsset file = Resources.Load<TextAsset>("ConfigJSon/BossG");
        string jsonString = file.ToString();
        ISFSObject sfsObj = SFSObject.NewFromJsonData(jsonString);
        ISFSArray arr = sfsObj.GetSFSArray("list");
        for (int i = 0; i < arr.Size(); i++)
        {
            ISFSObject obj = arr.GetSFSObject(i);
            //Debug.Log(obj.GetDump());

            M_BossG bossG = new M_BossG();
            bossG.id = obj.GetInt("id");
            bossG.name = obj.GetText("name");

            bossG.listBoss.Clear();

            ISFSArray cArr = obj.GetSFSArray("listBoss");
            for (int j = 0; j < cArr.Size(); j++)
            {
                ISFSObject cObj = cArr.GetSFSObject(j);
                //Debug.Log(cObj.GetDump());

                M_Character boss = new M_Character();
                boss.id_cfg = cObj.GetText("id");
                boss.lv = cObj.GetInt("lv");
                boss.idx = cObj.GetInt("idx");

                boss.type = C_Enum.CharacterType.Boss;

                boss.UpdateById();

                bossG.listBoss.Add(boss);
            }

            yield return bossG;
        }
    }
}
