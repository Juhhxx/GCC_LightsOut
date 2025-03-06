using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    [SerializeField] private List<Character> _playerParty;
    [SerializeField] private List<Character> _enemyParty;
    [SerializeField] private LayoutGroup3D _playerPartyGroup;
    [SerializeField] private LayoutGroup3D _enemyPartyGroup;
    [SerializeField] private Transform _playerPartyUI;
    [SerializeField] private GameObject _characterPrefab;
    [SerializeField] private GameObject _playerUIPerefab;
    [SerializeField] private GameObject _actionUI;
    private int _turn;

    private void Start()
    {
        StartBattle();
    }

    private void StartBattle()
    {
        SetPlayer();
        SetEnemy();
        PlayerTurn();
    }
    private void SetPlayer()
    {
        foreach (Character ch in _playerParty)
        {
            InstantiateCharacter(ch, _playerPartyGroup);
            InstantiateCharacterUI(ch, _playerPartyUI);
        }
    }
    private void SetEnemy()
    {
        foreach (Character ch in _enemyParty)
        {
            InstantiateCharacter(ch, _enemyPartyGroup);
            InstantiateCharacterUI(ch, _playerPartyUI);
        }
    }
    private void InstantiateCharacter(Character ch, LayoutGroup3D parent)
    {
        GameObject newChar = Instantiate(_characterPrefab);

        CharController ctrl = newChar.GetComponent<CharController>();

        ctrl.SetCharacter(Character.InstantiateCharacter(ch));

        Debug.Log($"{parent.name}, {newChar.name}");

        parent.AddChildren(newChar);
    }
    private void InstantiateCharacterUI(Character ch, Transform parent)
    {
        GameObject newUI = Instantiate(_playerUIPerefab, parent);

        CharacterStatusUI charUI = newUI.GetComponent<CharacterStatusUI>();

        charUI.SetCharacter(ch);
    }
    private void PlayerTurn()
    {
        // for (int i = 0; i < transform.childCount - 1; i++)
        // {
        //     CharController c = transform.GetChild(i).GetComponent<CharController>();
        //     StartCoroutine(CharacterTurn(c));
        // }
        CharController c = transform.GetChild(0).GetComponent<CharController>();

        GameObject actionUI = Instantiate(_actionUI, transform);

        Vector3 charPos = _playerPartyGroup.transform.GetChild(0).transform.position;

        Vector3 correctedPos = Camera.main.WorldToScreenPoint(charPos);

        actionUI.GetComponent<RectTransform>().position = correctedPos;

        Button attckBtt = actionUI.transform.GetChild(0).GetComponent<Button>();

        attckBtt.onClick.AddListener(()=>DoAttack(c));

        Button defBtt = actionUI.transform.GetChild(1).GetComponent<Button>();

        defBtt.onClick.AddListener(() => DoDefense(c));
    }
    private IEnumerator CharacterTurn(CharController c)
    {
        GameObject actionUI = Instantiate(_actionUI, transform);

        Vector3 charPos = _playerPartyGroup.transform.GetChild(0).transform.position;

        Vector3 correctedPos = Camera.main.WorldToScreenPoint(charPos);

        actionUI.GetComponent<RectTransform>().position = correctedPos;

        Button attckBtt = actionUI.transform.GetChild(0).GetComponent<Button>();

        attckBtt.onClick.AddListener(()=>DoAttack(c));

        Button defBtt = actionUI.transform.GetChild(0).GetComponent<Button>();

        defBtt.onClick.AddListener(() => DoDefense(c));

        yield return new WaitForEndOfFrame();
    }
    private void DoAttack(CharController character)
    {
        character.Attack(_enemyPartyGroup.transform.GetChild(0).GetComponent<CharController>());
    }
    private void DoDefense(CharController character)
    {
        character.Defend();
    }

}
