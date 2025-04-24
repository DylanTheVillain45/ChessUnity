public static class MoveGenerator {
    static readonly Type[] promotablePieces = {Type.King, Type.Bishop, Type.Rook, Type.Queen};

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
        if (piece.type == Type.King) GetCastleMoves(chess, piece);
    }

    static void GetPawnMoves(Chess chess, Piece piece)
    {
        int direction = piece.color == PieceColor.White ? 1 : -1;
        int startRow = piece.color == PieceColor.White ? 1 : 6;
        int enPassantRow = piece.color == PieceColor.White ? 5 : 4;
        int endRow = piece.color == PieceColor.White ? 7 : 0;

        // prevent out of range error
        if (piece.y + direction < 0 || piece.y + direction >= 8) return;

        if (chess.board[piece.y + direction, piece.x] == null)
        {
            if (piece.y == endRow) {
                for (int i = 0; i < promotablePieces.Length; i++) {
                    Move newMove = new Move(piece, piece.y, piece.x, piece.y + direction, piece.x, false, null, false, false, false, true, promotablePieces[i]);
                    Moves.AddMove(chess, newMove);
                }
            } else {
                Move newMove = new Move(piece, piece.y, piece.x, piece.y + direction, piece.x);
                Moves.AddMove(chess, newMove);
            }
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
                    if (piece.y == endRow) {
                        for (int i = 0; i < promotablePieces.Length; i++) {
                            Move newMove = new Move(piece, piece.y, piece.x, piece.y + direction, piece.x, true, capturedPiece, false, false, false, true, promotablePieces[i]);
                            Moves.AddMove(chess, newMove);
                        }
                    } else {
                        Move newMove = new Move(piece, piece.y, piece.x, newY, newX, true, capturedPiece);
                        Moves.AddMove(chess, newMove);
                    }
                }
                else if (capturedPiece == null && enPassantCapturePiece != null && enPassantCapturePiece.type == Type.Pawn && enPassantCapturePiece.color != piece.color && piece.y == enPassantRow)
                {
                    if (isValidEnPassant(chess, piece, newX, direction))
                    {
                        Move newMove = new Move(piece, piece.y, piece.x, newY, newX, true, enPassantCapturePiece, false, false, true);
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
                    Move newMove = new Move(piece, piece.y, piece.x, newY, newX, true, landingSquare);
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
        
    static void GetCastleMoves(Chess chess, Piece king) {
        // if (HasMoved(chess, king)) return;

        int y = king.color == PieceColor.White ? 7 : 0;

        Piece rookShort = chess.board[y, 0];
        if (rookShort != null && rookShort.type == Type.Rook
        //  && !HasMoved(chess, rookShort)
         ) {
            if (IsPathClear(chess, king, true)) {
                if (IsSafeForKing(chess, king, new int[] {2, 1})) {
                    Move newMove = new Move(king, king.y, king.x, king.y, king.x - 2, false, null, true, true);
                    Moves.AddMove(chess, newMove);
                }
            }
        }

        Piece rookLong = chess.board[y, 7];
        if (rookLong != null && rookLong.type == Type.Rook
        //  && !HasMoved(chess, rookLong)
         ) {
            if (IsPathClear(chess, king, false)) {
                if (IsSafeForKing(chess, king, new int[] {4, 5})) {
                    Move newMove = new Move(king, king.y, king.x, king.y, king.x - 2, false, null, true, false);
                    Moves.AddMove(chess, newMove);
                }
            }
        }
    }

    // static bool HasMoved(Game chess, Piece piece) {
    //     foreach (var pastMove in chess.PastMoves) {
    //         if (pastMove.Item1.Contains($"{AlgebraicNotation.RowMap[piece.posX]}{piece.posY + 1}") || pastMove.Item2.Contains($"{AlgebraicNotation.RowMap[piece.posX]}{piece.posY + 1}")) {
    //             return true;
    //         }
    //     }

    //     return false;
    // }

    static bool IsPathClear(Chess chess, Piece king, bool isShort) {
        if (isShort) {
            for (int i = 1; i <= 2; i++) {
                int x = king.x + i;
                if (x < 0 || x > 7) return false;
                if (chess.board[king.y, king.x - i] != null) return false;
            }

            return true;
        } else {
            for (int i = 1; i <= 3; i++) {
                int x = king.x + i;
                if (x < 0 || x > 7) return false;
                if (chess.board[king.y, king.x + i] != null) return false;
            }

            return true;
        }
    }

    static bool IsSafeForKing(Chess chess, Piece king, int[] pathX) {
        int startX = king.x;
        foreach (int x in pathX) {
            if (chess.board[king.y, x] != null) return false;
            chess.board[king.y, king.x] = null;
            king.x = x;
            chess.board[king.y, x] = king;
            if (Check.CheckCheck(chess, king.color)) {
                chess.board[king.y, king.x] = null;
                king.x = startX;
                chess.board[king.y, startX] = king;
                return false;
            }
        }
        chess.board[king.y, king.x] = null;
        king.x = startX;
        chess.board[king.y, startX] = king;
        return true;
    }
}