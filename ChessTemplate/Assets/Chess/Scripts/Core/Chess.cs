using UnityEngine;

public class ChessPiece : MonoBehaviour
{
    public int row, column;
    public bool isWhite; // True for White, False for Black

    private void Start()
    {
        PlacePiece(row, column);
    }

    // Ensure the piece moves to the correct board position
    public void PlacePiece(int r, int c)
    {
        row = r;
        column = c;

        var tile = ChessBoardPlacementHandler.Instance.GetTile(row, column);
        if (tile == null)
        {
            Debug.LogError($"Invalid tile at ({row}, {column})");
            return;
        }

        // Parent the piece to the tile and center it
        transform.SetParent(tile.transform);
        transform.localPosition = Vector3.zero;

        Debug.Log($"{name} placed at ({row}, {column}) with parent {tile.name}");
    }

    // Automatically sync position if row/column changes (for Inspector updates)
    private void Update()
    {
        // Check if position needs an update
        if (transform.parent != ChessBoardPlacementHandler.Instance.GetTile(row, column)?.transform)
        {
            UpdatePosition();
        }
    }

    // Update physical position and hierarchy
    private void UpdatePosition()
    {
        var tile = ChessBoardPlacementHandler.Instance.GetTile(row, column);
        if (tile == null)
        {
            Debug.LogError($"Invalid tile at ({row}, {column}) for {name}");
            return; // Avoid errors if the tile is null
        }

        // Set position and parent correctly
        transform.position = tile.transform.position;
        transform.SetParent(tile.transform); // Ensure correct parenting

        Debug.Log($"{name} updated to ({row}, {column}) with parent {tile.name}");
    }

    // Triggered when the piece is clicked
    private void OnMouseDown()
    {
        Debug.Log($"{GetType().Name} clicked at ({row}, {column})");

        // Clear old highlights and calculate new ones
        ChessBoardPlacementHandler.Instance.ClearHighlights();
        HighlightMoves();
    }

    // Virtual allows derived classes (e.g., Rook, Knight) to override it
    public virtual void HighlightMoves()
    {
        Debug.Log("Default highlight logic in ChessPiece.");
    }
}