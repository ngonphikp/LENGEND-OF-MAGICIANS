using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class C_CharacterAvEl : MonoBehaviour
{
    [SerializeField]
    private C_Avatar av = null;
    [SerializeField]
    private GameObject active = null;

    public bool isActive = false;

    public M_Character character;

    public void set(M_Character character)
    {
        this.character = character;

        av.set(character);

        if (character.idx != -1) Active();
    }

    public void ClickHero()
    {
        Debug.Log("ClickHero: " + character.id + " => " + isActive);
        if (ArrangeGame.instance.countActive >= C_Params.maxActive)
        {
            Debug.LogWarning("Full Active");
            return;
        }

        ArrangeGame.instance.Active(character.id);
        Active();
    }

    public void Active()
    {
        isActive = true;
        this.GetComponent<Button>().interactable = false;

        active.SetActive(isActive);
    }

    public void UnActive()
    {
        isActive = false;
        this.GetComponent<Button>().interactable = true;

        active.SetActive(isActive);
    }
}
