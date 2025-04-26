using System.Collections.Generic;

public static class MoveListOrdering {
    public static void OrderForABPrune(List<Move> moveList) {
        
    }

    public static List<Move> CreateDeepCopy(List<Move> moveList) {
        List<Move> newMoves = new List<Move>();

        foreach (Move move in moveList) {
            newMoves.Add(move.Clone());
        }

        return newMoves;
    }
}