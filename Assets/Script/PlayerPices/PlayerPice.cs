using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPice : MonoBehaviour
{

    public bool moveNow;
    public bool isReady;
    public int numberOfStepsAlreadyMove;
    public int numberOfStepsToMove;

    public PathObjectParent pathParent;
    Coroutine MovePlayerPice;

    public PathPoint previousPathPoint;
    public PathPoint CurrentPathPoint;

    private void Awake()
    {
        pathParent = FindObjectOfType<PathObjectParent>();
    }

    public void MoveSteps(PathPoint[] pathPointsToMoveon_)
    {
        MovePlayerPice = StartCoroutine(MoveSteps_Enum(pathPointsToMoveon_));
    }

    public void MakePlayerReadyToMove(PathPoint[] pathPointsToMoveon_)
    {
        isReady = true;
        transform.position = pathPointsToMoveon_[0].transform.position;
        numberOfStepsAlreadyMove = 1;

        previousPathPoint = pathPointsToMoveon_[0];
        CurrentPathPoint = pathPointsToMoveon_[0];

        CurrentPathPoint.AddPlayerPice(this);

        GameManager.gm.AddPathPoint(CurrentPathPoint);

        GameManager.gm.canDiceRoll = true;

        GameManager.gm.selfDice = true;

        GameManager.gm.transferDice = false;

    }

    IEnumerator MoveSteps_Enum(PathPoint[] pathPointsToMoveon_)
    {
        GameManager.gm.transferDice = false;
        yield return new WaitForSeconds(0.5f);
        numberOfStepsToMove =  GameManager.gm.numberofStepstoMove;

        CurrentPathPoint.RescaleAndRepositioning();

        for (int i = numberOfStepsAlreadyMove; i < (numberOfStepsAlreadyMove + numberOfStepsToMove); i++)
        {

       
            if (isPathPointsAvailableToMove(numberOfStepsToMove, numberOfStepsAlreadyMove, pathPointsToMoveon_))
            {
                transform.position = pathPointsToMoveon_[i].transform.position;
                GameManager.gm.ads.Play();
                yield return new WaitForSeconds(0.4f);
            }
        }

        if (isPathPointsAvailableToMove(numberOfStepsToMove, numberOfStepsAlreadyMove, pathPointsToMoveon_))
        {


         
            numberOfStepsAlreadyMove += numberOfStepsToMove;
          

            GameManager.gm.RemovePathPoint(previousPathPoint);
            previousPathPoint.RemovePlayerPce(this);
            CurrentPathPoint = pathPointsToMoveon_[numberOfStepsAlreadyMove - 1];

            if (CurrentPathPoint.AddPlayerPice(this))
            {
                if (numberOfStepsAlreadyMove == 57)
                {
                    GameManager.gm.selfDice = true;
                }
                else
                {
                    if (GameManager.gm.numberofStepstoMove != 6)
                    {


                        GameManager.gm.transferDice = true;
                    }
                    else
                    {
                        GameManager.gm.selfDice = true;


                    }
                }
            }
            else
            {
                GameManager.gm.selfDice = true;
            }


            GameManager.gm.AddPathPoint(CurrentPathPoint);
            previousPathPoint = CurrentPathPoint;

                      
            GameManager.gm.numberofStepstoMove = 0;

        }
        GameManager.gm.CanPlayerMove = true;

        GameManager.gm.RollingDiceManager();

        if (MovePlayerPice != null)
        {
            StopCoroutine("MoveSteps_Enum");
        }

    }

    bool isPathPointsAvailableToMove(int numOfStepsToMove_,int numOfStepsAllready,PathPoint[] pathPointToMove_)
    {
        if(numOfStepsToMove_ == 0)
        {
            return false;
        }

        int leftNumberOfPath = pathPointToMove_.Length - numOfStepsAllready;
        if(leftNumberOfPath >= numOfStepsToMove_)
        {
            return true;
        }

        else
        {
            return false;
        }

    }

}
