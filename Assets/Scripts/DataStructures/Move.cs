public class Move
{
    public Piece piece;
    public int startY;
    public int startX;
    public int endY;
    public int endX;
    public Piece capturedPiece;
    public Type promotionPiece;

    public bool isCapture;
    public bool isCheck; 
    public bool isShortCastle;
    public bool isCastle;
    public bool isEnpassant;
    public bool isPromotion;

    public bool isCheckMate;
    public bool isStaleMate;

    public Move(Piece piece, int startY, int startX, int endY, int endX, bool isCapture = false, Piece capturedPiece = null, bool isCastle = false, bool isShortCastle = false, bool isEnpassant = false, bool isPromotion = false, Type promotionPiece = Type.Pawn) {
        this.piece = piece;

        this.startY = startY;
        this.startX = startX;
        this.endY = endY;
        this.endX = endX;
        
        this.isCapture = isCapture;
        this.capturedPiece = capturedPiece;
        this.isCastle = isCastle;
        this.isShortCastle = isShortCastle;
        this.isEnpassant = isEnpassant;
        this.isPromotion = isPromotion;
        this.promotionPiece = promotionPiece;
    }

    public Move Clone()
{
    return new Move(
        piece,
        startY,
        startX,
        endY,
        endX,
        isCapture,
        capturedPiece != null ? capturedPiece : null,
        isCastle,
        isShortCastle,
        isEnpassant,
        isPromotion,
        promotionPiece
    )
    {
        isCheck = this.isCheck,
        isCheckMate = this.isCheckMate,
        isStaleMate = this.isStaleMate
    };
}

}
