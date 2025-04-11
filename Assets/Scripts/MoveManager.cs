using System.Collections.Generic;
using UnityEngine;

public class MoveManager : MonoBehaviour
{
    public static MoveManager instance;
    public void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    public int startY;
    public int StartX;
    public int endY;
    public int endX;
    public bool isStart;

    public void HandleClick(int startY, int startX)
    {
        if (isStart)
        {
            if (GameManager.instance.chess.board[startY, startX] = null) return;

            List<Move> moves = Moves.FindMovesWithStart(startY, startX);

            if (moves.Count > 0) { 

            }


        } else
        {

        }
    }
}