public class Move
{
    public Piece piece;
    public int startY;
    public int startX;
    public int endY;
    public int endX;

    public Move(Piece piece, int startY, int startX, int endY, int endX){
        this.piece = piece;
        this.startY = startY;
        this.startX = startX;
        this.endY = endY;
        this.endX = endX;
    }

}
