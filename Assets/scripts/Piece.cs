using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class piece : MonoBehaviour
{
    [SerializeField] private char pieceId;
    public bool IsWhite;
    private Vector2 piecePosition;
    [SerializeField] private Grid_Manager grid;
    [SerializeField] private Sprite[] PieceSpriteList;
    private List<Tile> Available_moves;
    [SerializeField] private SpriteRenderer Renderer;


    private void Start()
    {
        AssignSprite();
    }
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

    public void AssignSprite()
    {
        int SpriteIndex;
        switch (pieceId)
        {
            case 'K': SpriteIndex = 0;
                break;
            case 'Q': SpriteIndex = 1;
                break;
            case 'R':
                SpriteIndex = 2;
                break;
            case 'H':
                SpriteIndex = 3;
                break;
            case 'B':
                SpriteIndex = 4;
                break;
            case 'P':
                SpriteIndex = 5;
                break;
            default :
                SpriteIndex = 0;
                break;
        }
        if (IsWhite)
        {
            SpriteIndex += 6;
        }

        Renderer.sprite = PieceSpriteList[SpriteIndex];
    }
    

}
