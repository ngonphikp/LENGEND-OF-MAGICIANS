using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class C_FindAcc : MonoBehaviour
{
    [SerializeField]
    private InputField ipfFind = null;

    public bool isCheckId { get; set; } = false;

    public void Find()
    {
        if (ipfFind.text == "") return;

        RequestCF.FindAccountGlobal (ipfFind.text, isCheckId);

        ipfFind.text = "";
    }
}
