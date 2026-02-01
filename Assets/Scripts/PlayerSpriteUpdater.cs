using GGJ26.StateMachine;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpriteUpdater : MonoBehaviour
{
    SpriteRenderer PlayerSprite;
    public CharacterDatabase CharacterDatabase;
    CharacterData characterData;
    public HitboxManager HitboxManager;
    public HurtboxManager HurtboxManager;
    AnimationSetController animationSetController;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        PlayerSprite = GetComponent<SpriteRenderer>();
        // For now, we take the first character
    }

    public void SetCharacterIndex(int index)
    {
        // Usa l'indice dell'array invece di cercare per characterId
        characterData = CharacterDatabase.GetCharacter(index);
        
        if (characterData == null)
        {
            Debug.LogError($"[PlayerSpriteUpdater] Personaggio non trovato all'indice {index}");
        }
        else
        {
            Debug.Log($"[PlayerSpriteUpdater] Personaggio caricato: {characterData.characterName} (indice: {index})");
        }

        animationSetController = GetComponent<AnimationSetController>();
        animationSetController.SetAnimationSet(index);
    }

    /// <summary>
    /// Imposta direttamente un CharacterData (usato per la trasformazione)
    /// </summary>
    public void SetCharacterData(CharacterData newCharacterData, int animationSetIndex)
    {
        characterData = newCharacterData;
        
        if (characterData == null)
        {
            Debug.LogError($"[PlayerSpriteUpdater] CharacterData è null durante trasformazione!");
            return;
        }
        
        Debug.Log($"[PlayerSpriteUpdater] Trasformazione in: {characterData.characterName}");

        if (animationSetController == null)
            animationSetController = GetComponent<AnimationSetController>();
        
        animationSetController.SetAnimationSet(animationSetIndex);
        
        // Aggiorna lo sprite corrente con l'idle del nuovo personaggio
        if (characterData.idleSprites != null && characterData.idleSprites.Count > 0)
        {
            ChangeSprite("idle", 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeSprite(string state, int index,bool changeOnlyHitBox = false)
    {
        // Debug: verifica che characterData sia valido
        if (characterData == null)
        {
            Debug.LogWarning($"[PlayerSpriteUpdater] ChangeSprite chiamato ma characterData è NULL! State: {state}, Index: {index}, GameObject: {gameObject.name}");
            return;
        }
        if (animationSetController)
            animationSetController.animator.enabled = false;

        try
        {
            // Debug: verifica che i manager siano assegnati
            if (HitboxManager == null)
            {
                Debug.LogError($"[PlayerSpriteUpdater] HitboxManager è NULL! GameObject: {gameObject.name}");
                return;
            }
            if (HurtboxManager == null)
            {
                Debug.LogError($"[PlayerSpriteUpdater] HurtboxManager è NULL! GameObject: {gameObject.name}");
                return;
            }
            
            HitboxManager.ClearCollider();
            HurtboxManager.ClearCollider();
            switch (state)
            {
                case "block":

                    
                    
                    PlayerSprite.sprite = characterData.blockSprites[index].gameObject.GetComponent<SpriteRenderer>().sprite;
                    HitboxManager.SetupColliderFromDB(GetBoxCollider2D("HitBox", characterData.blockSprites[index].transform));
                    HurtboxManager.SetupColliderFromDB(GetBoxCollider2D("HurtBOx", characterData.blockSprites[index].transform));
                    break;
                case "movement":
                    animationSetController.animator.enabled = true;
                    animationSetController.animator.SetBool("isMoving", true);
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
                    if(!changeOnlyHitBox)
                        PlayerSprite.sprite = characterData.punchSprites[index].gameObject.GetComponent<SpriteRenderer>().sprite;
                    HitboxManager.SetupColliderFromDB(GetBoxCollider2D("HitBox", characterData.punchSprites[index].transform));
                    HurtboxManager.SetupColliderFromDB(GetBoxCollider2D("HurtBOx", characterData.punchSprites[index].transform));
                    break;
                case "kick":
                    if (!changeOnlyHitBox)
                        PlayerSprite.sprite = characterData.kickSprites[index].gameObject.GetComponent<SpriteRenderer>().sprite;
                    HitboxManager.SetupColliderFromDB(GetBoxCollider2D("HitBox", characterData.kickSprites[index].transform));
                    HurtboxManager.SetupColliderFromDB(GetBoxCollider2D("HurtBOx", characterData.kickSprites[index].transform));
                    break;
                case "lowHit":
                    if (!changeOnlyHitBox)
                        PlayerSprite.sprite = characterData.lowhitSprites[index].gameObject.GetComponent<SpriteRenderer>().sprite;
                    HitboxManager.SetupColliderFromDB(GetBoxCollider2D("HitBox", characterData.lowhitSprites[index].transform));
                    HurtboxManager.SetupColliderFromDB(GetBoxCollider2D("HurtBOx", characterData.lowhitSprites[index].transform));
                    break;
                case "special":
                    if (!changeOnlyHitBox)
                        PlayerSprite.sprite = characterData.specialSprites[index].gameObject.GetComponent<SpriteRenderer>().sprite;
                    HitboxManager.SetupColliderFromDB(GetBoxCollider2D("HitBox", characterData.specialSprites[index].transform));
                    HurtboxManager.SetupColliderFromDB(GetBoxCollider2D("HurtBOx", characterData.specialSprites[index].transform));
                    break;
                case "idle":
                    animationSetController.animator.enabled = true;
                    animationSetController.animator.SetBool("isMoving", false);
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

    internal void FlipSprite()
    {
        PlayerSprite.flipX = !PlayerSprite.flipX;
    }
}
