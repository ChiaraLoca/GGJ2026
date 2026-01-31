using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterData
{
    public string characterName;
    public Sprite characterImage;
    public Sprite characterPortrait; // Immagine ritratto per la selezione
    public int characterId;

    public List<GameObject> movementSprites;
    public List<GameObject> blockSprites;
    public List<GameObject> crouchSprites;
    public List<GameObject> jumpSprites;
    public List<GameObject> hitSprites;
    public List<GameObject> downSprites;
    public List<GameObject> standingSprites;
    public List<GameObject> punchSprites;
    public List<GameObject> kickSprites;
    public List<GameObject> lowhitSprites;
    public List<GameObject> specialSprites;

    public Sprite characterSpecialSprite; // Sprite speciale per mosse o trasformazioni
    
    [TextArea(2, 4)]
    public string description; // Descrizione opzionale del personaggio
    
    // Stats base del personaggio (opzionali)
    [Range(1, 10)] public int speed = 5;
    [Range(1, 10)] public int power = 5;
    [Range(0, 1000)] public int hp = 5;
    
    //due metodi public di callback che si attivano quando viene colpito il personaggio e quando colpisce
     public Action<CharacterData> OnHit;
     public Action<CharacterData> OnAttack;


}