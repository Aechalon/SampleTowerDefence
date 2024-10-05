using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UnitHandler : MonoBehaviour
{

    public Gate Gate {  get; private set; }     
    public Player Player {  get; private set; }
    public int[] CurrentPlayerUnits = new int[2];
    public int[] CurrentUnits = new int[2];

    public UIUnit[] UIUnits = new UIUnit[4];
    public TextMeshProUGUI TextTitle;
    public TMP_Dropdown HeroDropDown { get; private set; }
    public List<Hero.GateHero> CurrentHeroes{ get; private set; }
    public Hero.GateHero CurrentSelectedHero { get; private set; }
    private Unit_OnChanged _onChanged;
    public delegate void Unit_OnChanged();

    [HideInInspector]
    public int[] MaxUnit = new int[2];
    private void Awake()
    {
        Player = FindObjectOfType<Player>();
   
        UIUnits = GetComponentsInChildren<UIUnit>();
        HeroDropDown = GetComponentInChildren<TMP_Dropdown>();
        CurrentUnits = new int[2];
        GetCurrentPlayerUnits();
        SetUpHandler();
        _onChanged += OnUnitModify;
        _onChanged();

    }
    private void ModifyUnit(int unitType, int amount)
    {
        CurrentPlayerUnits[unitType] -= amount;
        CurrentUnits[unitType] += amount;
        _onChanged();
    }

    private void GetCurrentPlayerUnits()
    {
        CurrentPlayerUnits = new int[2];
        for (int i = 0; i < CurrentPlayerUnits.Length; i++)
            CurrentPlayerUnits[i] = Player.GetAvailableUnit(i);
        CurrentHeroes = Player.GetAvailabeHero();
        ResetHeroSelection();
   
    }
    private void GetCurrentGateUnits()
    {
        CurrentUnits = new int[2];
        for (int i = 0; i < CurrentUnits.Length; i++)
            CurrentUnits[i] = Gate.GetAvailableUnit(i);
        _onChanged();
    }

    private void SetUpHandler()
    {   
 
        for(int i = 0;i < UIUnits.Length;i++)
        {
            UIUnits[i].GetButton(true).onClick.RemoveAllListeners();
            UIUnits[i].GetButton(false).onClick.RemoveAllListeners();
        }
      
       UIUnits[0].GetButton(true).onClick.AddListener(() => ModifyUnit(0, 1));
       UIUnits[0].GetButton(false).onClick.AddListener(() => ModifyUnit(0, -1));
       UIUnits[1].GetButton(true).onClick.AddListener(() => ModifyUnit(1, 1));
       UIUnits[1].GetButton(false).onClick.AddListener(() => ModifyUnit(1, -1));
       HeroDropDown.onValueChanged.AddListener(OnDropDownChange);
    }

    public void SetSelectedGate(Gate gate, string name)
    {
        Gate = gate;
        TextTitle.text = name;
        GetCurrentPlayerUnits();
        GetCurrentGateUnits();

    }

    public void OnDropDownChange(int i)
    {
        string heroName = HeroDropDown.options[i].text;
        CurrentSelectedHero = (Hero.GateHero)Enum.Parse(typeof(Hero.GateHero), heroName);
    }
    public void SetRestriction(int maxArcher, int maxCanon)
    {
        MaxUnit[0] = maxArcher; MaxUnit[1] = maxCanon;
        _onChanged();
    }

    public void OnUnitModify()
    {
        for (int i = 0; i < UIUnits.Length; i++)
        {
            bool AddButtonInteractive = (CurrentPlayerUnits[i] > 0) && CurrentUnits[i] < MaxUnit[i];
            bool RemoveButtonInteractive = CurrentUnits[i] > 0 ;
            UIUnits[i].GetButton(true).interactable =  AddButtonInteractive;  
            UIUnits[i].GetButton(false).interactable = RemoveButtonInteractive;
            UIUnits[i].GetInputField().text = CurrentUnits[i].ToString();  
        }
      

    }
    public void FinalizeUnitSelection()
    {
        if (Gate == null) return;
        Gate.SetUnit(CurrentUnits, CurrentSelectedHero);

        if (CurrentSelectedHero != Hero.GateHero.NoHero)
            CurrentHeroes.Remove(CurrentSelectedHero);
        Player.ModifyUnit(CurrentUnits, CurrentHeroes);

        ResetHeroSelection();
        _onChanged();
        gameObject.SetActive(false);
    }
    public void RevertChanges()
    {
        CurrentHeroes = new List<Hero.GateHero>();
        _onChanged();
        HeroDropDown.ClearOptions();
        ResetHeroSelection();
        gameObject.SetActive(false);
    }
    private void ResetHeroSelection()
    {
        HeroDropDown.ClearOptions();
        CurrentHeroes = Player.GetAvailabeHero();
        if(!CurrentHeroes.Contains(Hero.GateHero.NoHero))
        CurrentHeroes.Add(Hero.GateHero.NoHero);
        List<string> HeroToString = new List<string>();
        foreach (Hero.GateHero hero in CurrentHeroes)
            HeroToString.Add(hero.ToString());

        HeroDropDown.AddOptions(HeroToString);

    }
}
