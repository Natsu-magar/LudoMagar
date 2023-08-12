using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowPlayerPice : PlayerPice
{

    RollingDice yellowHomeRollingDice;

    // Start is called before the first frame update
    void Start()
    {

        yellowHomeRollingDice = GetComponentInParent<YellowHome>().rollingDice;

    }


    public void OnMouseDown()
    {

        if (GameManager.gm.rolingDice != null)
        {

            if (!isReady)
            {

                if (GameManager.gm.rolingDice == yellowHomeRollingDice && GameManager.gm.numberofStepstoMove == 6)
                {

                    GameManager.gm.yellowOutPlayers += 1;
                    MakePlayerReadyToMove(pathParent.YellowPathPoint);
                    GameManager.gm.numberofStepstoMove = 0;
                    return;
                }
            }

            if (GameManager.gm.rolingDice == yellowHomeRollingDice && isReady && GameManager.gm.CanPlayerMove)
            {
                GameManager.gm.CanPlayerMove = false;
                MoveSteps(pathParent.YellowPathPoint);
            }
        }
    }

}
