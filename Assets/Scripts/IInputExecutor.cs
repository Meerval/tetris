using System;

public interface IInputExecutor
{
    bool OnRotationLeft(Action action);
    bool OnRotationRight(Action action);
    bool OnShiftLeft(Action action);
    bool OnShiftRight(Action action);
    bool OnShiftDown(Action action);
}