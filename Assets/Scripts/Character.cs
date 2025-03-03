using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "Scriptable Objects/Character")]
public class Character : ScriptableObject
{
    public Texture BattleSprite;
    public Texture Portrait;
    public int HP;
    public int Def;
    public int Attck;
}
