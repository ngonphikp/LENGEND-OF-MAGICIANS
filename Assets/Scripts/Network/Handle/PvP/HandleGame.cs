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
            case CmdDefine.CMD.JOIN_ROOM_GAME:
                HandleJoinRoom(sfsObject);
                break;
            default:

                break;
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
                M_Account acc = new M_Account(obj.GetSFSObject(CmdDefine.ModuleGame.ACCOUNTS), C_Enum.StatusAccount.On);
                if(acc.id != GameManager.instance.account.id)
                {
                    M_Account account = acc;
                    List<M_Character> characters = new List<M_Character>();
                    ISFSArray arr = obj.GetSFSArray(CmdDefine.ModuleGame.CHARACTERS);
                    for(int j = 0; j < arr.Count; j++)
                    {
                        M_Character character = new M_Character(arr.GetSFSObject(i), C_Enum.ReadType.SERVER);
                        if(character.idx != -1)
                        {
                            character.UpdateById();
                            character.UpdateLevel();
                            character.type = C_Enum.CharacterType.Hero;

                            characters.Add(character);
                        }                        
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
