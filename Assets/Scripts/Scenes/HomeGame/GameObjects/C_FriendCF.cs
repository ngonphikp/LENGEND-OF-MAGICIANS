using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class C_FriendCF : MonoBehaviour
{
    [SerializeField]
    private ScrollRect sc = null;
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

                if (data[i].status == C_Enum.StatusAccount.On) lstObjs[i].transform.SetSiblingIndex(0);

                news.Add(lstObjs[i]);
            }
            else
            {
                C_ProfileAcc obj = Instantiate(prb, content).GetComponent<C_ProfileAcc>();
                obj.set(data[i]);

                if (data[i].status == C_Enum.StatusAccount.On) obj.transform.SetSiblingIndex(0);

                news.Add(obj);
            }
        }

        for (int j = i; j < lstObjs.Count; j++)
        {
            Destroy(lstObjs[j].gameObject);
        }

        lstObjs = news;

        sc.verticalNormalizedPosition = 1;
    }
}
