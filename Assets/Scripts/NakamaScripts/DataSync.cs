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

                //change the recevied postion to int to pass it to the current postion
                int x = int.Parse(state["PosX"]);
                int y = int.Parse(state["PosY"]);


                //get back a gameobject in the scene that has the name of the received piece
                GameObject pieceOb = GameObject.Find(state["CurrentlyDragging"]);
                ChessPiece piece = pieceOb.GetComponent<ChessPiece>();

                //clear the old tile that had the peice in it
                ChessBoard.Instance.chessPieces[piece.currentX, piece.currentY] = null;
 
                //move the received piece to the position
                piece.SetPosition(ChessBoard.Instance.getTileCenter(x,y));

                piece.currentX = x;
                piece.currentY = y;

                //update the chesspiece list to include the new piece
                ChessBoard.Instance.chessPieces[piece.currentX, piece.currentY] = piece;
 
                break;


            case OpCode.Hit:

                int Current_X = int.Parse(state["x"]);
                int Current_Y = int.Parse(state["y"]);


                ChessPiece HittedPiece = ChessBoard.Instance.chessPieces[Current_X , Current_Y];

 
                if (HittedPiece.team == 0)
                {
                    if (HittedPiece.type == ChessPieceType.King)
                    {
                       ChessBoard.Instance.CheckMate(0);
                    }
                    ChessBoard.Instance.didLastMoveCapture = true;
                    //its redundant but because we reset it in different spots lets just do it

                    ChessBoard.Instance.capturedPiece = true;

                    ChessBoard.Instance.deadWhites.Add(HittedPiece);
                    HittedPiece.SetScale(Vector3.one * ChessBoard.Instance.deathSize);
                    Vector3 NewPostion = new Vector3(8 * ChessBoard.Instance.tileSize, -ChessBoard.Instance.tileSize / 4, ChessBoard.Instance.yOffset) + new Vector3(ChessBoard.Instance.tileSize / 2, 0, 0) + (Vector3.up * ChessBoard.Instance.deathSpacing) * ChessBoard.Instance.deadWhites.Count;
                    HittedPiece.SetPosition(NewPostion);
 

                }
                else
                {
                    if (HittedPiece.type == ChessPieceType.King)
                    {
                        ChessBoard.Instance.CheckMate(1);
                    }
                    ChessBoard.Instance.didLastMoveCapture = true;
                    ChessBoard.Instance.capturedPiece = true;

                    ChessBoard.Instance.deadBlacks.Add(HittedPiece);
                    HittedPiece.SetScale(Vector3.one * ChessBoard.Instance.deathSize);
                    HittedPiece.SetPosition(new Vector3(-ChessBoard.Instance.tileSize, -ChessBoard.Instance.tileSize / 4, ChessBoard.Instance.yOffset) + new Vector3(ChessBoard.Instance.tileSize / 2, 0, 0) + (Vector3.up * ChessBoard.Instance.deathSpacing) * ChessBoard.Instance.deadBlacks.Count);
 

                }

 

                break;


            case OpCode.Promotion:

                GameObject PromotedPieceOb = GameObject.Find(state["PromotedPiece"]);
                ChessPiece Promotediece = PromotedPieceOb.GetComponent<ChessPiece>();







                break;


        }
        }

    public async void SendMatchState(long opCode, string state)
    {
 
            await isocket.SendMatchStateAsync(PassData.match.Id ,opCode, state);
  
    }

}
