using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackPlayerPice : PlayerPice
{

    RollingDice blackHomeRollingDice;

    // Start is called before the first frame update
    void Start()
    {

        blackHomeRollingDice = GetComponentInParent<BlackHome>().rollingDice;

    }


    public void OnMouseDown()
    {

        if (GameManager.gm.rolingDice != null)
        {

            if (!isReady)
            {

                if (GameManager.gm.rolingDice == blackHomeRollingDice && GameManager.gm.numberofStepstoMove == 6)
                {
                    GameManager.gm.blackOutPlayers += 1;
                    MakePlayerReadyToMove(pathParent.BlackPathPoint);
                    GameManager.gm.numberofStepstoMove = 0;
                    return;
                }
            }

            if (GameManager.gm.rolingDice == blackHomeRollingDice && isReady)
            {

                MoveSteps(pathParent.BlackPathPoint);
            }
        }
    }
}
