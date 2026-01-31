using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerSpriteUpdater : MonoBehaviour
{
    SpriteRenderer PlayerSprite;
    public CharacterDatabase CharacterDatabase;
    CharacterData characterData;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        PlayerSprite = GetComponent<SpriteRenderer>();
        // For now, we take the first character
    }

    public void SetCharacterIndex(int index)
    {
        
        characterData = CharacterDatabase.GetAllCharacters().FirstOrDefault(obj => obj.characterId == index);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeSprite(string state, int index)
    {
        try
        {
            switch (state)
            {
                case "block":
                    PlayerSprite.sprite = characterData.blockSprites[index].gameObject.GetComponent<SpriteRenderer>().sprite;
                    break;
                case "movement":
                    PlayerSprite.sprite = characterData.movementSprites[index].gameObject.GetComponent<SpriteRenderer>().sprite;
                    break;
                case "crouch":
                    PlayerSprite.sprite = characterData.crouchSprites[index].gameObject.GetComponent<SpriteRenderer>().sprite;
                    break;
                case "jump":
                    PlayerSprite.sprite = characterData.movementSprites[index].gameObject.GetComponent<SpriteRenderer>().sprite;
                    break;
                case "hit":
                    PlayerSprite.sprite = characterData.hitSprites[index].gameObject.GetComponent<SpriteRenderer>().sprite;
                    break;
                case "down":
                    PlayerSprite.sprite = characterData.downSprites[index].gameObject.GetComponent<SpriteRenderer>().sprite;
                    break;
                case "standing":
                    PlayerSprite.sprite = characterData.standingSprites[index].gameObject.GetComponent<SpriteRenderer>().sprite;
                    break;
                case "punch":
                    PlayerSprite.sprite = characterData.punchSprites[index].gameObject.GetComponent<SpriteRenderer>().sprite;
                    break;
                case "kick":
                    PlayerSprite.sprite = characterData.kickSprites[index].gameObject.GetComponent<SpriteRenderer>().sprite;
                    break;
                case "lowHit":
                    PlayerSprite.sprite = characterData.lowhitSprites[index].gameObject.GetComponent<SpriteRenderer>().sprite;
                    break;
                case "special":
                    PlayerSprite.sprite = characterData.specialSprites[index].gameObject.GetComponent<SpriteRenderer>().sprite;
                    break;
                default:
                    Debug.LogWarning("Unknown state: " + state);
                    break;
            }

        }
        catch (System.Exception e)
        {
            Debug.LogWarning("Error changing sprite for state: " + state + " with index: " + index + ". Exception: " + e.Message);

        }
        
    }
}
