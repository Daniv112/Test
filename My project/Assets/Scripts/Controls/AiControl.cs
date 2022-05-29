using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiControl : Control
{
    public override void Activate(bool value)
    {
        base.Activate(value);
        if (value)
            SelectEnemy();
    }

    protected override void SelectEnemy()
    {
        base.SelectEnemy();
        Unit enemy = battle.playerTeam.team[Random.Range(0, battle.playerTeam.team.Count)];
        battle.SelectEnemy(enemy);
        StartCoroutine(PreBattlePause());
    }

    private IEnumerator PreBattlePause() 
    {
        yield return new WaitForSeconds(1f);
        battle.Attack();
    }
}
