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
    private List<Move> moveList;
    public void HandleClick(int y, int x)
    {
        GameObject tileObj = GameManager.instance.tileBoard[y, x];

        if (tileObj) return;

        Tile tile = tileObj.GetComponent<Tile>();

        if (isStart)
        {
            if (tileObj.transform.GetChild(0) != null) return;

            Piece piece = tileObj.transform.GetChild(0).gameObject.GetComponent<Piece>();

            SetStart(y, x);
            isStart = false;

            List<Move> moveList = Moves.FindMovesWithStart(GameManager.instance.chess, piece);

            if (moveList.Count > 0) {
                for (int i = 0; i < moveList.Count; i++)
                {
                    Move move = moveList[i];

                    GameManager.instance.tileBoard[move.startY, move.startX].GetComponent<SpriteRenderer>().color = GameManager.instance.hightlightColor;
                }
            }


        } else
        {

        }
    }

    private void SetStart(int y, int x)
    {
        startY = y;
        StartX = x;
    }
}