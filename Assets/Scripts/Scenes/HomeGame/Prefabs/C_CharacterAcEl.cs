using MEC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_CharacterAcEl : MonoBehaviour
{
    [SerializeField]
    private Transform content = null;

    private int idx;

    public IEnumerator<float> _set(int idx)
    {
        foreach (Transform item in content)
        {
            Destroy(item.gameObject);
        }

        M_Character character = new M_Character(GameManager.instance.characters[idx]);

        this.idx = idx;

        GameObject heroAs = Resources.Load("Prefabs/Character/" + character.id_cfg, typeof(GameObject)) as GameObject;

        // Test
        if (heroAs == null) heroAs = Resources.Load("Prefabs/Character/T1004", typeof(GameObject)) as GameObject;

        if (heroAs != null)
        {
            GameObject obj = Instantiate(heroAs, content);
            C_Character hero = obj.GetComponent<C_Character>();
            hero.Set(character);
        }

        yield return Timing.WaitForOneFrame;
    }

    public void ClickHero()
    {
        GameManager.instance.idxCharacter = idx;
        MainGame.instance.ShowScene(C_Enum.MainGame.InforScene);
    }
}
