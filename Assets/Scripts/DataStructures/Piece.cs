using UnityEngine;

public class Piece : MonoBehaviour
{
    public Type type;
    public PieceColor color;
    public int y;
    public int x;
    public SpriteRenderer spriteRenderer;

    public void SetPiece(Type type, PieceColor color, int y, int x, Sprite sprite){
        this.type = type;
        this.color = color;
        this.y = y;
        this.x = x;
        this.spriteRenderer.sprite = sprite;
    }
}