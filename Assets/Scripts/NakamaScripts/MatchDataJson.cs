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


    public static string SetPostion(string CurrentlyDragging,string  PosX , string PosY)
    {
        var values = new Dictionary<string, string>
        {
            { "CurrentlyDragging" , CurrentlyDragging },
            { "PosX" ,  PosX},
            { "PosY" ,  PosY}

        };

        return values.ToJson();
    }


}
