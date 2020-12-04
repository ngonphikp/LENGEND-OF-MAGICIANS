using MEC;
using Sfs2X.Entities.Data;
using System.Collections.Generic;
using UnityEngine;

public class HandleGame
{
    public static void OnResponse(SFSObject sfsObject)
    {
        int cmdid = (short)sfsObject.GetInt(CmdDefine.CMD_ID);

        //Debug.Log(sfsObject.GetDump());

        switch (cmdid)
        {
            case CmdDefine.CMD.OUT_ROOM_GAME:
                HandleOutRoom(sfsObject);
                break;
            case CmdDefine.CMD.JOIN_ROOM_GAME:
                HandleJoinRoom(sfsObject);
                break;
            case CmdDefine.CMD.UN_ACTIVE_CHAR:
                HandleUnActive(sfsObject);
                break;
            case CmdDefine.CMD.ACTIVE_CHAR:
                HandleActive(sfsObject);
                break;
            case CmdDefine.CMD.CHANGE_CHAR:
                HandleChange(sfsObject);
                break;
            case CmdDefine.CMD.LOCK_ARRANGE:
                HandleLock(sfsObject);
                break;
            case CmdDefine.CMD.START_GAME:
                HandleStartGame(sfsObject);
                break;
            default:
                break;
        }
    }

    public static void HandleStartGame(SFSObject packet)
    {
        Debug.Log("=========================== HANDLE START GAME\n" + packet.GetDump());
        short ec = packet.GetShort(CmdDefine.ERROR_CODE);
        if (ec == CmdDefine.ErrorCode.SUCCESS)
        {
            Timing.RunCoroutine(ArrangeGame.instance._StartGame());
        }
        else
        {
            Debug.Log(CmdDefine.ErrorCode.Errors.ContainsKey(ec) ? CmdDefine.ErrorCode.Errors[ec] : ("Error Code" + ec));
        }
    }

    public static void HandleLock(SFSObject packet)
    {
        Debug.Log("=========================== HANDLE LOCK\n" + packet.GetDump());
        short ec = packet.GetShort(CmdDefine.ERROR_CODE);
        if (ec == CmdDefine.ErrorCode.SUCCESS)
        {
            int id_ac = packet.GetInt(CmdDefine.ModuleAccount.ID);
            if (id_ac != GameManager.instance.account.id)
            {
                List<M_Character> characters = new List<M_Character>();
                ISFSArray arr = packet.GetSFSArray(CmdDefine.ModuleGame.CHARACTERS);
                for (int j = 0; j < arr.Count; j++)
                {
                    M_Character character = new M_Character(arr.GetSFSObject(j), C_Enum.ReadType.SERVER);
                    character.type = C_Enum.CharacterType.Hero;
                    character.UpdateById();
                    character.UpdateLevel();

                    characters.Add(character);
                }
                ArrangeGame.instance.RecLock(characters);
            }
        }
        else
        {
            Debug.Log(CmdDefine.ErrorCode.Errors.ContainsKey(ec) ? CmdDefine.ErrorCode.Errors[ec] : ("Error Code" + ec));
        }
    }

    public static void HandleChange(SFSObject packet)
    {
        Debug.Log("=========================== HANDLE CHANGE\n" + packet.GetDump());
        short ec = packet.GetShort(CmdDefine.ERROR_CODE);
        if (ec == CmdDefine.ErrorCode.SUCCESS)
        {
            int id_ac = packet.GetInt(CmdDefine.ModuleAccount.ID);
            if (id_ac != GameManager.instance.account.id)
            {
                int to = packet.GetInt(CmdDefine.ModuleGame.TO);
                int from = packet.GetInt(CmdDefine.ModuleGame.FROM);
                ArrangeGame.instance.RecChange(to, from);
            }
        }
        else
        {
            Debug.Log(CmdDefine.ErrorCode.Errors.ContainsKey(ec) ? CmdDefine.ErrorCode.Errors[ec] : ("Error Code" + ec));
        }
    }

    public static void HandleActive(SFSObject packet)
    {
        Debug.Log("=========================== HANDLE ACTIVE\n" + packet.GetDump());
        short ec = packet.GetShort(CmdDefine.ERROR_CODE);
        if (ec == CmdDefine.ErrorCode.SUCCESS)
        {
            int id_ac = packet.GetInt(CmdDefine.ModuleAccount.ID);
            if (id_ac != GameManager.instance.account.id)
            {
                int id_char = packet.GetInt(CmdDefine.ModuleCharacter.ID);
                int idx = packet.GetInt(CmdDefine.ModuleCharacter.IDX);
                ArrangeGame.instance.RecActive(id_char, idx);
            }
        }
        else
        {
            Debug.Log(CmdDefine.ErrorCode.Errors.ContainsKey(ec) ? CmdDefine.ErrorCode.Errors[ec] : ("Error Code" + ec));
        }
    }

    public static void HandleUnActive(SFSObject packet)
    {
        Debug.Log("=========================== HANDLE UN ACTIVE\n" + packet.GetDump());
        short ec = packet.GetShort(CmdDefine.ERROR_CODE);
        if (ec == CmdDefine.ErrorCode.SUCCESS)
        {
            int id_ac = packet.GetInt(CmdDefine.ModuleAccount.ID);
            if (id_ac != GameManager.instance.account.id)
            {
                int id_char = packet.GetInt(CmdDefine.ModuleCharacter.ID);
                ArrangeGame.instance.RecUnActive(id_char);
            }
        }
        else
        {
            Debug.Log(CmdDefine.ErrorCode.Errors.ContainsKey(ec) ? CmdDefine.ErrorCode.Errors[ec] : ("Error Code" + ec));
        }
    }

    public static void HandleOutRoom(SFSObject packet)
    {
        Debug.Log("=========================== HANDLE OUT ROOM\n" + packet.GetDump());
        short ec = packet.GetShort(CmdDefine.ERROR_CODE);
        if (ec == CmdDefine.ErrorCode.SUCCESS)
        {
            int id_ac = packet.GetInt(CmdDefine.ModuleAccount.ID);
            if(id_ac == GameManager.instance.account.id)
            {
                FightingGame.instance.RecEndGame();
            }
        }
        else
        {
            Debug.Log(CmdDefine.ErrorCode.Errors.ContainsKey(ec) ? CmdDefine.ErrorCode.Errors[ec] : ("Error Code" + ec));
        }
    }

    public static void HandleJoinRoom(SFSObject packet)
    {
        Debug.Log("=========================== HANDLE JOIN ROOM\n" + packet.GetDump());
        short ec = packet.GetShort(CmdDefine.ERROR_CODE);
        if (ec == CmdDefine.ErrorCode.SUCCESS)
        {
            ISFSArray list = packet.GetSFSArray(CmdDefine.ModuleGame.LIST);
            for (int i = 0; i < list.Count; i++)
            {
                ISFSObject obj = list.GetSFSObject(i);
                M_Account account = new M_Account(obj.GetSFSObject(CmdDefine.ModuleGame.ACCOUNTS), C_Enum.StatusAccount.On);
                if(account.id != GameManager.instance.account.id)
                {
                    List<M_Character> characters = new List<M_Character>();
                    ISFSArray arr = obj.GetSFSArray(CmdDefine.ModuleGame.CHARACTERS);
                    for(int j = 0; j < arr.Count; j++)
                    {
                        M_Character character = new M_Character(arr.GetSFSObject(j), C_Enum.ReadType.SERVER);
                        character.type = C_Enum.CharacterType.Hero;
                        character.UpdateById();
                        character.UpdateLevel();

                        characters.Add(character);
                    }

                    Timing.RunCoroutine(PvP.instance._SuccessPvP(account, characters));
                    return;
                }
            }
        }
        else
        {
            Debug.Log(CmdDefine.ErrorCode.Errors.ContainsKey(ec) ? CmdDefine.ErrorCode.Errors[ec] : ("Error Code" + ec));
        }
    }
}
