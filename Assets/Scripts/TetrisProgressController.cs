using System.Collections.Generic;
using Pieces;
using Progress;
using UnityEngine;

public class TetrisProgressController : MonoBehaviour, IConditionController
{
    private IProgress<State> _state;
    private IProgress<float> _pieceDropDelay;
    private IProgress<List<(int, IPiece)>> _spawnPieces;
    private IProgress<int> _level;
    private IProgress<int> _score;

    public static IConditionController Instance;

    private TetrisProgressController()
    {
    }

    public void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        _state = gameObject.AddComponent<Status>();
        _pieceDropDelay = gameObject.AddComponent<PieceDropDelay>();
        _spawnPieces = gameObject.AddComponent<SpawnedPieces>();
        _level = gameObject.AddComponent<Level>();
        _score = gameObject.AddComponent<Score>();
    }

    public State Status()
    {
        return _state.Value();
    }

    public float PieceDropDelay()
    {
        return _pieceDropDelay.Value();
    }

    public int Level()
    {
        return _level.Value();
    }

    public int Score()
    {
        return _score.Value();
    }

    public List<(int, IPiece)> SpawnPieces()
    {
        return _spawnPieces.Value();
    }
}