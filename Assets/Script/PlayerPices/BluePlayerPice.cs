using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BluePlayerPice : PlayerPice
{

    RollingDice blueHomeRollingDice;

    // Start is called before the first frame update
    void Start()
    {

        blueHomeRollingDice = GetComponentInParent<BlueHome>().rollingDice;

    }


    public void OnMouseDown()
    {

        if (GameManager.gm.rolingDice != null)
        {

            if (!isReady)
            {

                if (GameManager.gm.rolingDice == blueHomeRollingDice && GameManager.gm.numberofStepstoMove == 6)
                {
                    GameManager.gm.blueOutPlayers += 1;
                    MakePlayerReadyToMove(pathParent.BluePathPoint);
                    GameManager.gm.numberofStepstoMove = 0;
                    return;
                }
            }

            if (GameManager.gm.rolingDice == blueHomeRollingDice && isReady)
            {

                MoveSteps(pathParent.BluePathPoint);
            }
        }
    }
}
