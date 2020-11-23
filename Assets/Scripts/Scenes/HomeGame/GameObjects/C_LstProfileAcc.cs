using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_LstProfileAcc : MonoBehaviour
{
    [SerializeField]
    private GameObject prb = null;
    [SerializeField]
    private Transform content = null;

    private List<C_ProfileAcc> lstObjs = new List<C_ProfileAcc>();

    public void set(List<M_Account> data)
    {
        List<C_ProfileAcc> news = new List<C_ProfileAcc>();
        int i;
        for (i = 0; i < data.Count; i++)
        {
            if (i < lstObjs.Count)
            {
                lstObjs[i].set(data[i]);
                news.Add(lstObjs[i]);
            }
            else
            {
                C_ProfileAcc obj = Instantiate(prb, content).GetComponent<C_ProfileAcc>();
                obj.set(data[i]);
                news.Add(obj);
            }
        }

        for (int j = i; j < lstObjs.Count; j++)
        {
            Destroy(lstObjs[j].gameObject);
        }

        lstObjs = news;
    }
}
