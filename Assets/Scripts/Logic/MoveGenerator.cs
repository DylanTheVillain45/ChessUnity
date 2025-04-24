public static class MoveGenerator {
    static readonly Type[] promotablePieces = {Type.Knight, Type.Bishop, Type.Rook, Type.Queen};

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
        int enPassantRow = piece.color == PieceColor.White ? 4 : 3;
        int endRow = piece.color == PieceColor.White ? 7 : 0;

        if (piece.y + direction < 0 || piece.y + direction >= 8) return;

        if (chess.board[piece.y + direction, piece.x] == null) {
            if (piece.y + direction == endRow) {
                GameManager.instance.RaiseError("PROMOTION");
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
                    if (newY == endRow) {
                        GameManager.instance.RaiseError("PROMOTION");
                        for (int i = 0; i < promotablePieces.Length; i++) {
                            Move newMove = new Move(piece, piece.y, piece.x, newY, newX, true, capturedPiece, false, false, false, true, promotablePieces[i]);
                            Moves.AddMove(chess, newMove);
                        }
                    } else {
                        Move newMove = new Move(piece, piece.y, piece.x, newY, newX, true, capturedPiece);
                        Moves.AddMove(chess, newMove);
                    }
                }
                else if (enPassantCapturePiece != null && enPassantCapturePiece.type == Type.Pawn && enPassantCapturePiece.color != piece.color && piece.y == enPassantRow)
                {
                    if (isValidEnPassant(chess, enPassantCapturePiece))
                    {
                        Move newMove = new Move(piece, piece.y, piece.x, newY, newX, true, enPassantCapturePiece, false, false, true);
                        Moves.AddMove(chess, newMove);
                    }
                }
            }
        }
    }
    static bool isValidEnPassant(Chess chess, Piece capturePiece) {
        if (chess.PastMoves.Count == 0 || capturePiece == null) {
            GameManager.instance.RaiseError("piece is null");
            return false;
        }


        var lastTurn = chess.PastMoves[^1];
        Move lastMove = capturePiece.color == PieceColor.White ? lastTurn.Item1 : lastTurn.Item2;

        if (lastMove == null || lastMove.piece == null) {
            GameManager.instance.RaiseError("last move is null");
            return false;
        }

        int startRow = capturePiece.color == PieceColor.White ? 1 : 6;

        return lastMove.startY == startRow;
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
        if (HasMoved(chess, king)) {
            return;
        };

        int y = king.color == PieceColor.White ? 0 : 7;

        Piece rookShort = chess.board[y, 0];
        if (rookShort != null && rookShort.type == Type.Rook && rookShort.color == king.color && !HasMoved(chess, rookShort)) {
            if (IsPathClear(chess, king, true)) {
                if (IsSafeForKing(chess, king, new int[] {5, 6})) {
                    Move newMove = new Move(king, king.y, king.x, king.y, king.x + 2, false, null, true, true);
                    Moves.AddMove(chess, newMove);
                }
            }
        }

        Piece rookLong = chess.board[y, 7];
        if (rookLong != null && rookLong.type == Type.Rook && rookLong.color == king.color && !HasMoved(chess, rookShort)) {
            if (IsPathClear(chess, king, false)) {
                if (IsSafeForKing(chess, king, new int[] {3, 2})) {
                    Move newMove = new Move(king, king.y, king.x, king.y, king.x - 2, false, null, true, false);
                    Moves.AddMove(chess, newMove);
                }
            }
        }
    }

    static bool HasMoved(Chess chess, Piece piece) {
        if (piece == null) return true;
        foreach (var move in chess.PastMoves) {
            if (piece.color == PieceColor.White) {
                if (move.Item1 != null && move.Item1.piece == piece) {
                    return true;
                }
            } else {
                if (move.Item2 != null && move.Item2.piece == piece) {
                    return true;
                }
            }
        }

        return false;
    }

    static bool IsPathClear(Chess chess, Piece king, bool isShort) {
        if (isShort) {
            for (int i = 1; i <= 2; i++) {
                int x = king.x + i;
                if (x < 0 || x > 7) return false;
                if (chess.board[king.y, king.x + i] != null) return false;
            }

            return true;
        } else {
            for (int i = 1; i <= 3; i++) {
                int x = king.x - i;
                if (x < 0 || x > 7) return false;
                if (chess.board[king.y, king.x - i] != null) return false;
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
            chess.board[king.y, king.x] = king;
            if (Check.CheckCheck(chess, king.color)) {
                chess.board[king.y, king.x] = null;
                king.x = startX;
                chess.board[king.y, king.x] = king;
                return false;
            }
        }
        chess.board[king.y, king.x] = null;
        king.x = startX;
        chess.board[king.y, king.x] = king;
        return true;
    }
}