using Sfs2X.Entities.Data;
using System.Collections.Generic;
using UnityEngine;

public class HandleUser
{
    public static void OnResponse(SFSObject sfsObject)
    {
        int cmdid = (short)sfsObject.GetInt(CmdDefine.CMD_ID);

        Debug.Log(sfsObject.GetDump());

        switch (cmdid)
        {
            case CmdDefine.CMD.GETINFO:
                HandleGetInfo(sfsObject);
                break;
            case CmdDefine.CMD.SELECTION:
                HandleSelection(sfsObject);
                break;
            case CmdDefine.CMD.TAVERN:
                HandleTavern(sfsObject);
                break;
            default:

                break;
        }
    }

    public static void HandleGetInfo(SFSObject packet)
    {
        Debug.Log("=========================== HANDLE GET INFO\n" + packet.GetDump());
        short ec = packet.GetShort(CmdDefine.ERROR_CODE);
        if (ec == CmdDefine.ErrorCode.SUCCESS)
        {
            List<M_Character> lstCharacter = new List<M_Character>();
            ISFSArray characters = packet.GetSFSArray(CmdDefine.ModuleUser.CHARACTERS);
            for(int i = 0; i < characters.Size(); i++)
            {
                M_Character nhanVat = new M_Character(characters.GetSFSObject(i), C_Enum.ReadType.SERVER);
                nhanVat.UpdateById();
                nhanVat.UpdateLevel();
                nhanVat.type = C_Enum.CharacterType.Hero;
                lstCharacter.Add(nhanVat);
            }

            List<M_Milestone> lstTick_milestones = new List<M_Milestone>();
            ISFSArray tick_milestones = packet.GetSFSArray(CmdDefine.ModuleUser.TICK_MILESTONES);
            for (int i = 0; i < tick_milestones.Size(); i++)
            {
                lstTick_milestones.Add(new M_Milestone(tick_milestones.GetSFSObject(i)));
            }

            lstTick_milestones.Add(new M_Milestone(tick_milestones.Size(), 0));

            LoginGame.instance.RecInfo(lstCharacter, lstTick_milestones);
        }
        else
        {
            Debug.Log("ErrorCode: " + ec);
        }
    }

    public static void HandleSelection(SFSObject packet)
    {
        Debug.Log("=========================== HANDLE SELECTION\n" + packet.GetDump());
        short ec = packet.GetShort(CmdDefine.ERROR_CODE);
        if (ec == CmdDefine.ErrorCode.SUCCESS)
        {
            List<M_Character> lstNhanVat = new List<M_Character>();
            ISFSArray nhanvats = packet.GetSFSArray("nhanvats");
            for (int i = 0; i < nhanvats.Size(); i++)
            {
                M_Character nhanVat = new M_Character(nhanvats.GetSFSObject(i), C_Enum.ReadType.SERVER);
                nhanVat.UpdateById();
                nhanVat.UpdateLevel();
                nhanVat.type = C_Enum.CharacterType.Hero;
                lstNhanVat.Add(nhanVat);
            }

            SelectionGame.instance.RecSelection(lstNhanVat);
        }
        else
        {
            Debug.Log("ErrorCode: " + ec);
        }
    }

    public static void HandleTavern(SFSObject packet)
    {
        Debug.Log("=========================== HANDLE TAVERN\n" + packet.GetDump());
        short ec = packet.GetShort(CmdDefine.ERROR_CODE);
        if (ec == CmdDefine.ErrorCode.SUCCESS)
        {
            C_Enum.CardType type = (C_Enum.CardType)packet.GetInt("type_tavern");

            M_Character nhanvat = new M_Character(packet.GetSFSObject("nhanvat"), C_Enum.ReadType.SERVER);
            nhanvat.lv = 1;
            nhanvat.UpdateById();
            nhanvat.Current_ep = nhanvat.max_ep = 100;
            nhanvat.Current_hp = nhanvat.max_hp = nhanvat.hp;
            nhanvat.UpdateLevel();
            nhanvat.type = C_Enum.CharacterType.Hero;            
                                    
            TavernGame.instance.RecCard(type, nhanvat);
        }
        else
        {
            Debug.Log("ErrorCode: " + ec);
        }
    }
}
