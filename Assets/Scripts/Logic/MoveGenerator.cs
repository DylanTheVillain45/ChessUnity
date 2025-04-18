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
        int direction = piece.color == PieceColor.White ? 1 : -1;
        int startRow = piece.color == PieceColor.White ? 1 : 6;

        // prevent out of range error
        if (piece.y + direction < 0 || piece.y + direction >= 8) return;

        if (chess.board[piece.y + direction, piece.x] == null)
        {
            Move newMove = new Move(piece, piece.y, piece.x, piece.y + direction, piece.x);
            Moves.AddMove(chess, newMove);
            if (piece.y == startRow && chess.board[piece.y + direction * 2, piece.x] == null)
            {
                Move newMove2 = new Move(piece, piece.y, piece.x, piece.y + direction * 2, piece.x);
                Moves.AddMove(chess, newMove2);
            }
        }

        for (int dx = -1; dx <= 1; dx += 2)
        {
            int newY = piece.y + direction;
            int newX = piece.x + dx;
            if (newX >= 0 && newX < 8)
            {
                Piece capturedPiece = chess.board[newY, newX];
                Piece enPassantCapturePiece = chess.board[piece.y, newX];
                if (capturedPiece != null && capturedPiece.color != piece.color)
                {
                    Move newMove = new Move(piece, piece.y, piece.x, newY, newX);
                    Moves.AddMove(chess, newMove);
                }
                else if (capturedPiece == null && enPassantCapturePiece != null && enPassantCapturePiece.type == Type.Pawn && enPassantCapturePiece.color != piece.color)
                {
                    if (isValidEnPassant(chess, piece, newX, direction))
                    {
                        Move newMove = new Move(piece, piece.y, piece.x, newY, newX);
                        Moves.AddMove(chess, newMove);
                    }
                }
            }
        }
    }
    static bool isValidEnPassant(Chess chess, Piece piece, int newX, int direction)
    {
        //string lastMove = piece.color == Color.White ? chess.PastMoves[^1].Item2 : chess.PastMoves[^1].Item1;

        //if (lastMove != $"{AlgebraicNotation.RowMap[newX]}{piece.posY + 1}") return false;

        //for (int i = 0; i < chess.PastMoves.Count - 1; i++)
        //{
        //    string pastMove = piece.color == Color.White ? chess.PastMoves[i].Item2 : chess.PastMoves[i].Item1;
        //    if (pastMove == $"{AlgebraicNotation.RowMap[newX]}{piece.posY + direction + 1}")
        //    {
        //        return false;
        //    }
        //}

        return true;
    }
    static void GetNonPawnMoves(Chess chess, Piece piece)
    {
        foreach (var (dx, dy, repeatable) in Moves.PieceMap[piece.type])
        {
            int newX = piece.x + dx;
            int newY = piece.y + dy;

            while (newX < 8 && newX >= 0 && newY < 8 && newY >= 0)
            {
                Piece landingSquare = chess.board[newY, newX];

                if (landingSquare == null)
                {
                    Move newMove = new Move(piece, piece.y, piece.x, newY, newX);
                    Moves.AddMove(chess, newMove);
                }

                else if (landingSquare.color != piece.color)
                {
                    Move newMove = new Move(piece, piece.y, piece.x, newY, newX);
                    Moves.AddMove(chess, newMove);
                    break;
                }

                else break;

                if (!repeatable) break;

                newY += dy;
                newX += dx;
            }
        }
    }
}