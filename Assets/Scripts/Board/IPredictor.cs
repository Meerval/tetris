using Board.Pieces;

namespace Board
{
    public interface IPredictor
    {
        void Predict(IPiece piece);
    }
}