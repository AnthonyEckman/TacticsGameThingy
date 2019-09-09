using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMove : TacticsMove
{

    GameObject target;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position, transform.forward);

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
        else
        {
            
            Move();
        }
    }

    private void CalculatePath()
    {
        Tile targetTile = GetTargetTile(target);
        FindPath(targetTile);
        
    }

    void FindNearestTarget()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Player");

        GameObject nearest = null;
        float distance = Mathf.Infinity;

        foreach (GameObject unit in targets)
        {
            //should change to vector3.magnitude
            float d = Vector3.Distance(transform.position, unit.transform.position);

            if ( d < distance)
            {
                distance = d;
                nearest = unit;
            }

        }

        target = nearest;
    }
}
