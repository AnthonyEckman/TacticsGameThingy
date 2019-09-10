using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetterTurnManager : MonoBehaviour
{
    static List<GameObject> Team1 = new List<GameObject>();
    static List<GameObject> Team2 = new List<GameObject>();
    static Queue<GameObject> turnQueue = new Queue<GameObject>();

    static bool gameOver = false;
    static bool team1Turn = true;
    static bool team2Turn = false;

    private void Start()
    {
        SetupTeams();
    }

    private void Update()
    {
        
    }

    public void SetupTeams()
    {
        
        
        if(team1Turn)
        {
            
            foreach (GameObject soldier in Team1)
            {
                
                turnQueue.Enqueue(soldier);
            }
            
            StartTurn();
        }
        if(team2Turn)
        {
            foreach (GameObject soldier in Team2)
            {
                turnQueue.Enqueue(soldier);
            }
            StartTurn();
        }

    }

    public static void StartTurn()
    {
        if(gameOver)
        {
            Debug.Log("Game Over");
        }
        if(!Team1.Contains(turnQueue.Peek()) && !Team2.Contains(turnQueue.Peek()))
        {
            EndTurn();
        }
        if (turnQueue.Count > 0) 
        {
            turnQueue.Peek().GetComponent<TacticsMove>().BeginTurn();
        }
    }
    public static void EndTurn()
    {
        GameObject unit = turnQueue.Dequeue();
        unit.GetComponent<TacticsMove>().EndTurn();
        if(turnQueue.Count > 0)
        {
            StartTurn();
        }
        else if (turnQueue.Count == 0 && team1Turn)
        {
            
            team1Turn = false;
            team2Turn = true;
            foreach (GameObject soldier in Team2)
            {
                turnQueue.Enqueue(soldier);
            }
            StartTurn();
        }
        else if (turnQueue.Count == 0 && team2Turn)
        {
            
            team1Turn = true;
            team2Turn = false;
            foreach (GameObject soldier in Team1)
            {
                turnQueue.Enqueue(soldier);
            }
            StartTurn();
        }
        
    }

    public static void AddUnit(GameObject unit)
    {
        
        if(unit.tag == "Player")
        {
            Team1.Add(unit);

        }
        if (unit.tag == "NPC")
        {
            Team2.Add(unit);
        }
    }

    public static void RemoveUnit(GameObject unit)
    {
        if (unit.tag == "Player")
        {
            Team1.Remove(unit);
            Destroy(unit);
            
            if(Team1.Count == 0)
            {
                gameOver = true;
            }
        }
        if (unit.tag == "NPC")
        {
            Team2.Remove(unit);
            Destroy(unit);
            if (Team2.Count == 0)
            {
                gameOver = true;
            }
        }
    }

    public void ManualEndTurn()
    {
        GameObject unit = turnQueue.Dequeue();
        unit.GetComponent<TacticsMove>().EndTurn();
        if (turnQueue.Count > 0)
        {
            StartTurn();
        }
        else if (turnQueue.Count == 0 && team1Turn)
        {

            team1Turn = false;
            team2Turn = true;
            foreach (GameObject soldier in Team2)
            {
                turnQueue.Enqueue(soldier);
            }
            StartTurn();
        }
        else if (turnQueue.Count == 0 && team2Turn)
        {

            team1Turn = true;
            team2Turn = false;
            foreach (GameObject soldier in Team1)
            {
                turnQueue.Enqueue(soldier);
            }
            StartTurn();
        }

    }

}
