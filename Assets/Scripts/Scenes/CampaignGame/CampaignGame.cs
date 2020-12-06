using MEC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampaignGame : MonoBehaviour
{
    public static CampaignGame instance = null;

    [SerializeField]
    private C_Milestone[] milestones = null;

    private Dictionary<int, C_Milestone> campainsDic = new Dictionary<int, C_Milestone>();

    private void Awake()
    {
        if (instance == null) instance = this;

        Timing.RunCoroutine(_LoadMilestones());
    }

    private void OnEnable()
    {
        RequestCampaign.GetTicks();        
    }

    private IEnumerator<float> _LoadMilestones()
    {
        yield return Timing.WaitForOneFrame;
        for(int i = 0; i < milestones.Length; i++)
        {
            milestones[i].set(GameManager.instance.campaigns[i]);
            campainsDic.Add(GameManager.instance.campaigns[i].id, milestones[i]);
        }
    }

    public void RecTicks(List<M_Tick_Campaign> ticks)
    {
        for (int i = 0; i < ticks.Count && i < campainsDic.Count; i++)
        {
            campainsDic[i + 1].UpdateStar(ticks[i].star);
        }
    }

    public void Back()
    {
        MainGame.instance.ShowScene(C_Enum.MainGame.HomeScene);
    }
}
