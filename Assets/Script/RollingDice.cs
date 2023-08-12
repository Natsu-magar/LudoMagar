using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingDice : MonoBehaviour
{

    [SerializeField] Sprite[] numberSprits;
        [SerializeField] SpriteRenderer numberedSpriteHolder;
    [SerializeField] int numberGot;
       [SerializeField] SpriteRenderer rollingDiceAnm;

    Coroutine generateRandomNuuonDice;

    public int outpieces;

    public PathObjectParent pathParent;
    PlayerPice[] currentplayerpices;
    PathPoint[] pathPointToMoveOn_;
    Coroutine MovePlayerPice;
    PlayerPice outPlayerPice;

    public Dice DiceSound;

    int maxNumber = 6;

    private void Awake()
    {
        pathParent = FindObjectOfType<PathObjectParent>();
    }


    // Update is called once per frame
    public void OnMouseDown()
    {

        generateRandomNuuonDice =  StartCoroutine(RolingDice());
    }

    public void mouseRoll()
    {

        generateRandomNuuonDice = StartCoroutine(RolingDice());
    }

    IEnumerator RolingDice()
    {
        GameManager.gm.transferDice = false;
        yield return new WaitForEndOfFrame();

        if (GameManager.gm.canDiceRoll)
        {

            DiceSound.PlaySound();

            GameManager.gm.canDiceRoll = false;

            numberedSpriteHolder.gameObject.SetActive(false);
            rollingDiceAnm.gameObject.SetActive(true);

            yield return new WaitForSeconds(1f);

            if (GameManager.gm.totalSix == 2)
            {
                maxNumber = 5;
            }

            numberGot = Random.Range(4, maxNumber);
            if (numberGot==6)
            {
                GameManager.gm.totalSix += 1;
            }
            else
            {
                GameManager.gm.totalSix += 0;
            }

            numberedSpriteHolder.sprite = numberSprits[numberGot];
            numberGot += 1;

            GameManager.gm.numberofStepstoMove = numberGot;
            GameManager.gm.rolingDice = this;

            numberedSpriteHolder.gameObject.SetActive(true);
            rollingDiceAnm.gameObject.SetActive(false);
            yield return new WaitForEndOfFrame();

            int nummberGot = GameManager.gm.numberofStepstoMove;

            if (PlayerCanNotMove())
            {
                yield return new WaitForSeconds(1f);
                if(nummberGot != 6)     { GameManager.gm.transferDice = true; }
                else { GameManager.gm.selfDice = true; }
            }
            else
            {
                if (GameManager.gm.rolingDice == GameManager.gm.manageRollingDice[0]) { outpieces = GameManager.gm.yellowOutPlayers; }
                else if (GameManager.gm.rolingDice == GameManager.gm.manageRollingDice[1]) { outpieces = GameManager.gm.blackOutPlayers; }
                else if (GameManager.gm.rolingDice == GameManager.gm.manageRollingDice[2]) { outpieces = GameManager.gm.orangeOutPlayers; }
                else if (GameManager.gm.rolingDice == GameManager.gm.manageRollingDice[3]) { outpieces = GameManager.gm.blueOutPlayers; }

                if ( outpieces==0 && numberGot != 6)
                {
                    yield return new WaitForSeconds(1f);
                    GameManager.gm.transferDice = true;
                                                      }

                else
                {
                    if (outpieces == 0 && numberGot ==6)
                    {
                        MakePlayerReadyToMove(0);  

                    }
                    else if(outpieces == 1 && numberGot!=6 && GameManager.gm.CanPlayerMove)
                    {

                        int playerPicePosition = CheckoutPlayer();

                        if (playerPicePosition>=0)
                        {
                            GameManager.gm.CanPlayerMove = false;
                            MovePlayerPice = StartCoroutine(MoveSteps_Enum(playerPicePosition));
                        }
                        else
                        {
                            yield return new WaitForSeconds(1f);
                            if (nummberGot != 6) { GameManager.gm.transferDice = true; }
                            else { GameManager.gm.selfDice = true; }
                        }
                       
                    }

                    else if (GameManager.gm.totalPlayerCanPlay ==1 && GameManager.gm.rolingDice==GameManager.gm.manageRollingDice[2])  {

                        if (numberGot ==6 && outpieces < 4)
                        {
                            MakePlayerReadyToMove(outPlayerToMove());
                        }
                        else
                        {
                            int playerPicePosition = CheckoutPlayer();
                            if(playerPicePosition >= 0)
                            {
                                GameManager.gm.CanPlayerMove = false;
                                MovePlayerPice = StartCoroutine(MoveSteps_Enum(playerPicePosition));
                            }
                            else
                            {
                                yield return new WaitForSeconds(1f);
                                if (nummberGot != 6) { GameManager.gm.transferDice = true; }
                                else { GameManager.gm.selfDice = true; }
                            }
                        }
                    }
                    else
                    {
                        if (CheckoutPlayer() < 0)
                        {
                            yield return new WaitForSeconds(1f);
                            if (nummberGot != 6) { GameManager.gm.transferDice = true; }
                            else { GameManager.gm.selfDice = true; }
                        }
                    }
                }

             
            }
            GameManager.gm.RollingDiceManager();

            if (generateRandomNuuonDice != null)
            {
                StopCoroutine(RolingDice());
            }
        }
    }

    int outPlayerToMove()
    {
        for (int i= 0;i<4; i++)
        {
            if (!GameManager.gm.orangePlayerPice[i].isReady)
            {
                return i;
            }
        }
        return 0;
    }

    int CheckoutPlayer()
    {
        if (GameManager.gm.rolingDice == GameManager.gm.manageRollingDice[0]) { currentplayerpices = GameManager.gm.yellowPlayerPice; pathPointToMoveOn_ = pathParent.YellowPathPoint; }
        else if (GameManager.gm.rolingDice == GameManager.gm.manageRollingDice[1]) { currentplayerpices = GameManager.gm.blackPlayerPice; pathPointToMoveOn_ = pathParent.BlackPathPoint; }
        else if (GameManager.gm.rolingDice == GameManager.gm.manageRollingDice[2]) { currentplayerpices = GameManager.gm.orangePlayerPice; pathPointToMoveOn_ = pathParent.OrangePathPoint; }
        else if (GameManager.gm.rolingDice == GameManager.gm.manageRollingDice[3]) { currentplayerpices = GameManager.gm.bluePlayerPice; pathPointToMoveOn_ = pathParent.BluePathPoint; }


        for (int i = 0;i<currentplayerpices.Length;i++)
        {
            if (currentplayerpices[i].isReady && isPathPointsAvailableToMove(GameManager.gm.numberofStepstoMove, currentplayerpices[i].numberOfStepsAlreadyMove, pathPointToMoveOn_))
            {
                return i;
            }
        }

        return -1;
    }

    

    public bool PlayerCanNotMove()
    {

        if(outpieces>0)
        {
            bool canNotMove = false;
            if (GameManager.gm.rolingDice == GameManager.gm.manageRollingDice[0]) { currentplayerpices = GameManager.gm.yellowPlayerPice; pathPointToMoveOn_ = pathParent.YellowPathPoint; }
            else if (GameManager.gm.rolingDice == GameManager.gm.manageRollingDice[1]) { currentplayerpices = GameManager.gm.blackPlayerPice; pathPointToMoveOn_ = pathParent.BlackPathPoint; }
            else if (GameManager.gm.rolingDice == GameManager.gm.manageRollingDice[2]) { currentplayerpices = GameManager.gm.orangePlayerPice; pathPointToMoveOn_ = pathParent.OrangePathPoint; }
            else if (GameManager.gm.rolingDice == GameManager.gm.manageRollingDice[3]) { currentplayerpices = GameManager.gm.bluePlayerPice; pathPointToMoveOn_ = pathParent.BluePathPoint; }

            for (int i=0; i<currentplayerpices.Length;i++)
            {
                if (currentplayerpices[i].isReady)
                {
                    if (isPathPointsAvailableToMove( GameManager.gm.numberofStepstoMove , currentplayerpices[i].numberOfStepsAlreadyMove, pathPointToMoveOn_))
                    {
                        return false;
                    }
                }
                else
                {
                    if (!canNotMove)
                    {
                        canNotMove = true;
                    }
                }
            }
            if (canNotMove)
            {
                return true;
            }
        }
        return false;
    }

    bool isPathPointsAvailableToMove(int numOfStepsToMove_, int numOfStepsAllready, PathPoint[] pathPointToMove_)
    {
        if (numOfStepsToMove_ == 0)
        {
            return false;
        }

        int leftNumberOfPath = pathPointToMove_.Length - numOfStepsAllready;
        if (leftNumberOfPath >= numOfStepsToMove_)
        {
            return true;
        }

        else
        {
            return false;
        }

    }

    public void MakePlayerReadyToMove(int outPlayer)
    {

        if (GameManager.gm.rolingDice == GameManager.gm.manageRollingDice[0]) { outPlayerPice = GameManager.gm.yellowPlayerPice[outPlayer]; pathPointToMoveOn_ = pathParent.YellowPathPoint; GameManager.gm.yellowOutPlayers +=1; }
        else if (GameManager.gm.rolingDice == GameManager.gm.manageRollingDice[1]) { outPlayerPice = GameManager.gm.blackPlayerPice[outPlayer]; pathPointToMoveOn_ = pathParent.BlackPathPoint; GameManager.gm.blackOutPlayers += 1; }
        else if (GameManager.gm.rolingDice == GameManager.gm.manageRollingDice[2]) { outPlayerPice = GameManager.gm.orangePlayerPice[outPlayer]; pathPointToMoveOn_ = pathParent.OrangePathPoint; GameManager.gm.orangeOutPlayers += 1; }
        else if (GameManager.gm.rolingDice == GameManager.gm.manageRollingDice[3]) { outPlayerPice = GameManager.gm.bluePlayerPice[outPlayer]; pathPointToMoveOn_ = pathParent.BluePathPoint; GameManager.gm.blueOutPlayers += 1; }



       outPlayerPice.isReady = true;
        outPlayerPice.transform.position = pathPointToMoveOn_[0].transform.position;
        outPlayerPice.numberOfStepsAlreadyMove = 1;

        outPlayerPice.previousPathPoint = pathPointToMoveOn_[0];
        outPlayerPice.CurrentPathPoint = pathPointToMoveOn_[0];

        outPlayerPice.CurrentPathPoint.AddPlayerPice(outPlayerPice);

        GameManager.gm.AddPathPoint(outPlayerPice.CurrentPathPoint);

        GameManager.gm.canDiceRoll = true;

        GameManager.gm.selfDice = true;

        GameManager.gm.transferDice = false;

        GameManager.gm.numberofStepstoMove = 0;


        GameManager.gm.SelfRoll();

    }

    IEnumerator MoveSteps_Enum(int movePlayer)
    {

        if (GameManager.gm.rolingDice == GameManager.gm.manageRollingDice[0]) { outPlayerPice = GameManager.gm.yellowPlayerPice[movePlayer]; pathPointToMoveOn_ = pathParent.YellowPathPoint; }
        else if (GameManager.gm.rolingDice == GameManager.gm.manageRollingDice[1]) { outPlayerPice = GameManager.gm.blackPlayerPice[movePlayer]; pathPointToMoveOn_ = pathParent.BlackPathPoint; }
        else if (GameManager.gm.rolingDice == GameManager.gm.manageRollingDice[2]) { outPlayerPice = GameManager.gm.orangePlayerPice[movePlayer]; pathPointToMoveOn_ = pathParent.OrangePathPoint;  }
        else if (GameManager.gm.rolingDice == GameManager.gm.manageRollingDice[3]) { outPlayerPice = GameManager.gm.bluePlayerPice[movePlayer]; pathPointToMoveOn_ = pathParent.BluePathPoint; }


        GameManager.gm.transferDice = false;
        yield return new WaitForSeconds(0.5f);
        int numberOfStepsToMove = GameManager.gm.numberofStepstoMove;

        outPlayerPice.CurrentPathPoint.RescaleAndRepositioning();

        for (int i = outPlayerPice.numberOfStepsAlreadyMove; i < (outPlayerPice.numberOfStepsAlreadyMove + numberOfStepsToMove); i++)
        {

           
            if (isPathPointsAvailableToMove(numberOfStepsToMove, outPlayerPice.numberOfStepsAlreadyMove, pathPointToMoveOn_))
            {
                outPlayerPice.transform.position = pathPointToMoveOn_[i].transform.position;
                GameManager.gm.ads.Play();
                yield return new WaitForSeconds(0.4f);
            }
        }

        if (isPathPointsAvailableToMove(numberOfStepsToMove, outPlayerPice.numberOfStepsAlreadyMove, pathPointToMoveOn_))
        {



            outPlayerPice.numberOfStepsAlreadyMove += numberOfStepsToMove;


            GameManager.gm.RemovePathPoint(outPlayerPice.previousPathPoint);
            outPlayerPice.previousPathPoint.RemovePlayerPce(outPlayerPice);
            outPlayerPice.CurrentPathPoint = pathPointToMoveOn_[outPlayerPice.numberOfStepsAlreadyMove - 1];

            if (outPlayerPice.CurrentPathPoint.AddPlayerPice(outPlayerPice))
            {
                if (outPlayerPice.numberOfStepsAlreadyMove == 57)
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


            GameManager.gm.AddPathPoint(outPlayerPice.CurrentPathPoint);
            outPlayerPice.previousPathPoint = outPlayerPice.CurrentPathPoint;


            GameManager.gm.numberofStepstoMove = 0;

        }
        GameManager.gm.CanPlayerMove = true;

        GameManager.gm.RollingDiceManager();

        if (MovePlayerPice != null)
        {
            StopCoroutine("MoveSteps_Enum");
        }

    }
}
