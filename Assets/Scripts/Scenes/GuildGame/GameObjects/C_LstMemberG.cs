using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_LstMemberG : MonoBehaviour
{
    [SerializeField]
    private GameObject prb = null;
    [SerializeField]
    private Transform content = null;

    private List<C_MemberG> lstObjs = new List<C_MemberG>();

    public void set(List<M_Account> data)
    {
        List<C_MemberG> news = new List<C_MemberG>();
        int i;
        for (i = 0; i < data.Count; i++)
        {
            if(i < lstObjs.Count)
            {
                lstObjs[i].set(data[i]);
                news.Add(lstObjs[i]);
            }
            else
            {
                C_MemberG obj = Instantiate(prb, content).GetComponent<C_MemberG>();
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
