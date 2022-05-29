using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Team
{
    public List<Unit> team;
    public Control control;
}

public class Battle : MonoBehaviour
{
    public Team playerTeam;
    public Team enemyTeam;
    [SerializeField] private GameObject blackout;
    [SerializeField] private GameObject enemyPoint;
    [SerializeField] private GameObject playerPoint;
    [SerializeField] private GameObject newRoundText;
    private Unit currentEnemy;
    private Unit currentCharacter;
    private Team currentTeam;
    [SerializeField]private List<Unit> activeUnits;

    private void Start()
    {
        currentTeam = playerTeam;
        currentTeam.control.Activate(true);
        activeUnits = new List<Unit>(currentTeam.team);
        SelectCharacter(currentTeam);
    }

    private Team ChangeTeam()
    {
        if (currentTeam == playerTeam)
            currentTeam = enemyTeam;
        else
            currentTeam = playerTeam;
        activeUnits = new List<Unit>(currentTeam.team); ;
        return currentTeam;
    }

    private void SelectCharacter(Team team)
    {
        if (activeUnits.Count == 0)
        {
            StartCoroutine(NewRoundTimer());
            return;
        }
        int i = UnityEngine.Random.Range(0, activeUnits.Count);
        currentCharacter = activeUnits[i];
        currentCharacter.Select();
        currentTeam.control.Activate(true);
        activeUnits.RemoveAt(i);
    }

    public void SelectEnemy(Unit unit)
    {
        if (currentEnemy != null)
            currentEnemy.Deactivate();
        currentEnemy = unit;
        currentEnemy.Select();
    }

    public void Attack()
    {
        currentCharacter.Attack(currentEnemy);
        blackout.SetActive(true);
        if (currentTeam == playerTeam)
        {
            currentCharacter.transform.position = playerPoint.transform.position;
            currentEnemy.transform.position = enemyPoint.transform.position;
        }
        else
        {
            currentCharacter.transform.position = enemyPoint.transform.position;
            currentEnemy.transform.position = playerPoint.transform.position;
        }
        currentTeam.control.Activate(false);

        StartCoroutine(BattleTimer());
    }

    private IEnumerator BattleTimer()
    {
        yield return new WaitForSeconds(2f);
        Deactivate();
    }

    public void Deactivate()
    {
        blackout.SetActive(false);
        currentCharacter.Deactivate();
        currentEnemy.Deactivate();
        currentCharacter = null;
        currentEnemy = null;
        newRoundText.SetActive(false);
        SelectCharacter(currentTeam);
    }

    private IEnumerator NewRoundTimer()
    {
        newRoundText.SetActive(true);
        yield return new WaitForSeconds(1f);
        newRoundText.SetActive(false);
        SelectCharacter(ChangeTeam());
    }
}
