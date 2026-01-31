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

    public List<Sprite> movementSprites;
    public List<Sprite> blockSprites;
    public List<Sprite> crouchSprites;
    public List<Sprite> jumpSprites;
    public List<Sprite> hitSprites;
    public List<Sprite> downSprites;
    public List<Sprite> standingSprites;
    public List<Sprite> punchSprites;
    public List<Sprite> kickSprites;
    public List<Sprite> lowhitSprites;
    public List<Sprite> specialSprites;

    public Sprite characterSpecialSprite; // Sprite speciale per mosse o trasformazioni
    
    [TextArea(2, 4)]
    public string description; // Descrizione opzionale del personaggio
    
    // Stats base del personaggio (opzionali)
    [Range(1, 10)] public int speed = 5;
    [Range(1, 10)] public int power = 5;
    [Range(1, 10)] public int defense = 5;
}