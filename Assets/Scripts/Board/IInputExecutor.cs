using System;

namespace Board
{
    public interface IInputExecutor
    {
        bool OnRotationLeft(Func<bool> action, out bool isRotated);
        bool OnRotationRight(Func<bool> action, out bool isRotated);
        bool OnShiftLeft(Func<bool> action, out bool isShifted);
        bool OnShiftRight(Func<bool> action, out bool isShifted);
        bool OnShiftDown(Func<bool> action, out bool isShifted);
        void OnNewGame(Action action);
        void OnPauseGame(Action action);
    }
}