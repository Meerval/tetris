using System.Collections.Generic;
using System.Linq;
using Pieces;
using Progress;
using UnityEngine;

public class TetrisMeta : MonoBehaviour, IMeta
{
    private IProgress<EState> _state;
    private IProgress<bool> _updateLock;
    private IProgress<float> _pieceDropDelay;
    private IProgress<List<(int, IPiece)>> _spawnPieces;
    private IPiece _activePiece;
    private IProgress<int> _level;
    private IProgress<int> _score;

    public static IMeta Instance;

    private TetrisMeta()
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

        _state = gameObject.AddComponent<State>();
        _updateLock = gameObject.AddComponent<UpdateLock>();
        _pieceDropDelay = gameObject.AddComponent<PieceDropDelay>();
        _spawnPieces = gameObject.AddComponent<SpawnedPieces>();
        _level = GetComponentInChildren<Level>();
        _score = GetComponentInChildren<Score>();
    }

    public EState State()
    {
        return _state.Value();
    }

    public bool IsUpdateLocked()
    {
        return _updateLock.Value();
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
        _activePiece = _spawnPieces.Value().Last().Item2;
        return _spawnPieces.Value();
    }

    public IPiece ActivePiece()
    {
        return _activePiece;
    }
}