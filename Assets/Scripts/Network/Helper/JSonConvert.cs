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
            TextAsset file = Resources.Load<TextAsset>("ConfigJSon/Connect");
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

    public IEnumerable<M_Milestone> GetListCampaign()
    {
        TextAsset file = Resources.Load<TextAsset>("ConfigJSon/Campaign");
        string jsonString = file.ToString();
        ISFSObject sfsObj = SFSObject.NewFromJsonData(jsonString);
        ISFSArray arr = sfsObj.GetSFSArray("list");
        for (int i = 0; i < arr.Size(); i++)
        {
            yield return new M_Milestone(arr.GetSFSObject(i), C_Enum.CharacterType.Creep);
        }
    }

    public IEnumerable<M_Milestone> GetListBossGuild()
    {
        TextAsset file = Resources.Load<TextAsset>("ConfigJSon/BossGuild");
        string jsonString = file.ToString();
        ISFSObject sfsObj = SFSObject.NewFromJsonData(jsonString);
        ISFSArray arr = sfsObj.GetSFSArray("list");
        for (int i = 0; i < arr.Size(); i++)
        {
            yield return new M_Milestone(arr.GetSFSObject(i), C_Enum.CharacterType.Boss);
        }
    }
}
