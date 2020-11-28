using Sfs2X.Entities.Data;
using System.Collections.Generic;
using UnityEngine;

public class HandleAccount
{
    public static void OnResponse(SFSObject sfsObject)
    {
        int cmdid = (short)sfsObject.GetInt(CmdDefine.CMD_ID);

        Debug.Log(sfsObject.GetDump());

        switch (cmdid)
        {
            case CmdDefine.CMD.GET_INFO:
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
            int id_guild = packet.GetInt(CmdDefine.ModuleAccount.ID_GUILD);

            List<M_Character> lstCharacter = new List<M_Character>();
            ISFSArray characters = packet.GetSFSArray(CmdDefine.ModuleAccount.CHARACTERS);
            for(int i = 0; i < characters.Size(); i++)
            {
                M_Character character = new M_Character(characters.GetSFSObject(i), C_Enum.ReadType.SERVER);
                character.UpdateById();
                character.UpdateLevel();
                character.type = C_Enum.CharacterType.Hero;
                lstCharacter.Add(character);
            }

            List<M_Tick_Milestone> lstTick_milestones = new List<M_Tick_Milestone>();
            ISFSArray tick_milestones = packet.GetSFSArray(CmdDefine.ModuleAccount.TICK_MILESTONES);
            for (int i = 0; i < tick_milestones.Size(); i++)
            {
                lstTick_milestones.Add(new M_Tick_Milestone(tick_milestones.GetSFSObject(i)));
            }

            lstTick_milestones.Add(new M_Tick_Milestone(tick_milestones.Size() + 1, GameManager.instance.account.id, tick_milestones.Size() + 1, 0));

            LoginGame.instance.RecInfo(id_guild, lstCharacter, lstTick_milestones);
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
            List<M_Character> lstCharacter = new List<M_Character>();
            ISFSArray characters = packet.GetSFSArray(CmdDefine.ModuleAccount.CHARACTERS);
            for (int i = 0; i < characters.Size(); i++)
            {
                M_Character character = new M_Character(characters.GetSFSObject(i), C_Enum.ReadType.SERVER);
                character.UpdateById();
                character.UpdateLevel();
                character.type = C_Enum.CharacterType.Hero;
                lstCharacter.Add(character);
            }

            SelectionGame.instance.RecSelection(lstCharacter);
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
            C_Enum.CardType type = (C_Enum.CardType)packet.GetInt(CmdDefine.ModuleAccount.TYPE_TAVERN);

            M_Character character = new M_Character(packet.GetSFSObject(CmdDefine.ModuleAccount.CHARACTER), C_Enum.ReadType.SERVER);
            character.lv = 1;
            character.UpdateById();
            character.Current_ep = character.max_ep = 100;
            character.Current_hp = character.max_hp = character.hp;
            character.UpdateLevel();
            character.type = C_Enum.CharacterType.Hero;            
                                    
            TavernGame.instance.RecCard(type, character);
        }
        else
        {
            Debug.Log("ErrorCode: " + ec);
        }
    }
}
