using System.Collections.Generic;
using System.Drawing;

public static class Moves
{
    public static readonly Dictionary<Type, List<(int, int, bool)>> PieceMap = new Dictionary<Type, List<(int, int, bool)>> {
        {Type.Knight, new List<(int, int, bool)> {(1, 2, false), (2, 1, false), (-1, 2, false), (-2, 1, false), (1, -2, false), (2, -1, false), (-1, -2, false), (-2, -1, false)}},
        {Type.Bishop, new List<(int, int, bool)> {(1, 1, true), (-1, 1, true), (1, -1, true), (-1, -1, true)}},
        {Type.Rook, new List<(int, int, bool)> {(1, 0, true), (-1, 0, true), (0, 1, true), (0, -1, true)}},
        {Type.Queen, new List<(int, int, bool)> {(1, 1, true), (-1, 1, true), (1, -1, true), (-1, -1, true), (1, 0, true), (-1, 0, true), (0, 1, true), (0, -1, true)}},
        {Type.King, new List<(int, int, bool)> {(1, 1, false), (-1, 1, false), (1, -1, false), (-1, -1, false), (1, 0, false), (-1, 0, false), (0, 1, false), (0, -1, false)}},
    };

    public static void AddMove(Chess chess, Move move)
    {
        CommitMove(chess, move);

        bool checkFilter = Check.CheckCheck(chess, move.piece.color);

        if (checkFilter)
        {
           UnCommitMove(chess, move);
           return;
        }

        PieceColor opponentColor = move.piece.color == PieceColor.White ? PieceColor.Black : PieceColor.White;

        bool isCheck = Check.CheckCheck(chess, opponentColor);

        if (isCheck)
        {
           List<Move> temp = chess.MovesList;
           MoveGenerator.GetAllMoves(chess, opponentColor);
           if (chess.MovesList.Count == 0) {
               move.isCheckMate = true;
           }
           else {
               move.isCheck = true;
           }
           chess.MovesList = temp;
        }

        UnCommitMove(chess, move);

        chess.MovesList.Add(move);
    }

    public static void CommitMove(Chess chess, Move move) {
        int startY = move.startY;
        int startX = move.startX;
        int endY = move.endY;
        int endX = move.endX;
        
        Piece piece = move.piece;
        (piece.x, piece.y) = (endX, endY);

        chess.board[startY, startX] = null;
        chess.board[endY, endX] = piece;

        if (move.isCastle) {
            int rookStartX = move.isShortCastle ? 0 : 7;
            int rookEndX = move.isShortCastle ? 2 : 4; 

            Piece rook = chess.board[startY, rookStartX];
            if (rook != null && rook.type == Type.Rook) {
                rook.x = rookEndX;
                chess.board[startY, rookEndX] = rook;
                chess.board[startY, rookStartX] = null;
            }
        }

        if (move.isEnpassant) {
            chess.board[startY, endX] = null;
        }
    }

    public static void UnCommitMove(Chess chess, Move move) {
        int startY = move.startY;
        int startX = move.startX;
        int endY = move.endY;
        int endX = move.endX;
        
        Piece piece = move.piece;
        Piece capturedPiece = move.capturedPiece;
        (piece.x, piece.y) = (startX, startY);

        
        if (move.isCastle) {
            int rookStartX = move.isShortCastle ? 0 : 7;
            int rookEndX = move.isShortCastle ? 2 : 4; 

            Piece rook = chess.board[startY, rookEndX];
            if (rook != null && rook.type == Type.Rook) {
                rook.x = rookStartX;
                chess.board[startY, rookStartX] = rook;
                chess.board[startY, rookEndX] = null;
            }
        }

        if (move.isCapture) {
            if (move.isEnpassant) {
                chess.board[startY, endX] = capturedPiece;
            } else {
                chess.board[endY, endX] = capturedPiece;
            }
        }      
        else {
            chess.board[move.endY, move.endX] = null;    
        } 
    }

    public static List<Move> FindMovesWithStart(Chess chess, Piece piece)
    {
        List<Move> moves = new List<Move>();

        for (int i = 0; i < chess.MovesList.Count; i++)
        {
            if (chess.MovesList[i].piece == piece)
            {
                moves.Add(chess.MovesList[i]);
            }
        }

        return moves;
    }

    public static Move FindMoveWithEndAndList(List<Move> moves, int endY, int endX)
    {
        for (int i = 0; i < moves.Count; i++)
        {
            if (moves[i].endY == endY && moves[i].endX == endX)
            {
                return moves[i];
            }
        }
        return null;
    }

}