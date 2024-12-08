using System;
using UnityEngine;

namespace Board.Pieces
{
    public enum EPiece
    {
        I,
        J,
        L,
        O,
        S,
        T,
        Z
    }

    public static class EPieceParser
    {
        public static EPiece Parse(char code)
        {
            if (Enum.TryParse(code.ToString(), out EPiece pieceType))
            {
                return pieceType;
            }

            Debug.LogWarning($"There is no EPiece with code {code}. Piece selected by default");
            return EPiece.I;
        }
    }
}