using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class C_BossG : MonoBehaviour
{
    [SerializeField]
    private float speedAc = -200f;
    [SerializeField]
    private float speedUnAc = 20f;
    [SerializeField]
    private Transform tranFrame = null;

    [SerializeField]
    private Image imgBoss = null;
    [SerializeField]
    private Material gray = null;

    private int id;

    public bool isActive = false;

    private void Update()
    {
        if(this.gameObject.activeInHierarchy) RotatingFrame();
    }

    private void RotatingFrame()
    {
        tranFrame.Rotate(0, 0, ((isActive) ? speedAc : speedUnAc) * Time.deltaTime);
    }

    public void set(int id)
    {
        this.id = id;        
    }

    public void UpdateUI()
    {
        imgBoss.material = (GuildGame.instance.bossesDic[id].status == C_Enum.StatusBossG.Lock) ? gray : null;
    }

    public void OnClick()
    {
        RequestGuild.GetTickBoss(id);
    }
}
