using UnityEngine;

[CreateAssetMenu(fileName = "CharacterDatabase", menuName = "Game/Character Database")]
public class CharacterDatabase : ScriptableObject
{
    [SerializeField] private CharacterData[] characters;

    public int CharacterCount => characters.Length;

    public CharacterData GetCharacter(int index)
    {
        if (index < 0 || index >= characters.Length)
            return null;

        return characters[index];
    }

    public CharacterData[] GetAllCharacters()
    {
        return characters;
    }
}
