using System.Collections.Generic;

public class Chess {
    public Piece[,] board;
    public List<Move> MovesList;
    public PieceColor gameColor;

    public void GetMoves() {
        MovesList = new List<Move>();

        MoveGenerator.GetAllMoves(this, this.gameColor);
    }

    public void MakeMove(Move move) {
        Moves.CommitMove(this, move);
        gameColor = gameColor == PieceColor.White ? PieceColor.Black : PieceColor.White;
    }
}