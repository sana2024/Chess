using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nakama;
using Nakama.TinyJson;
using System.Threading.Tasks;
using System;

public class DataSync : MonoBehaviour
{
    ISocket isocket;

    public static DataSync Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        isocket = PassData.socket;
        var mainThread = UnityMainThreadDispatcher.Instance();
        isocket.ReceivedMatchState += m => mainThread.Enqueue(async () => await OnReceivedMatchState(m));
    }

    private async Task  OnReceivedMatchState(IMatchState matchState)
    {
        var state = matchState.State.Length > 0 ? System.Text.Encoding.UTF8.GetString(matchState.State).FromJson<Dictionary<string, string>>() : null;

        switch (matchState.OpCode)
        {
            case OpCode.Turn:


                if (state["Turn"] == "White")
                {

                    ChessBoard.Instance.isWhiteTurn = true;

                }


                if (state["Turn"] == "Black")
                {

                    ChessBoard.Instance.isWhiteTurn = false;

                }


                break;


            case OpCode.Postion:

                int x = int.Parse(state["PosX"]);
                int y = int.Parse(state["PosY"]);


                GameObject pieceOb = GameObject.Find(state["CurrentlyDragging"]);
                ChessPiece piece = pieceOb.GetComponent<ChessPiece>();

                piece.SetPosition(ChessBoard.Instance.getTileCenter(x,y));

                piece.currentX = x;
                piece.currentY = y;
 

                break;

        }
        }

    public async void SendMatchState(long opCode, string state)
    {
 
            await isocket.SendMatchStateAsync(PassData.match.Id ,opCode, state);
  
    }

}
