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
        M_Character character = new M_Character(GameManager.instance.characters[idx]);
        character.Current_ep = character.max_ep = 100;
        character.Current_hp = character.max_hp = character.hp;

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
        MainGame.instance.ShowScene("InforScene");
    }
}
