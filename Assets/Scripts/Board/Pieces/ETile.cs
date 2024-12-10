using System;
using UnityEngine;

namespace Board.Pieces
{
    public enum ETile
    {
        C,
        B,
        G,
        O,
        P,
        R,
        Y,
        Null
    }

    public static class ETileParser
    {
        public static ETile Parse(char code)
        {
            if (code == '0')
            {
                return ETile.Null;
            }

            if (Enum.TryParse(code.ToString(), out ETile pieceType))
            {
                return pieceType;
            }

            Debug.LogWarning($"There is no ETile with code {code}. Piece selected by default");
            return ETile.Null;
        }
    }
}