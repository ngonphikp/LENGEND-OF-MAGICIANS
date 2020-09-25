using MEC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampaignGame : MonoBehaviour
{
    [SerializeField]
    private C_Milestone[] milestones = null;

    private void OnEnable()
    {
        Timing.RunCoroutine(_LoadMilestones());
    }

    private IEnumerator<float> _LoadMilestones()
    {
        yield return Timing.WaitForOneFrame;
        for(int i = 0; i < milestones.Length; i++)
        {
            milestones[i].set(GameManager.instance.milestones[i]);
        }
    }
}
