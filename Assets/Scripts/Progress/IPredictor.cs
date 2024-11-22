using Pieces;

namespace Progress
{
    public interface IPredictor
    {
        void Predict(IPiece piece);
    }
}