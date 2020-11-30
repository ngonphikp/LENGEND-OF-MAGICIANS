using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class C_ConfigBossG : MonoBehaviour
{
    [SerializeField]
    private Text txtHp = null;
    [SerializeField]
    private GameObject btnFight = null;
    [SerializeField]
    private GameObject btnLock = null;
    [SerializeField]
    private GameObject btnReward = null;

    [SerializeField]
    private Text txtTurn = null;

    [SerializeField]
    private Material gray = null;

    private int id;

    public void set(int id)
    {
        this.id = id;

        M_TickBossGuild tick = GuildGame.instance.tick_bossesDic[id];

        txtHp.text = tick.cur_hp + " / " + tick.max_hp;

        switch (tick.status)
        {
            case C_Enum.StatusBossG.Lock:
                C_Util.ActiveGO(false, btnFight, btnReward);
                C_Util.ActiveGO(true, btnLock);

                btnLock.GetComponent<Image>().material = (GameManager.instance.account.job == C_Enum.JobGuild.Master) ? null : gray; 
                break;
            case C_Enum.StatusBossG.Fight:
                C_Util.ActiveGO(false, btnLock, btnReward);
                C_Util.ActiveGO(true, btnFight);

                txtTurn.text = "Chiến " + tick.cut_turn + " / " + tick.max_turn;
                btnFight.GetComponent<Image>().material = (tick.cut_turn > 0) ? null : gray;

                break;
            case C_Enum.StatusBossG.Reward:
                C_Util.ActiveGO(false, btnLock, btnFight);
                C_Util.ActiveGO(true, btnReward);

                btnReward.GetComponent<Image>().material = (tick.isReward) ? null : gray;
                break;
            default:
                break;
        }
    }
}
