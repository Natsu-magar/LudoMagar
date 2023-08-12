using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathPoint : MonoBehaviour
{

    PathPoint[] pathpointToMoveon_;
    public PathObjectParent pathObjectParent;
    public List<PlayerPice> PlayerPiceList = new List<PlayerPice>();


    public void Start()
    {
        pathObjectParent = GetComponentInParent<PathObjectParent>();
    }

    public bool AddPlayerPice(PlayerPice playerPice_)
    {

        if (this.name == "CenterPath")
        {
            Completed(playerPice_);
        }


        if (this.name != "Pathpoint" && this.name != "Pathpoint (8)"&&
            this.name != "Pathpoint (13)"&&
                this.name != "Pathpoint (21)"&&
            this.name != "Pathpoint (26)"&&
            this.name != "Pathpoint (34)"&&
            this.name != "Pathpoint (39)"&&
            this.name != "Pathpoint (47)" &&
            this.name != "CenterPath"

            ) {

            if (PlayerPiceList.Count == 1)
            {
                string previousPlayerPiceName = PlayerPiceList[0].name;
                string currentPlayerPiceName = playerPice_.name;
                currentPlayerPiceName = currentPlayerPiceName.Substring(0, currentPlayerPiceName.Length - 4);

                if (!previousPlayerPiceName.Contains(currentPlayerPiceName))
                {
                    PlayerPiceList[0].isReady = false;

                    StartCoroutine(revertOnStart(PlayerPiceList[0]));


                    PlayerPiceList[0].numberOfStepsAlreadyMove = 0;

                    RemovePlayerPce(PlayerPiceList[0]);

                    PlayerPiceList.Add(playerPice_);

                    return false;

                }

            }
        }
        addPlayer(playerPice_);
        return true;
    }


    IEnumerator revertOnStart(PlayerPice playerPice_)
    {

        if (playerPice_.name.Contains("blue"))
        {
            GameManager.gm.blueOutPlayers -= 1;
            pathpointToMoveon_ = pathObjectParent.BluePathPoint;
        }

        else if (playerPice_.name.Contains("yellow"))
        {
            GameManager.gm.yellowOutPlayers -= 1;
            pathpointToMoveon_ = pathObjectParent.YellowPathPoint;

        }
        else if (playerPice_.name.Contains("black"))
        {
            GameManager.gm.blackOutPlayers -= 1;
            pathpointToMoveon_ = pathObjectParent.BlackPathPoint;

        }

        else if (playerPice_.name.Contains("orange"))
        {
            GameManager.gm.orangeOutPlayers -= 1;
            pathpointToMoveon_ = pathObjectParent.OrangePathPoint;

        }

        for(int i = playerPice_.numberOfStepsAlreadyMove; i >= 0; i--)
        {
            playerPice_.transform.position = pathpointToMoveon_[i].transform.position;
            yield return new WaitForSeconds(0.15f);

        }

        playerPice_.transform.position = pathObjectParent.BasePoints[BasePointPosition(playerPice_.name)].transform.position;
    }

    int BasePointPosition(string name)
    {
       

        for(int i=0;i<pathObjectParent.BasePoints.Length; i++)
        {
            if(pathObjectParent.BasePoints[i].name == name)
            {
                return i;
            }
        }

        return -1;

    }


    void addPlayer(PlayerPice playerPice_)
    {

       
        PlayerPiceList.Add(playerPice_);
        RescaleAndRepositioning();
    }

    public void RemovePlayerPce(PlayerPice playerPice_)
    {
        if (PlayerPiceList.Contains(playerPice_))
        {
            PlayerPiceList.Remove(playerPice_);
            RescaleAndRepositioning();
        }
    }


    private void Completed(PlayerPice playerPice_)
    {

        if (playerPice_.name.Contains("blue"))
        {
            GameManager.gm.blueCompletePlayers += 1;
            GameManager.gm.blueOutPlayers -= 1;
           if(GameManager.gm.blueCompletePlayers == 4)
            {
                ShowCelebration();
            }
        }

        else if (playerPice_.name.Contains("yellow"))
        {
            GameManager.gm.yellowCompletePlayers += 1;
            GameManager.gm.yellowOutPlayers -= 1;
            if (GameManager.gm.yellowCompletePlayers == 4)
            {
                ShowCelebration();
            }

        }
        else if (playerPice_.name.Contains("black"))
        {
            GameManager.gm.blackCompletePlayers += 1;
            GameManager.gm.blackOutPlayers -= 1;
            if (GameManager.gm.blackCompletePlayers == 4)
            {
                ShowCelebration();
            }

        }

        else if (playerPice_.name.Contains("orange"))
        {
            GameManager.gm.orangeCompletePlayers += 1;
            GameManager.gm.orangeOutPlayers -= 1;
            if (GameManager.gm.orangeCompletePlayers == 4)
            {
                ShowCelebration();
            }

        }

    }

    void ShowCelebration()
    {

    }

    public void RescaleAndRepositioning()
    {
        int placeCount = PlayerPiceList.Count;
        bool isOdd = (placeCount % 2) ==0?false : true;

        int extent = placeCount / 2;
        int counter = 0;
        int spriteLayer = 0;

        if (isOdd)
        {
            for(int i= -extent;i<=extent; i++)
            {
                PlayerPiceList[counter].transform.localScale = new Vector3(pathObjectParent.scale[placeCount - 1], pathObjectParent.scale[placeCount - 1], 0.5f);
                PlayerPiceList[counter].transform.position = new Vector3(transform.position.x +(i*pathObjectParent.positionDiff[placeCount-1]),transform.position.y,0f     );
                counter++;
            
}
        }
        else
        {
            for(int i = -extent; i < extent; i++)
            {
                PlayerPiceList[counter].transform.localScale = new Vector3(pathObjectParent.scale[placeCount - 1], pathObjectParent.scale[placeCount - 1], 0.5f);
                PlayerPiceList[counter].transform.position = new Vector3(transform.position.x + (i * pathObjectParent.positionDiff[placeCount - 1]), transform.position.y, 0f);
                counter++;

            }
        }


        for(int i =0; i < PlayerPiceList.Count; i++)
        {
            PlayerPiceList[i].GetComponentInChildren<SpriteRenderer>().sortingOrder= spriteLayer;
            spriteLayer++;
        }
       
    }
}
