using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    public Vector3 startPos;

    public Vector3 endPos;

    public TacticsMove parent;

    public TacticsMove target;

    private int Height = 3;

    private float Incrementor = 0;

    private bool StartThrow = false;


    private void Awake()
    {
        startPos = transform.position;
    }
    private void Update()
    {

        if(target != null)
        {
            StartThrow = true;
        }
        if (StartThrow)
        {

            //if start throw is set active from the move method, it will transform the chesspiece to the target locations
            Incrementor += 0.04f;
            Vector3 currentPos = Vector3.Lerp(startPos, endPos, Incrementor);
            currentPos.y += Height * Mathf.Sin(Mathf.Clamp01(Incrementor) * Mathf.PI);
            transform.position = currentPos;
        }
        //once the chess piece reaches its final destination it will turn start throw back to off and end the movment and reset it.
        if (transform.position == endPos)
        {
            target.TakeDamage(2);
            BetterTurnManager.EndTurn();
            Destroy(gameObject);


        }
    }

}
