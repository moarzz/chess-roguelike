using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class piece : MonoBehaviour
{
    [SerializeField] private char pieceId;
    public bool IsWhite;
    private Vector2 PiecePosition;
    [SerializeField] private Grid_Manager grid;
    [SerializeField] private Sprite[] PieceSpriteList;
    public List<Tile> AvailableMoves;
    [SerializeField] private SpriteRenderer Renderer;
    private Grid_Manager Grid;


    public void Init(Grid_Manager grid, Vector2 initialposition)
    {
        Grid = grid;
        PiecePosition = initialposition;
    }
    private void Start()
    {
        AssignSprite();
    }
    public void GetAvailableMoves()
    {
        switch (pieceId)
        {
            case 'K':
                AvailableMoves = Grid.GetLinesInDirections(PiecePosition, new List<string> { "U", "D", "L", "R", "UL", "UR", "DL", "DR" }, 1);
                break;
        }
        Grid.SetTilesAvailable(AvailableMoves);
    }


    public void MovePiece(Vector2 MovePosition)
    {
        if(grid.GetTileAtPos(MovePosition) == null)
        {
            print("Tile where piece wants to move doesn't exist");
        }
        else if(grid.GetTileAtPos(MovePosition).occupied)
        {
            print("eat this piece");
        }
        else
        {
            this.transform.position = new Vector3(MovePosition.x, MovePosition.y, -1);
            grid.GetTileAtPos(MovePosition).PieceOnTile = this;
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
