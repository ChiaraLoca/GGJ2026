using UnityEngine;

[System.Serializable]
public class CharacterData
{
    public string characterName;
    public Sprite characterImage;
    public Sprite characterPortrait; // Immagine ritratto per la selezione
    public int characterId;

    public Sprite characterSpecialSprite; // Sprite speciale per mosse o trasformazioni
    
    [TextArea(2, 4)]
    public string description; // Descrizione opzionale del personaggio
    
    // Stats base del personaggio (opzionali)
    [Range(1, 10)] public int speed = 5;
    [Range(1, 10)] public int power = 5;
    [Range(1, 10)] public int defense = 5;
}