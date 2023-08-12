using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrangePlayerPice : PlayerPice
{

    RollingDice orangeHomeRollingDice;

    // Start is called before the first frame update
    void Start()
    {

        orangeHomeRollingDice = GetComponentInParent<OrangeHome>().rollingDice;

    }


    public void OnMouseDown()
    {

        if (GameManager.gm.rolingDice != null)
        {

            if (!isReady)
            {

                if (GameManager.gm.rolingDice == orangeHomeRollingDice && GameManager.gm.numberofStepstoMove == 6)
                {
                    GameManager.gm.orangeOutPlayers += 1;
                    MakePlayerReadyToMove(pathParent.OrangePathPoint);
                    GameManager.gm.numberofStepstoMove = 0;
                    return;
                }
            }

            if (GameManager.gm.rolingDice == orangeHomeRollingDice && isReady)
            {

                MoveSteps(pathParent.OrangePathPoint);
            }
        }
    }
}