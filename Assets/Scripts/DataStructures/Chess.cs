using System.Collections.Generic;

public class Chess {
    public Piece[,] board;
    public List<Move> MovesList;
    public PieceColor gameColor;
    
    public List<(Move, Move)> PastMoves = new List<(Move, Move)>();


    public void GetMoves() {
        MovesList = new List<Move>();

        MoveGenerator.GetAllMoves(this, this.gameColor);
    }

    public void MakeMove(Move move) {
        Moves.CommitMove(this, move);

        if (gameColor == PieceColor.White) {
            PastMoves.Add((move, null));
        } else {
            PastMoves[^1] = (PastMoves[^1].Item1, move);
        }

        gameColor = gameColor == PieceColor.White ? PieceColor.Black : PieceColor.White;
    }
}