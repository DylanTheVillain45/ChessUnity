using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

public enum Type {Pawn, Knight, Bishop, Rook, Queen, King};
public enum PieceColor {White, Black};

#nullable enable
public class BoardManager : MonoBehaviour
{
    public static BoardManager instance;

    public Color blackTile;
    public Color whiteTIle;

    public GameObject TilePref;
    public GameObject PiecePref;
    public Transform boardParent;

    public Piece?[,] board;
    public GameObject[,] tileBoard;

    public Sprite[] pieceSprites = new Sprite[12];
    public Dictionary<(Type, PieceColor), int> pieceMap = new Dictionary<(Type, PieceColor), int>()
{
    { (Type.Pawn, PieceColor.White), 0 },
    { (Type.Knight, PieceColor.White), 1 },
    { (Type.Bishop, PieceColor.White), 2 },
    { (Type.Rook, PieceColor.White), 3 },
    { (Type.Queen, PieceColor.White), 4 },
    { (Type.King, PieceColor.White), 5 },

    { (Type.Pawn, PieceColor.Black), 6 },
    { (Type.Knight, PieceColor.Black), 7 },
    { (Type.Bishop, PieceColor.Black), 8 },
    { (Type.Rook, PieceColor.Black), 9 },
    { (Type.Queen, PieceColor.Black), 10 },
    { (Type.King, PieceColor.Black), 11 }
};
    
    public void Awake() {
        if (instance != null) {
            Destroy(gameObject);
        } else {
            instance = this;
        }
    }

    public void Start() {
        SetUpBoard();
    }

    public void SetUpBoard() {
        board = new Piece?[8,8];

        for (int i = 0; i < 8; i++) {
            for (int j = 0; j < 8; j++) {
                GameObject tile = Instantiate(TilePref, boardParent);

                tile.transform.position = new Vector2((j - 3.5f) * tile.transform.localScale.x, (i - 3.5f) * tile.transform.localScale.y);

                SpriteRenderer tileRenderer = tile.GetComponent<SpriteRenderer>();

                if ((i + j) % 2 == 0) {
                    tileRenderer.color = blackTile;
                } else {
                    tileRenderer.color = whiteTIle;
                }

                tile.name = $"Tile-{i}{j}";

                if (i == 0 || i == 1 || i == 6 || i == 7) {
                    GameObject pieceObj = Instantiate(PiecePref, tile.transform);
                    Piece piece = pieceObj.GetComponent<Piece>();

                    PieceColor pieceColor;
                    Type type = Type.Pawn;
                    if (i == 1 || i == 6) {
                        pieceColor = i == 1 ? PieceColor.White : PieceColor.Black;
                    } else {
                        pieceColor = i == 0 ? PieceColor.White : PieceColor.Black;
                        if (j == 0 || j == 7) type = Type.Rook;
                        if (j == 1 || j == 6) type = Type.Knight;
                        if (j == 2 || j == 5) type = Type.Bishop;
                        if (j == 3) type = Type.Queen;
                        if (j == 4) type = Type.King;

                    }
                    piece.SetPiece(Type.Pawn, pieceColor, i, j, pieceSprites[pieceMap[(type, pieceColor)]]);
                    board[i, j] = piece;
                }

            }
        }
    }


}
