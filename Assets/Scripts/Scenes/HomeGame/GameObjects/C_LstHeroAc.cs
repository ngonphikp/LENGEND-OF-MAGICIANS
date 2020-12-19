using MEC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_LstHeroAc : MonoBehaviour
{
    [SerializeField]
    private Transform content = null;
    [SerializeField]
    private GameObject Prb = null;

    [SerializeField]
    private List<C_CharacterAcEl> Objs = new List<C_CharacterAcEl>();

    public IEnumerator<float> _set()
    {
        int i;
        int count = 0;
        for (i = 0; i < GameManager.instance.characters.Count; i++)
        {
            if (GameManager.instance.characters[i].idx != -1)
            {
                if (count < Objs.Count)
                {
                    Timing.RunCoroutine(Objs[count]._set(i));
                    Objs[count].gameObject.SetActive(true);
                }
                else
                {
                    C_CharacterAcEl obj = Instantiate(Prb, content).GetComponent<C_CharacterAcEl>();
                    Timing.RunCoroutine(obj._set(i));
                    Objs.Add(obj);
                }
                count++;
            }
                
        }

        for (int j = count; j < Objs.Count; j++)
        {
            Objs[j].gameObject.SetActive(false);
        }

        yield break;
    }
}
