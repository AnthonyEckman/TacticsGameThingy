using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NecroMancer : NPCMove
{

    private int summonTimer = 2;
    public GameObject summon;
    void Awake()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position, transform.forward);
        if (health <= 0)
        {
            Die();
        }
        if (!turn)
        {
            return;
        }

        if (!moving)
        {

            FindNearestTarget();
            CalculatePath();
            FindSelectableTiles();

            actualTargetTile.target = true;
        }
        else if (!hasMoved)
        {

            Move();
        }
        else
        {
            
            if (summonTimer == 0)
            {
                summonTimer = 3;
                Summon();
                

            }
            else
            {
                summonTimer -= 1;
                Refresh();
                BetterTurnManager.EndTurn();
            }

        }


    }

    public void Summon()
    {
        Instantiate(summon, new Vector3(transform.position.x + 1, transform.position.y, transform.position.z), Quaternion.identity);
        Instantiate(summon, new Vector3(transform.position.x - 1, transform.position.y, transform.position.z), Quaternion.identity);
        Refresh();
        BetterTurnManager.EndTurn();
    }
}
