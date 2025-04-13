public static class MoveGenerator {
    public static void GetAllMoves(Chess chess, PieceColor color) {
        for (int i = 0; i < 8; i++) {
            for (int j = 0; j < 8; j++) {
                if (chess.board[i, j] != null && chess.board[i, j].color == color) {
                    GetMoves(chess, chess.board[i, j]);
                }
            }
        }

    }

    static void GetMoves(Chess chess, Piece piece) {
        if (piece.type == Type.Pawn) GetPawnMoves(chess, piece);
        else GetNonPawnMoves(chess, piece);
        // if (piece.type == Type.King) GetCastleMoves();
    }

    static void GetPawnMoves(Chess chess, Piece piece)
    {

    }
    static void GetNonPawnMoves(Chess chess, Piece piece)
    {

    }
}