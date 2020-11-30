using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class C_BossG : MonoBehaviour
{
    [SerializeField]
    private float speed = 10f;
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
        if(isActive) RotatingFrame();
    }

    private void RotatingFrame()
    {
        tranFrame.Rotate(0, 0, speed * Time.deltaTime);
    }

    public void set(int id)
    {
        this.id = id;        
    }

    public void UpdateUI()
    {
        imgBoss.material = (GuildGame.instance.tick_bossesDic[id].status == C_Enum.StatusBossG.Lock) ? gray : null;
    }

    public void OnClick()
    {
        RequestGuild.GetTickBoss(id);
    }
}
