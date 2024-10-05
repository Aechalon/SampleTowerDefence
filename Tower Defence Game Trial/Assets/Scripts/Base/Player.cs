using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int _availableArchers = 20;
    [SerializeField] private int _availableCanons = 2;
    [SerializeField] private List<Hero.GateHero> _availableHero = new List<Hero.GateHero>();

    public int GetAvailableUnit(int unitType) => unitType == 0 ?
        _availableArchers : _availableCanons;

    public List<Hero.GateHero> GetAvailabeHero() => _availableHero;

    public void ModifyUnit(int[] type, List<Hero.GateHero> heroes)
    {
        _availableArchers -= type[0];
        _availableCanons -= type[1];
        _availableHero = heroes;
    }

    public void UseHero(Hero.GateHero hero)
    {
        if (_availableHero.Contains(hero))
            _availableHero.Remove(hero);
    }
    public void ReturnHero(Hero.GateHero hero)
    {
        if (!_availableHero.Contains(hero))
            _availableHero.Add(hero);
    }
}
