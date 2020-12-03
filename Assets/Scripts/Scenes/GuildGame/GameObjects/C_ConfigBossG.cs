using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class C_ConfigBossG : MonoBehaviour
{
    [SerializeField]
    private Text txtHp = null;
    [SerializeField]
    private Image imgHp = null;
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

    private int id_bg;
    private int id_tbg;

    private M_Milestone milestone = null;

    public void set(int id_bg, int id_tbg)
    {
        this.id_bg = id_bg;
        this.id_tbg = id_tbg;

        M_BossGuild bossG = GuildGame.instance.bossesDic[id_bg];
        M_TickBossGuild tick = GuildGame.instance.tick_bossesDic[id_tbg];

        milestone = GameManager.instance.bossGuildsDic[bossG.id_boss];
        M_Character boss = milestone.lstCharacter[0];
        boss.UpdateById();
        boss.UpdateLevel();
        boss.current_hp = bossG.cur_hp;

        txtHp.text = boss.current_hp + " / " + boss.max_hp;
        imgHp.fillAmount = boss.current_hp * 1.0f / boss.max_hp;

        switch (bossG.status)
        {
            case C_Enum.StatusBossG.Lock:
                C_Util.ActiveGO(false, btnFight, btnReward);
                C_Util.ActiveGO(true, btnLock);

                btnLock.GetComponent<Image>().material = (GameManager.instance.account.job == C_Enum.JobGuild.Master) ? null : gray; 
                break;
            case C_Enum.StatusBossG.Fight:
                C_Util.ActiveGO(false, btnLock, btnReward);
                C_Util.ActiveGO(true, btnFight);

                txtTurn.text = "Chiến " + tick.cur_turn + " / " + 2;
                btnFight.GetComponent<Image>().material = (tick.cur_turn > 0) ? null : gray;

                break;
            case C_Enum.StatusBossG.Reward:
                C_Util.ActiveGO(false, btnLock, btnFight);
                C_Util.ActiveGO(true, btnReward);

                btnReward.GetComponent<Image>().material = (tick.is_reward) ? null : gray;
                break;
            default:
                break;
        }
    }

    public void UnLock()
    {
        if (GameManager.instance.account.job == C_Enum.JobGuild.Master)
        {
            RequestGuild.UnLockBoss(id_bg);
        }
        else Debug.LogWarning("Chỉ chủ hội mới được mở khóa chức năng này!");
    }

    public void Fight()
    {       
        if(GuildGame.instance.tick_bossesDic[id_tbg].cur_turn > 0)
        {
            GameManager.instance.isAttack = true;
            GameManager.instance.battleType = C_Enum.BattleType.BossGuild;

            GameManager.instance.milestone = milestone;

            GameManager.instance.mainName = C_Enum.MainGame.GuildScene;
            SceneManager.LoadSceneAsync("PlayGame");
        }
        else Debug.LogWarning("Bạn đã hết lượt đánh boss này!");
    }

    public void Reward()
    {
        if (GuildGame.instance.tick_bossesDic[id_tbg].is_reward)
        {
            RequestGuild.RewardBoss(id_tbg);
        }
        else Debug.LogWarning("Bạn đã nhận thưởng rồi hoặc bạn không tham gia hạ gục boss!");
    }
}
