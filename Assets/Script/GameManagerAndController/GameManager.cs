using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager gm;
    public RollingDice rolingDice;
    public int numberofStepstoMove;
    public bool CanPlayerMove = true;

    List<PathPoint> playerOnPathPointList = new List<PathPoint>();

    public bool canDiceRoll = true;
    public bool transferDice = false;
    public bool selfDice = false;

    public int yellowOutPlayers;
    public int blackOutPlayers;
    public int orangeOutPlayers;
    public int blueOutPlayers;

    public int yellowCompletePlayers;
    public int blackCompletePlayers;
    public int orangeCompletePlayers;
    public int blueCompletePlayers;

    public RollingDice[] manageRollingDice;

    public PlayerPice[] yellowPlayerPice;
    public PlayerPice[] blackPlayerPice;
    public PlayerPice[] orangePlayerPice;
    public PlayerPice[] bluePlayerPice;

    public int totalPlayerCanPlay;

    public int totalSix=0;

    public AudioSource ads;

    private void Awake()
    {
        gm = this;
        ads = GetComponent<AudioSource>();
    }

    public void AddPathPoint(PathPoint pathPoint)
    {
        playerOnPathPointList.Add(pathPoint);
    }
    public void RemovePathPoint(PathPoint pathpoint)
    {
        if (playerOnPathPointList.Contains(pathpoint)) {
            playerOnPathPointList.Remove(pathpoint);
        }
        else
        {
            Debug.Log("Pathpoint not Found to be removed");
        }

    }

    public void RollingDiceManager()
    {
     
        if (GameManager.gm.transferDice)
        {
           if( GameManager.gm.numberofStepstoMove != 6)
            {
                ShiftDice();
            }
           

            GameManager.gm.canDiceRoll = true;

         
        }
        else
        {
            if (GameManager.gm.selfDice)
            {
                GameManager.gm.selfDice = false;
                GameManager.gm.canDiceRoll = true;
                GameManager.gm.SelfRoll();
            }
        }
    }


    public void SelfRoll()
    {
        if(GameManager.gm.totalPlayerCanPlay==1 && GameManager.gm.rolingDice == GameManager.gm.manageRollingDice[2])
        {
            Invoke("roled",0.6f);
        }
    }

    void roled()
    {
        GameManager.gm.manageRollingDice[2].mouseRoll();
    }


    void ShiftDice()
    {

        int nextdice;

        if (GameManager.gm.totalPlayerCanPlay == 1)
        {
            if (GameManager.gm.rolingDice = GameManager.gm.manageRollingDice[0])
            {
                GameManager.gm.manageRollingDice[0].gameObject.SetActive(false);
                GameManager.gm.manageRollingDice[2].gameObject.SetActive(true);

                passout(0);

                GameManager.gm.manageRollingDice[2].mouseRoll();

            }
            else
            {
                GameManager.gm.manageRollingDice[0].gameObject.SetActive(true);
                GameManager.gm.manageRollingDice[2].gameObject.SetActive(false);
                passout(2);
            }
        }




        else if (GameManager.gm.totalPlayerCanPlay == 2)
        {
         if(GameManager.gm.rolingDice = GameManager.gm.manageRollingDice[0])
            {
                GameManager.gm.manageRollingDice[0].gameObject.SetActive(false);
                GameManager.gm.manageRollingDice[2].gameObject.SetActive(true);
                passout(0);
            }
            else
            {
                GameManager.gm.manageRollingDice[0].gameObject.SetActive(true);
                GameManager.gm.manageRollingDice[2].gameObject.SetActive(false);
                passout(2);
            }
        }
        else if (GameManager.gm.totalPlayerCanPlay == 3)
        {
            for (int i = 0; i < 3; i++)
            {
                if (i == 2)
                {
                    nextdice = 0;
                }
                else
                {
                    nextdice = i + 1;
                }

                i = passout(i);

                if (GameManager.gm.rolingDice == GameManager.gm.manageRollingDice[i])
                {

                    GameManager.gm.manageRollingDice[i].gameObject.SetActive(false);
                    GameManager.gm.manageRollingDice[nextdice].gameObject.SetActive(true);
                }
            }
        }
        else 
        {
            for (int i = 0; i < 4; i++)
            {
                if (i == 3)
                {
                    nextdice = 0;
                }
                else
                {
                    nextdice = i + 1;
                }

                i = passout(i);

                if (GameManager.gm.rolingDice == GameManager.gm.manageRollingDice[i])
                {

                    GameManager.gm.manageRollingDice[i].gameObject.SetActive(false);
                    GameManager.gm.manageRollingDice[nextdice].gameObject.SetActive(true);
                }
            }
        }
    }

    int passout(int i)
    {
        if (i == 0) { if (GameManager.gm.yellowCompletePlayers == 4) { return (i + 1); } }
      else  if (i == 1) { if (GameManager.gm.yellowCompletePlayers == 4) { return (i + 1); } }
        else if (i == 2) { if (GameManager.gm.yellowCompletePlayers == 4) { return (i + 1); } }
        else if (i == 3) { if (GameManager.gm.yellowCompletePlayers == 4) { return (i + 1); } }

        return i;
    }

}
