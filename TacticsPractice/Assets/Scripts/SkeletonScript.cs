using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonScript : NPCMove
{
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
        Debug.Log(moving);
        if (!moving)
        {
            
            FindNearestTarget();
            CalculatePath();
            FindSelectableTiles();

            actualTargetTile.target = true;
        }
        else if(!hasMoved)
        {

            Move();
        }
        else
        {
            FindNearestTarget();
            if (InRange(target))
            {
                Attack(target);
                
            }
            else
            {
                Refresh();
                BetterTurnManager.EndTurn();
            }
            
        }


    }

    public void Attack(GameObject target)
    {
        CalculateHeading(target.transform.position);
        transform.forward = heading;
        target.GetComponent<TacticsMove>().TakeDamage(attack);
        Refresh();
        BetterTurnManager.EndTurn();

    }
}
