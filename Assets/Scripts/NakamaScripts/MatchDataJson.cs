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


    public static string SetHighLight(string x0, string y0 , string x1 , string y1)
    {
        var values = new Dictionary<string, string>
        {
            { "x0",  x0},
            { "y0",  y0},
            { "x1",  x1},
            { "y1",  y1}
        };

        return values.ToJson();
    }


    public static string SetCheck(int x , int y)
    {
        var values = new Dictionary<string, string>
        {
            { "Tilex",  x.ToString()},
            { "Tiley",  y.ToString()}

        };

        return values.ToJson();
    }


    public static string SetCheckmate(string winner)
    {
        var values = new Dictionary<string, string>
        {
            { "Winner",  winner}
 

        };

        return values.ToJson();
    }

    public static string SetNotation(string Notation)
    {
        var values = new Dictionary<string, string>
        {
            { "Notation",  Notation},

        };

        return values.ToJson();
    }
}
