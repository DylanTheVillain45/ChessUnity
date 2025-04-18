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
    public List<Move> moveList = new List<Move>();
    public List<GameObject> higlightedTiles = new List<GameObject>();

    public void HandleClick(GameObject tileObj)
    {
        if (tileObj == null) return;

        Tile tile = tileObj.GetComponent<Tile>();
        Piece piece = tile.transform.childCount > 0 ? tile.transform.GetChild(0).GetComponent<Piece>() : null;

        int y = tile.y;
        int x = tile.x;

        if (piece != null && piece.color == GameManager.instance.chess.gameColor)
        {
            UnHighlightTiles();

            moveList = Moves.FindMovesWithStart(GameManager.instance.chess, piece);

            if (moveList.Count > 0) {
                SetStart(y, x);

                for (int i = 0; i < moveList.Count; i++)
                {
                    Move move = moveList[i];
                    GameObject landingTileObj = GameManager.instance.tileBoard[move.endY, move.endX];
                    SpriteRenderer landingTileRender = landingTileObj.GetComponent<SpriteRenderer>();
                    higlightedTiles.Add(landingTileObj);
                    landingTileRender.color = GameManager.instance.hightlightColor;
                }
            }
        } else if (isStart == false && (piece == null || (piece.color != GameManager.instance.chess.gameColor) && piece.type != Type.King)) {
            Move move = Moves.FindMoveWithEndAndList(moveList, y, x);

            if (move == null) return;

            SetReset();
            GameManager.instance.MakeMove(move);
        }
    }

    private void SetStart(int y, int x)
    {
        this.startY = y;
        this.startX = x;
        this.isStart = false;
    }

    private void SetReset()
    {
        UnHighlightTiles();
        this.startY = 0;
        this.startX = 0;
        this.isStart = true;
        higlightedTiles = new List<GameObject>();
    }

    private void UnHighlightTiles()
    {
        foreach (GameObject tileObj in higlightedTiles)
        {
            SpriteRenderer landingTileRender = tileObj.GetComponent<SpriteRenderer>();
            Tile tile = tileObj.GetComponent<Tile>();
            landingTileRender.color = (tile.y + tile.x) % 2 == 0 ? GameManager.instance.blackTile : GameManager.instance.whiteTIle;

        }
    }
}