using System.Collections.Generic;

[System.Serializable]
public class M_Milestone
{
    public int id;
    public string name;
    public int maxTurn = 20;
    public List<M_Character> listCreep = new List<M_Character>();
}
