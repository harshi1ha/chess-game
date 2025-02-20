using System;
using UnityEngine;

public sealed class ChessBoardPlacementHandler : MonoBehaviour
{
    [SerializeField] private GameObject[] _rowsArray; // Rows of the board (set in the Inspector)
    [SerializeField] private GameObject _highlightPrefab; // Prefab for highlighting cells
    private GameObject[,] _chessBoard; // 2D array to store board tiles

    public static ChessBoardPlacementHandler Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("Multiple instances of ChessBoardPlacementHandler detected.");
            Destroy(gameObject);
            return;
        }
        GenerateArray();
    }

    private void GenerateArray()
    {
        _chessBoard = new GameObject[8, 8];
        for (var row = 0; row < 8; row++)
        {
            for (var col = 0; col < 8; col++)
            {
                _chessBoard[row, col] = _rowsArray[row].transform.GetChild(col).gameObject;
            }
        }
    }

    public GameObject GetTile(int row, int col)
    {
        if (IsValidPosition(row, col))
        {
            return _chessBoard[row, col];
        }
        Debug.LogError($"Invalid tile position: ({row}, {col})");
        return null;
    }

    public void Highlight(int row, int col)
    {
        var tile = GetTile(row, col);
        if (tile == null) return;

        if (!HasHighlight(tile))
        {
            var highlight = Instantiate(_highlightPrefab, tile.transform.position + new Vector3(0, 0, -0.1f), Quaternion.identity, tile.transform);

            var spriteRenderer = highlight.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                spriteRenderer.sortingLayerName = "Highlight";
                spriteRenderer.sortingOrder = 5;
            }
        }
    }

    public void ClearHighlights()
    {
        foreach (var tile in _chessBoard)
        {
            for (int i = tile.transform.childCount - 1; i >= 0; i--)
            {
                var child = tile.transform.GetChild(i);
                if (child.name == _highlightPrefab.name + "(Clone)")
                {
                    Destroy(child.gameObject);
                }
            }
        }
    }

    public bool IsValidPosition(int row, int col)
    {
        return row >= 0 && row < 8 && col >= 0 && col < 8;
    }

    private bool HasHighlight(GameObject tile)
    {
        foreach (Transform child in tile.transform)
        {
            if (child.name == _highlightPrefab.name + "(Clone)")
            {
                return true;
            }
        }
        return false;
    }

    public bool HasPiece(int row, int col)
    {
        var tile = GetTile(row, col);
        if (tile == null) return false;

        foreach (Transform child in tile.transform)
        {
            if (child.CompareTag("ChessPiece"))
            {
                return true;
            }
        }
        return false;
    }

    public void MovePiece(ChessPiece piece, int newRow, int newCol)
    {
        if (piece == null)
        {
            Debug.LogError("MovePiece called with a null piece!");
            return;
        }

        // Update logical position
        piece.row = newRow;
        piece.column = newCol;

        // Move the piece to the correct tile visually
        Transform newTile = GetTile(newRow, newCol).transform;
        piece.transform.SetParent(newTile);
        piece.transform.localPosition = Vector3.zero; // Ensure perfect centering

        Debug.Log($"{piece.GetType().Name} moved to ({newRow}, {newCol})");
    }


    public void DebugTileContents(int row, int col)
    {
        var tile = GetTile(row, col);
        if (tile == null)
        {
            Debug.LogError($"Tile at ({row}, {col}) is invalid.");
            return;
        }

        Debug.Log($"Tile ({row}, {col}) has {tile.transform.childCount} children.");
        foreach (Transform child in tile.transform)
        {
            Debug.Log($"Child: {child.name}");
        }
    }

    #region Highlight Testing (Optional)

    // Uncomment to test highlighting
    // private void Start() {
    //     StartCoroutine(Testing());
    // }

    // private IEnumerator Testing() {
    //     Highlight(2, 7);
    //     yield return new WaitForSeconds(1f);
    //
    //     ClearHighlights();
    //     Highlight(2, 7);
    //     Highlight(2, 6);
    //     Highlight(2, 5);
    //     yield return new WaitForSeconds(1f);
    //
    //     ClearHighlights();
    //     Highlight(7, 7);
    //     Highlight(2, 7);
    //     yield return new WaitForSeconds(1f);
    // }

    #endregion

}using System;
using UnityEngine;

public sealed class ChessBoardPlacementHandler : MonoBehaviour
{
    [SerializeField] private GameObject[] _rowsArray; // Rows of the board (set in the Inspector)
    [SerializeField] private GameObject _highlightPrefab; // Prefab for highlighting cells
    private GameObject[,] _chessBoard; // 2D array to store board tiles

    public static ChessBoardPlacementHandler Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("Multiple instances of ChessBoardPlacementHandler detected.");
            Destroy(gameObject);
            return;
        }
        GenerateArray();
    }

    private void GenerateArray()
    {
        _chessBoard = new GameObject[8, 8];
        for (var row = 0; row < 8; row++)
        {
            for (var col = 0; col < 8; col++)
            {
                _chessBoard[row, col] = _rowsArray[row].transform.GetChild(col).gameObject;
            }
        }
    }

    public GameObject GetTile(int row, int col)
    {
        if (IsValidPosition(row, col))
        {
            return _chessBoard[row, col];
        }
        Debug.LogError($"Invalid tile position: ({row}, {col})");
        return null;
    }

    public void Highlight(int row, int col)
    {
        var tile = GetTile(row, col);
        if (tile == null) return;

        if (!HasHighlight(tile))
        {
            var highlight = Instantiate(_highlightPrefab, tile.transform.position + new Vector3(0, 0, -0.1f), Quaternion.identity, tile.transform);

            var spriteRenderer = highlight.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                spriteRenderer.sortingLayerName = "Highlight";
                spriteRenderer.sortingOrder = 5;
            }
        }
    }

    public void ClearHighlights()
    {
        foreach (var tile in _chessBoard)
        {
            for (int i = tile.transform.childCount - 1; i >= 0; i--)
            {
                var child = tile.transform.GetChild(i);
                if (child.name == _highlightPrefab.name + "(Clone)")
                {
                    Destroy(child.gameObject);
                }
            }
        }
    }

    public bool IsValidPosition(int row, int col)
    {
        return row >= 0 && row < 8 && col >= 0 && col < 8;
    }

    private bool HasHighlight(GameObject tile)
    {
        foreach (Transform child in tile.transform)
        {
            if (child.name == _highlightPrefab.name + "(Clone)")
            {
                return true;
            }
        }
        return false;
    }

    public bool HasPiece(int row, int col)
    {
        var tile = GetTile(row, col);
        if (tile == null) return false;

        foreach (Transform child in tile.transform)
        {
            if (child.CompareTag("ChessPiece"))
            {
                return true;
            }
        }
        return false;
    }

    public void MovePiece(ChessPiece piece, int newRow, int newCol)
    {
        if (piece == null)
        {
            Debug.LogError("MovePiece called with a null piece!");
            return;
        }

        // Update logical position
        piece.row = newRow;
        piece.column = newCol;

        // Move the piece to the correct tile visually
        Transform newTile = GetTile(newRow, newCol).transform;
        piece.transform.SetParent(newTile);
        piece.transform.localPosition = Vector3.zero; // Ensure perfect centering

        Debug.Log($"{piece.GetType().Name} moved to ({newRow}, {newCol})");
    }


    public void DebugTileContents(int row, int col)
    {
        var tile = GetTile(row, col);
        if (tile == null)
        {
            Debug.LogError($"Tile at ({row}, {col}) is invalid.");
            return;
        }

        Debug.Log($"Tile ({row}, {col}) has {tile.transform.childCount} children.");
        foreach (Transform child in tile.transform)
        {
            Debug.Log($"Child: {child.name}");
        }
    }

    #region Highlight Testing (Optional)

    // Uncomment to test highlighting
    // private void Start() {
    //     StartCoroutine(Testing());
    // }

    // private IEnumerator Testing() {
    //     Highlight(2, 7);
    //     yield return new WaitForSeconds(1f);
    //
    //     ClearHighlights();
    //     Highlight(2, 7);
    //     Highlight(2, 6);
    //     Highlight(2, 5);
    //     yield return new WaitForSeconds(1f);
    //
    //     ClearHighlights();
    //     Highlight(7, 7);
    //     Highlight(2, 7);
    //     yield return new WaitForSeconds(1f);
    // }

    #endregion

}