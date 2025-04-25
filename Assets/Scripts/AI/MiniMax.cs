public static class MiniMax {
    public static int GetBestMove(int[] board, List<int> posMoves, bool isX) {
        int bestScore = -9999;
        int bestMove = 4;

        HelperFunction.RandomizeList(posMoves);

        foreach (int move in posMoves) {
            HelperFunction.CommitMove(board, move, isX);

            int score;
            if (HelperFunction.CheckWin(board, move) != 0) {
                score = 10;
            } else {
                score = Beta(board, !isX, 0);
            }

            if (score > bestScore) {
                bestScore = score;
                bestMove = move;
            }

            HelperFunction.UndoMove(board, move);
        }

        return bestMove;
    } 

    static int Alpha(int[] board, bool isX, int depth) {
        List<int> posMoves = HelperFunction.GetMoves(board);
        if (posMoves.Count == 0) return 0;
        int bestScore = -9999;

        foreach (int move in posMoves) {
            HelperFunction.CommitMove(board, move, isX);

            int score;
            if (HelperFunction.CheckWin(board, move) != 0) {
                score = 10 - depth;
            } else {
                score = Beta(board, !isX, depth + 1);
            }

            if (bestScore < score) {
                bestScore = score;
            }

            HelperFunction.UndoMove(board, move);
        }

        return bestScore;

    }

    static int Beta(int[] board, bool isX, int depth) {
        List<int> posMoves = HelperFunction.GetMoves(board);
        if (posMoves.Count == 0) return 0;
        int bestScore = 9999;

        foreach (int move in posMoves) {
            HelperFunction.CommitMove(board, move, isX);

            int score;
            if (HelperFunction.CheckWin(board, move) != 0) {
                score = -10 + depth;
            } else {
                score = Alpha(board, !isX, depth + 1);
            }

            if (bestScore > score) {
                bestScore = score;
            }

            HelperFunction.UndoMove(board, move);
        }

        return bestScore;

    }
}