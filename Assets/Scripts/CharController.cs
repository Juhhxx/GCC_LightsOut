using UnityEditor.Search;
using UnityEngine;

public class CharController : MonoBehaviour
{
    [SerializeField] private Character _character;
    [SerializeField] private Renderer _characterRenderer;

    public void SetCharacter(Character c)
    {
        _character = c;
        UpdateCharacter();
    }
    private void UpdateCharacter()
    {
        _characterRenderer.material.mainTexture = _character.BattleSprite;
    }
}
