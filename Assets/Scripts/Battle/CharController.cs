using UnityEditor.Search;
using UnityEngine;

public class CharController : MonoBehaviour
{
    [SerializeField] private Character _character;
    public Character Character => _character;
    [SerializeField] private Renderer _characterRenderer;
    private bool _isDefending;
    public bool IsDefending => _isDefending;

    public void SetCharacter(Character c)
    {
        _character = c;
        UpdateCharacter();
    }
    private void UpdateCharacter()
    {
        _characterRenderer.material.mainTexture = _character.BattleSprite;
    }
    public void Attack(CharController other)
    {
        int finalAttack = other.IsDefending ? 
        _character.Attck - other.Character.Def : _character.Attck;

        other.Character.HP -= finalAttack;
    }
    public void Defend()
    {
        _isDefending = true;
    }
    public void EndTurn()
    {
        _isDefending = false;
    }
}
