namespace Progress
{
    public interface IConditionController
    {
        State Status();
        float PieceDropDelay();
        int Level();
        int Step();
        int Score();
    }
}