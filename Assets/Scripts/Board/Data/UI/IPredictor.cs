using Board.Pieces;

namespace Board.Data.UI
{
    public interface IPredictor
    {
        void Predict(IPiece piece);
    }
}