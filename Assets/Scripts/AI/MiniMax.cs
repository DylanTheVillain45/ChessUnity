using System.Collections.Generic;
using System;

public static class MiniMax {
    public static Move GetBestMove(Chess chess, int maxDepth) {
        int bestScore = -9999;
        int alpha = -9999;
        int beta = 9999;
        Move bestMove = null;

        MoveListOrdering.OrderForABPrune(chess.MovesList);

        List<Move> posMoves = MoveListOrdering.CreateDeepCopy(chess.MovesList);

        foreach (Move move in posMoves) {
            Moves.CommitMove(chess, move);
            chess.gameColor = move.piece.color == PieceColor.White ? PieceColor.Black : PieceColor.White;

            int score;
            if (move.isCheckMate) {
                score = 1000;
            } else {
                score = Beta(chess, alpha, beta, 1, maxDepth);
            }

            if (score > bestScore) {
                bestScore = score;
                bestMove = move;
            }

            alpha = Math.Max(alpha, bestScore);

            chess.gameColor = move.piece.color;
            Moves.UnCommitMove(chess, move);
        }

        return bestMove;
    } 

    static int Alpha(Chess chess, int alpha, int beta, int depth, int maxDepth) {
        if (depth > maxDepth) return Evalution.EvalBoard(chess);      

        chess.GetMoves();  
        List<Move> posMoves = MoveListOrdering.CreateDeepCopy(chess.MovesList);

        if (posMoves.Count == 0) return 0;
        int bestScore = -9999;

        foreach (Move move in posMoves) {
            Moves.CommitMove(chess, move);

            int score;
            if (move.isCheckMate) {
                score = 1000 - depth * 100;
            } else {
                score = Beta(chess, alpha, beta, depth + 1, maxDepth);
            }

            if (bestScore < score) {
                bestScore = score;
            }

            chess.gameColor = move.piece.color;
            Moves.UnCommitMove(chess, move);
        }

        return bestScore;
    }

    static int Beta(Chess chess, int alpha, int beta, int depth, int maxDepth) {
        if (depth > maxDepth) return Evalution.EvalBoard(chess);

        chess.GetMoves();
        List<Move> posMoves = MoveListOrdering.CreateDeepCopy(chess.MovesList);

        if (posMoves.Count == 0) return 0;
        int bestScore = 9999;

        foreach (Move move in posMoves) {
            Moves.CommitMove(chess, move);

            int score;
            if (move.isCheckMate) {
                score = -1000 + depth * 100;
            } else {
                score = Beta(chess, alpha, beta, depth + 1, maxDepth);
            }

            if (bestScore > score) {
                bestScore = score;
            }

            chess.gameColor = move.piece.color;
            Moves.UnCommitMove(chess, move);
        }

        return bestScore;

    }
}