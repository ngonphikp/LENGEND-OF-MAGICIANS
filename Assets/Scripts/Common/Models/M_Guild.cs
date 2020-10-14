using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class M_Guild
{
    public int id;
    public string name;
    public int lv;
    public string noti;
    public string boss;
    public int currentMember;
    public int maxMember = 20;

    public M_Guild()
    {

    }

    public void UpdateLevel()
    {
        for (int i = 1; i < lv; i++) UpLevel();
    }

    public void UpLevel()
    {
        maxMember += 5;
    }
}
