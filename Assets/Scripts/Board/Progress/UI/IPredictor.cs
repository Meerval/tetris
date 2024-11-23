using Board.Pieces;

namespace Board.Progress.UI
{
    public interface IPredictor
    {
        void Predict(IPiece piece);
    }
}