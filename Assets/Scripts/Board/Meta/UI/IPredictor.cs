using Board.Pieces;

namespace Board.Meta.UI
{
    public interface IPredictor
    {
        void Predict(IPiece piece);
    }
}