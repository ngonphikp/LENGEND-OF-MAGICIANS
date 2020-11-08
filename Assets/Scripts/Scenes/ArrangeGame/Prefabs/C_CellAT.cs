using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_CellAT : MonoBehaviour
{
    public C_DD content = null;

    public void set(M_Character character, Canvas canvas = null, bool isDD = true)
    {
        content.Init(character, canvas, isDD);
    }
}
