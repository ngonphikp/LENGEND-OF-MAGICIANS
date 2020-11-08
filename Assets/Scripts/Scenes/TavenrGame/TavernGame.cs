using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class TavernGame : MonoBehaviour
{
    public static TavernGame instance = null;

    private Dictionary<C_Enum.CardType, C_TVCard> cardsDic = new Dictionary<C_Enum.CardType, C_TVCard>();

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    [System.Obsolete]
    public void ReqCard(C_TVCard card)
    {
        Debug.Log("ReqCard: " + card.type);

        cardsDic.Add(card.type, card);
        
        if (GameManager.instance.test)
        {
            M_Character character = new M_Character();
            character.id = GameManager.instance.characters[GameManager.instance.characters.Count - 1].id + Random.RandomRange(99, 9999);
            character.id_cfg = "T100" + UnityEngine.Random.Range(2, 8);

            character.idx = -1;

            character.lv = 1;
            character.UpdateById();
            character.Current_ep = character.max_ep = 100;
            character.Current_hp = character.max_hp = character.hp;
            character.UpdateLevel();
            character.type = C_Enum.CharacterType.Hero;

            RecCard(card.type, character);
        }
        else RequestAccount.Tavern(card.type);
    }

    public void RecCard(C_Enum.CardType type, M_Character character)
    {
        Debug.Log("RecCard: " + type);        

        cardsDic[type].Rec(character);
        cardsDic.Remove(type);
    }
}
