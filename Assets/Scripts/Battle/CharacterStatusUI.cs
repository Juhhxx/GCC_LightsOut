using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterStatusUI : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private RawImage _portaitImage;
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private Image _healthBar;
    [SerializeField] private TextMeshProUGUI _healthText;
    private Character _character;
    private int _fullHealth;

    public void SetCharacter(Character c)
    {
        _character = c;
        _fullHealth = c.HP;
        UpdatePortrait(c.Portrait);
        UpdateCharacterName(c.Name);
        UpdateHealth(c.HP);
    }
    private void Update()
    {
        if (_character != null)
            UpdateHealth(_character.HP);
    }
    private void UpdatePortrait(Texture tex) => _portaitImage.texture = tex;
    private void UpdateCharacterName(string name) => _nameText.text = name;
    private void UpdateHealth(int curHealth)
    {
        float healthValue = Mathf.InverseLerp(0f, _fullHealth, curHealth);

        _healthBar.fillAmount = healthValue;

        _healthText.text = $"{curHealth} / {_fullHealth}";
    }
}
