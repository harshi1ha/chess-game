using UnityEngine;

public class ChessPlayerPlacementHandler : MonoBehaviour
{
    [SerializeField] private int row, column; // Editable in Inspector
    [SerializeField] private bool isWhite; // Supports both White and Black pieces

    private ChessPiece piece;

    private void Start()
    {
        piece = GetComponent<ChessPiece>();
        if (piece != null)
        {
            piece.isWhite = isWhite; // Set piece color
            UpdatePiecePosition(); // Ensure initial position is correct
        }
    }

    private void Update()
    {
        // If row or column is changed in Inspector, update the piece
        if (piece != null && (piece.row != row || piece.column != column))
        {
            UpdatePiecePosition();
        }
    }

    private void UpdatePiecePosition()
    {
        // Sync ChessPiece position and color
        piece.row = row;
        piece.column = column;
        piece.PlacePiece(row, column);

        Debug.Log($"{piece.GetType().Name} updated to ({row}, {column}), IsWhite: {piece.isWhite}");
    }
}