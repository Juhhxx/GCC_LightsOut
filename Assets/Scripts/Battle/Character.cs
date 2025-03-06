using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "Scriptable Objects/Character")]
public class Character : ScriptableObject
{
    public Texture BattleSprite;
    public Texture Portrait;
    public string Name;
    public int HP;
    public int Def;
    public int Attck;

    public static Character InstantiateCharacter(Character character)
    {
        Character newChar = CreateInstance<Character>();

        newChar.BattleSprite    = character.BattleSprite;        
        newChar.Portrait        = character.Portrait;
        newChar.Name            = character.Name;
        newChar.HP              = character.HP;
        newChar.Def             = character.Def;
        newChar.Attck           = character.Attck;

        return newChar;
    }
}
