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
        bool OnNewGame(Action action);
        bool OnPause(Action action);
        bool OnUnpause(Action action);
    }
}