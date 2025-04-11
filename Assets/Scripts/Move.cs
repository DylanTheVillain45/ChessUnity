using UnityEngine;

public class Move : MonoBehaviour
{
    public Piece piece;
    public int startY;
    public int startX;
    public int endY;
    public int endX;

    public void SetPiece(Piece piece, int startY, int startX, int x, Sprite sprite){
        this.type = type;
        this.color = color;
        this.y = y;
        this.x = x;
        this.spriteRenderer.sprite = sprite;
    }
}
