using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nakama.TinyJson;

public class MatchDataJson
{

    public static string SetTurn(string Turn)
    {
        var values = new Dictionary<string, string>
        {
            { "Turn" , Turn.ToString()}

        };

        return values.ToJson();
    }


    public static string SetPostion(string piece, string  PosX , string PosY)
    {
        var values = new Dictionary<string, string>
        {
            { "CurrentlyDragging" , piece},
            { "PosX" ,  PosX},
            { "PosY" ,  PosY}

        };

        return values.ToJson();
    }


    public static string SetHit(int x , int y)
    {
        var values = new Dictionary<string, string>
        {
            { "x",  x.ToString() },
            { "y",  y.ToString() },
 
 
 
        };

        return values.ToJson();
    }



    public static string SetPromotion(string x , string y , string team , string pieceType)
    {
        var values = new Dictionary<string, string>
        {
            { "LastMove_x",  x},
            { "LastMove_Y",  y},
            { "Team",  team},
            { "Type",  pieceType},



        };

        return values.ToJson();
    }


}
