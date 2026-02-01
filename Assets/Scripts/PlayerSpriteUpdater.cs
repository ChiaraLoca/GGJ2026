using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerSpriteUpdater : MonoBehaviour
{
    SpriteRenderer PlayerSprite;
    public CharacterDatabase CharacterDatabase;
    CharacterData characterData;
    public HitboxManager HitboxManager;
    public HurtboxManager HurtboxManager;

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
                    HitboxManager.SetupColliderFromDB(GetBoxCollider2D("HitBox", characterData.blockSprites[index].transform));
                    HurtboxManager.SetupColliderFromDB(GetBoxCollider2D("HurtBOx", characterData.blockSprites[index].transform));
                    break;
                case "movement":
                    PlayerSprite.sprite = characterData.movementSprites[index].gameObject.GetComponent<SpriteRenderer>().sprite;
                    HitboxManager.SetupColliderFromDB(GetBoxCollider2D("HitBox", characterData.movementSprites[index].transform));
                    HurtboxManager.SetupColliderFromDB(GetBoxCollider2D("HurtBOx", characterData.movementSprites[index].transform));
                    break;
                case "crouch":
                    PlayerSprite.sprite = characterData.crouchSprites[index].gameObject.GetComponent<SpriteRenderer>().sprite;
                    HitboxManager.SetupColliderFromDB(GetBoxCollider2D("HitBox", characterData.crouchSprites[index].transform));
                    HurtboxManager.SetupColliderFromDB(GetBoxCollider2D("HurtBOx", characterData.crouchSprites[index].transform));
                    break;
                case "jump":
                    PlayerSprite.sprite = characterData.jumpSprites[index].gameObject.GetComponent<SpriteRenderer>().sprite;
                    HitboxManager.SetupColliderFromDB(GetBoxCollider2D("HitBox", characterData.jumpSprites[index].transform));
                    HurtboxManager.SetupColliderFromDB(GetBoxCollider2D("HurtBOx", characterData.jumpSprites[index].transform));
                    break;
                case "hit":
                    PlayerSprite.sprite = characterData.hitSprites[index].gameObject.GetComponent<SpriteRenderer>().sprite;
                    HitboxManager.SetupColliderFromDB(GetBoxCollider2D("HitBox", characterData.hitSprites[index].transform));
                    HurtboxManager.SetupColliderFromDB(GetBoxCollider2D("HurtBOx", characterData.hitSprites[index].transform));
                    break;
                case "down":
                    PlayerSprite.sprite = characterData.downSprites[index].gameObject.GetComponent<SpriteRenderer>().sprite;
                    HitboxManager.SetupColliderFromDB(GetBoxCollider2D("HitBox", characterData.downSprites[index].transform));
                    HurtboxManager.SetupColliderFromDB(GetBoxCollider2D("HurtBOx", characterData.downSprites[index].transform));
                    break;
                case "standing":
                    PlayerSprite.sprite = characterData.standingSprites[index].gameObject.GetComponent<SpriteRenderer>().sprite;
                    HitboxManager.SetupColliderFromDB(GetBoxCollider2D("HitBox", characterData.standingSprites[index].transform));
                    HurtboxManager.SetupColliderFromDB(GetBoxCollider2D("HurtBOx", characterData.standingSprites[index].transform));
                    break;
                case "punch":
                    PlayerSprite.sprite = characterData.punchSprites[index].gameObject.GetComponent<SpriteRenderer>().sprite;
                    HitboxManager.SetupColliderFromDB(GetBoxCollider2D("HitBox", characterData.punchSprites[index].transform));
                    HurtboxManager.SetupColliderFromDB(GetBoxCollider2D("HurtBOx", characterData.punchSprites[index].transform));
                    break;
                case "kick":
                    PlayerSprite.sprite = characterData.kickSprites[index].gameObject.GetComponent<SpriteRenderer>().sprite;
                    HitboxManager.SetupColliderFromDB(GetBoxCollider2D("HitBox", characterData.kickSprites[index].transform));
                    HurtboxManager.SetupColliderFromDB(GetBoxCollider2D("HurtBOx", characterData.kickSprites[index].transform));
                    break;
                case "lowHit":
                    PlayerSprite.sprite = characterData.lowhitSprites[index].gameObject.GetComponent<SpriteRenderer>().sprite;
                    HitboxManager.SetupColliderFromDB(GetBoxCollider2D("HitBox", characterData.lowhitSprites[index].transform));
                    HurtboxManager.SetupColliderFromDB(GetBoxCollider2D("HurtBOx", characterData.lowhitSprites[index].transform));
                    break;
                case "special":
                    PlayerSprite.sprite = characterData.specialSprites[index].gameObject.GetComponent<SpriteRenderer>().sprite;
                    HitboxManager.SetupColliderFromDB(GetBoxCollider2D("HitBox", characterData.specialSprites[index].transform));
                    HurtboxManager.SetupColliderFromDB(GetBoxCollider2D("HurtBOx", characterData.specialSprites[index].transform));
                    break;
                case "idle":
                    PlayerSprite.sprite = characterData.idleSprites[index].gameObject.GetComponent<SpriteRenderer>().sprite;
                    HitboxManager.SetupColliderFromDB(GetBoxCollider2D("HitBox", characterData.idleSprites[index].transform));
                    HurtboxManager.SetupColliderFromDB(GetBoxCollider2D("HurtBOx", characterData.idleSprites[index].transform));
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

    BoxCollider2D GetBoxCollider2D(string childname, Transform parent)
    {
        Transform child = parent.Find(childname);

        if (child != null)
        {
            BoxCollider2D collider = child.GetComponent<BoxCollider2D>();
            return collider;
        }

        return null;
    }
}
