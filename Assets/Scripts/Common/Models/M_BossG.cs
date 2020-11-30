using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class M_BossG
{
    public int id;
    public string name;
    public int maxTurn = 20;
    public List<M_Character> listBoss = new List<M_Character>();
}
