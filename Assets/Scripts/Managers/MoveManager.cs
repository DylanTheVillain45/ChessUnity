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
    public int startX;
    public int endY;
    public int endX;
    public bool isStart = true;
    private List<Move> moveList;

    public void HandleClick(GameObject tileObj)
    {
        if (tileObj == null) return;

        Tile tile = tileObj.GetComponent<Tile>();

        int y = tile.y;
        int x = tile.x;

        if (isStart)
        {
            if (tileObj.transform.GetChild(0) == null) return;

            Piece piece = tileObj.transform.GetChild(0).gameObject.GetComponent<Piece>();

            List<Move> moveList = Moves.FindMovesWithStart(GameManager.instance.chess, piece);

            if (moveList.Count > 0) {
                SetStart(y, x);

                for (int i = 0; i < moveList.Count; i++)
                {
                    Move move = moveList[i];
                    GameObject landingTileObj = GameManager.instance.tileBoard[move.endY, move.endX];
                    SpriteRenderer landingTileRender = landingTileObj.GetComponent<SpriteRenderer>();
                    landingTileRender.color = GameManager.instance.hightlightColor;
                }
            }


        } else
        {

        }
    }

    private void SetStart(int y, int x)
    {
        this.startY = y;
        this.startX = x;
        this.isStart = false;
    }
}