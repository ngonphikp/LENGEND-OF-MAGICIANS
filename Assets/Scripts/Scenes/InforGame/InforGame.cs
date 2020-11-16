using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InforGame : MonoBehaviour
{
    public static InforGame instance = null;

    [SerializeField]
    private Transform posCharacter = null;
    [SerializeField]
    private Image[] imgSkills = null;
    [SerializeField]
    private C_Profile profile = null;

    private int index = 0;
    private C_Character hero;
    private M_Character character;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    private void OnEnable()
    {
        index = GameManager.instance.idxCharacter;
        LoadCharacter();
    }

    private void LoadCharacter()
    {
        character = GameManager.instance.characters[index];

        foreach (Transform child in posCharacter)
        {
            Destroy(child.gameObject);
        }

        GameObject heroAs = Resources.Load("Prefabs/Character/" + character.id_cfg, typeof(GameObject)) as GameObject;

        // Test
        if (heroAs == null) heroAs = Resources.Load("Prefabs/Character/T1004", typeof(GameObject)) as GameObject;

        if (heroAs != null)
        {
            GameObject heroObj = Instantiate(heroAs, posCharacter);
            hero = heroObj.GetComponent<C_Character>();

            character.Current_ep = character.max_ep = 100;
            character.Current_hp = character.max_hp = character.hp;

            hero.Set(character, false);
        }

        for (int i = 0; i < GameManager.instance.herosDic[character.id_cfg].skills.Count; i++)
        {
            Sprite sprite = Resources.Load<Sprite>("Sprites/Skill/" + GameManager.instance.herosDic[character.id_cfg].skills[i]);

            if(sprite != null) imgSkills[i].sprite = Resources.Load<Sprite>("Sprites/Skill/" + GameManager.instance.herosDic[character.id_cfg].skills[i]);
        }

        profile.set(character);
    }

    public void SkillHero(int anim)
    {
        hero.Play(anim);
    }

    public void Next()
    {
        index++;
        if (index >= GameManager.instance.characters.Count) index = 0;
        LoadCharacter();
    }

    public void Previous()
    {
        index--;
        if (index <= -1) index = GameManager.instance.characters.Count - 1;
        LoadCharacter();
    }

    public void UpLevel()
    {        
        RequestCharacter.Uplevel(character.id);
    }

    public void RecUpLevel()
    {
        GameManager.instance.characters[index].lv += 1;
        GameManager.instance.characters[index].UpLevel();
        profile.set(character);
    }
}
