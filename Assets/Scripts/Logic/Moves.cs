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
        //CommitMove(chess, move);

        //bool checkFilter = Check.CheckCheck(chess, move.piece.color);

        //if (checkFilter)
        //{
        //    UnCommitMove(chess, move);
        //    return;
        //}

        //Color opponentColor = move.piece.color == Color.White ? Color.Black : Color.White;

        //bool isCheck = Check.CheckCheck(chess, opponentColor);

        //if (isCheck)
        //{
        //    Dictionary<string, Move> temp = chess.MoveDictionary;
        //    MoveGenerator.GetMoves(chess, opponentColor);
        //    if (chess.MoveDictionary.Count == 0)
        //    {
        //        move.isCheckMate = true;
        //    }
        //    else
        //    {
        //        move.isCheck = true;
        //    }
        //    chess.MoveDictionary = temp;
        //}

        //UnCommitMove(chess, move);


        //string algebraicNotation = AlgebraicNotation.ToAlgebraicNotation(move, chess);


        //if (chess.MoveDictionary.ContainsKey(algebraicNotation) == false && move.piece.color == chess.color)
        //{
        //    chess.MoveDictionary.Add(algebraicNotation, move);
        //}

        chess.Moves.Add(move);
    }

    public static List<Move> FindMovesWithStart(Chess chess, Piece piece)
    {
        List<Move> moves = new List<Move>();

        for (int i = 0; i < chess.Moves.Count; i++)
        {
            if (chess.Moves[i].piece == piece)
            {
                moves.Add(chess.Moves[i]);
            }
        }

        return moves;
    }
}