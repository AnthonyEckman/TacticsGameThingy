using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : TacticsMove
{
    public bool hasAttacked = false;

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

        if (!moving)
        {
            FindSelectableTiles();
            CheckMouse();
        }
        else
        {
            Move();
        }

    }
    

    public void Refresh()
    {
        hasMoved = false;
        hasAttacked = false;
    }

    public void CheckMouse()
    {
        if(Input.GetMouseButtonUp(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            if(Physics.Raycast(ray,out hit))
            {
                if(hit.collider.tag == "Tile")
                {
                    Tile t = hit.collider.GetComponent<Tile>();
                    
                    if (t.selectable)
                    {
                        MoveToTile(t);
                    }
                }
                if(hit.collider.tag == "NPC")
                {
                    Debug.Log("Attacking Enemy");
                    InRange(hit.collider.gameObject);
                }
            }
        }
    }

    
}
