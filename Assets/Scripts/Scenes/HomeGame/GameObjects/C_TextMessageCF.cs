using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class C_TextMessageCF : MonoBehaviour
{
    [SerializeField]
    private Text txt = null;

    [SerializeField]
    private ScrollRect sc = null;

    public void set(string message)
    {
        txt.text = message;
        sc.verticalNormalizedPosition = 0;
    }

    public void add(string message)
    {
        txt.text += message;
        sc.verticalNormalizedPosition = 0;
    }
}
