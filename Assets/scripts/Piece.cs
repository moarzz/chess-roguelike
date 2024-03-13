using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class piece : MonoBehaviour
{
    private char pieceId;
    private Vector2 piecePosition;
    [SerializeField] private Grid_Manager grid;
    private List<Tile> Available_moves;

    public void GetAvailableMoves()
    {
        Vector2 TileLocation;
        switch (pieceId)
        {
            case 'K':
                TileLocation = new Vector2(0, 0);
                Available_moves.Add(grid.GetTileAtPos(TileLocation));
                break;
        }
    }


    public void MovePiece(Vector2 MovePosition)
    {
        if(grid.GetTileAtPos(MovePosition) == null)
        {
            print("can't move piece to this tile");
        }
        else if(grid.GetTileAtPos(MovePosition).IsOccupied())
        {
            print("eat this piece");
        }
        else
        {
            this.transform.position = new Vector3(MovePosition.x, MovePosition.y, -1);
            grid.GetTileAtPos(MovePosition).SetPieceOnThis(this);
        }
    }


}
