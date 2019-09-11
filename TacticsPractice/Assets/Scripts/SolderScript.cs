using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolderScript : PlayerMove
{

    private void Awake()
    {
        Init();
    }

    private void Update()
    {

        Debug.DrawRay(transform.position, transform.forward);

        if (!turn)
        {
            return;
        }

        if (!moving && !hasMoved)
        {
            FindSelectableTiles();
            CheckMouse();
        }
        else if(!hasMoved)
        {
            Move();
        }
        else
        {
            CheckMouse();
        }

    }

    

    public new void CheckMouse()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag == "Tile")
                {
                    Tile t = hit.collider.GetComponent<Tile>();

                    if (t.selectable)
                    {
                        MoveToTile(t);
                    }
                }
                if (hit.collider.tag == "NPC")
                {
                    Debug.Log("Attacking Enemy");
                    if(InRange(hit.collider.gameObject) && !hasAttacked)
                    {
                        Attack(hit.collider.gameObject);
                        
                    }
                    else
                    {
                        Debug.Log("Target Out Of Range");
                    }
                }
            }
        }
    }
    
    private void Attack(GameObject target)
    {
        CalculateHeading(target.transform.position);
        transform.forward = heading;
        
        target.GetComponent<TacticsMove>().TakeDamage(attack);
        Refresh();
        BetterTurnManager.EndTurn();
        

    }
}
