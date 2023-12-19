using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSystem : simdata {

	// Use this for initialization
	void Start () {
		
	}
	
    public static void LevelUp()
    {
        if(playerExp >= 100)
        {
            playerLevel = 2;
        }
    }

	// Update is called once per frame
	public static void PlayerInfo ()
    {
        playerName = "Donna";
        playerLevel = 1;

        switch(playerLevel)
        {
            case (1):
                playerAP = 3;
                playerMaxHP = 100;
                break;
            case (2):
                playerAP = 6;
                playerMaxHP = 105;
                break;
            case (3):
                playerAP = 7;
                playerMaxHP = 110;
                break;
            case (4):
                playerAP = 8;
                playerMaxHP = 115;
                break;
            case (5):
                playerAP = 9;
                playerMaxHP = 120;
                break;
            case (6):
                playerAP = 10;
                playerMaxHP = 125;
                break;
            case (7):
                playerAP = 11;
                playerMaxHP = 130;
                break;
            case (8):
                playerAP = 12;
                playerMaxHP = 135;
                break;
            case (9):
                playerAP = 13;
                playerMaxHP = 140;
                break;
            case (10):
                playerAP = 14;
                playerMaxHP = 145;
                break;
            case (11):
                playerAP = 15;
                playerMaxHP = 150;
                break;
            case (12):
                playerAP = 16;
                playerMaxHP = 155;
                break;
        }

    }
}
