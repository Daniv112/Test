using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : Control
{
    [SerializeField] private GameObject buttons;

    public override void Activate(bool value)
    {
        base.Activate(value);
    }

    protected override void SelectEnemy()
    {
        base.SelectEnemy();
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        if (hit.collider != null && hit.collider.GetComponent<Enemy>() != null)
        {
            Unit enemy = hit.collider.GetComponent<Unit>();
            battle.SelectEnemy(enemy);
            buttons.SetActive(true);
        }
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && active)
            SelectEnemy();
    }
}
