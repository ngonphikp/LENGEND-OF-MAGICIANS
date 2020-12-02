using MEC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_BossesG : MonoBehaviour
{
    [SerializeField]
    private C_BossG[] bosses = null;

    private Dictionary<int, C_BossG> bossDic = null;

    public void show()
    {
        if(bossDic == null)
        {
            bossDic = new Dictionary<int, C_BossG>();
            for (int i = 0; i < bosses.Length; i++)
            {
                bosses[i].set(GuildGame.instance.bosses[i].id);
                bossDic.Add(GuildGame.instance.bosses[i].id, bosses[i]);
            }
        }

        for (int i = 0; i < bosses.Length; i++)
        {
            bosses[i].UpdateUI();
        }

        this.gameObject.SetActive(true);
    }

    public void UpdateUI(int id)
    {
        for (int i = 0; i < bosses.Length; i++)
        {
            bosses[i].isActive = false;
        }
        bossDic[id].UpdateUI();
        bossDic[id].isActive = true;
    }
}
