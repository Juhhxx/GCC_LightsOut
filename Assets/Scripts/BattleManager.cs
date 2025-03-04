using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    [SerializeField] private List<Character> _playerParty;
    [SerializeField] private List<Character> _enemyParty;
    [SerializeField] private LayoutGroup3D _playerPartyGroup;
    [SerializeField] private LayoutGroup3D _enemyPartyGroup;
    [SerializeField] private GameObject _characterPrefab;
    private int _turn;

    private void Start()
    {
        StartBattle();
    }

    private void StartBattle()
    {
        SetPlayer();
    }
    private void SetPlayer()
    {
        foreach (Character ch in _playerParty)
        {
            GameObject newChar = Instantiate(_characterPrefab);

            CharController ctrl = newChar.GetComponent<CharController>();

            ctrl.SetCharacter(Character.InstantiateCharacter(ch));

            Debug.Log($"{_playerPartyGroup.name}, {newChar.name}");

            _playerPartyGroup.AddChildren(newChar);

        }
    }
}
