using System.Collections.Generic;
using UnityEngine;

public class PlayerSpriteUpdater : MonoBehaviour
{
    SpriteRenderer PlayerSprite;
    CharacterData characterData;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        PlayerSprite = GetComponent<SpriteRenderer>();
        characterData = GetComponent<CharacterData>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ChangeSprite(string state, int index)
    {
        switch(state)         {
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
                PlayerSprite.sprite = characterData.movementSprites[index].gameObject.GetComponent<SpriteRenderer>().sprite;
                break;
            case "standing":
                PlayerSprite.sprite = characterData.movementSprites[index].gameObject.GetComponent<SpriteRenderer>().sprite;
                break;
            case "punch":
                PlayerSprite.sprite = characterData.movementSprites[index].gameObject.GetComponent<SpriteRenderer>().sprite;
                break;
            case "kick":
                PlayerSprite.sprite = characterData.movementSprites[index].gameObject.GetComponent<SpriteRenderer>().sprite;
                break;
            case "lowHit":
                PlayerSprite.sprite = characterData.movementSprites[index].gameObject.GetComponent<SpriteRenderer>().sprite;
                break;
            case "special":
                PlayerSprite.sprite = characterData.movementSprites[index].gameObject.GetComponent<SpriteRenderer>().sprite;
                break;
            default:
                Debug.LogWarning("Unknown state: " + state);
                break;
        }

    }
}
