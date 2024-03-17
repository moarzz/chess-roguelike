using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class piece : MonoBehaviour
{
    [SerializeField] private char pieceId;
    public bool IsWhite;
    private Vector2 PiecePosition;
    [SerializeField] private Sprite[] PieceSpriteList;
    public List<Tile> AvailableMoves;
    [SerializeField] private SpriteRenderer Renderer;
    private Grid_Manager Grid;


    public void Init(Grid_Manager grid, Vector2 initialposition, char id, bool iswhite)
    {
        IsWhite = iswhite;
        pieceId = id;
        Grid = grid;
        PiecePosition = initialposition;
    }
    private void Start()
    {
        AssignSprite();
    }
    public void GetAvailableMoves()
    {
        AvailableMoves = new List<Tile>();
        switch (pieceId)
        {
            case 'K':
                AvailableMoves = Grid.GetLinesInDirections(PiecePosition, new List<string> { "U", "D", "L", "R", "UL", "UR", "DL", "DR" }, 1, IsWhite);
                break;
            case 'Q':
                AvailableMoves = Grid.GetLinesInDirections(PiecePosition, new List<string> { "U", "D", "L", "R", "UL", "UR", "DL", "DR" }, 8, IsWhite);
                break;
            case 'B':
                AvailableMoves = Grid.GetLinesInDirections(PiecePosition, new List<string> { "UL", "UR", "DL", "DR" }, 8, IsWhite);
                break;
            case 'R':
                AvailableMoves = Grid.GetLinesInDirections(PiecePosition, new List<string> { "U", "D", "L", "R" }, 8, IsWhite);
                break;
            case 'N':
                List<Vector2> legaloffsets = new List<Vector2> {new Vector2 (1,2), new Vector2(-1, 2), new Vector2(1, -2), new Vector2(-1, -2), new Vector2(2, 1), new Vector2(-2, 1), new Vector2(2, -1), new Vector2(-2, -1), };
                foreach (Vector2 offset in legaloffsets) 
                {
                    if (Grid.GetTileAtPos(PiecePosition + offset) != null)
                    {
                        if(Grid.GetTileAtPos(PiecePosition + offset).occupied)
                        {
                            if(Grid.GetTileAtPos(PiecePosition + offset).PieceOnTile.IsWhite != IsWhite)
                            {
                                AvailableMoves.Add(Grid.GetTileAtPos(PiecePosition + offset));
                            }
                        }
                        else
                        {
                            AvailableMoves.Add(Grid.GetTileAtPos(PiecePosition + offset));
                        }
                    }
                }
                break;
            case 'P':
                int pawndirection = IsWhite ?  1: -1;
                if (!Grid.PieceAtPos(PiecePosition + new Vector2(0, pawndirection)))
                {
                    AvailableMoves.Add(Grid.GetTileAtPos(PiecePosition + new Vector2( 0, pawndirection )));
                }
                if (Grid.PieceAtPos(PiecePosition + new Vector2(1, pawndirection)))
                {
                    if (Grid.GetTileAtPos(PiecePosition + new Vector2(1, pawndirection)).PieceOnTile.IsWhite != IsWhite)
                    {
                        print("lesgo2");
                        AvailableMoves.Add(Grid.GetTileAtPos(PiecePosition + new Vector2(1, pawndirection)));
                    }
                }
                else { print(PiecePosition + new Vector2(1, pawndirection)); }
                if (Grid.PieceAtPos(PiecePosition + new Vector2(-1, pawndirection)))
                {
                    if (Grid.GetTileAtPos(PiecePosition + new Vector2(-1, pawndirection)).PieceOnTile.IsWhite != IsWhite)
                    {
                        print(PiecePosition + new Vector2(-1, pawndirection));
                    }
                }
                break;
        }
        Grid.SetTilesAvailable(AvailableMoves);
    }


    public void MovePiece(Vector2 MovePosition)
    {
        if(Grid.GetTileAtPos(MovePosition) == null)
        {
            print("Tile where piece wants to move doesn't exist");
        }
        else if(Grid.GetTileAtPos(MovePosition).occupied)
        {
            Grid.GetTileAtPos(MovePosition).PieceOnTile.GetCaptured();
            Grid.GetTileAtPos(PiecePosition).UpdatePieceOnThis(null, false);
            this.transform.position = new Vector3(MovePosition.x, MovePosition.y, -1);
            Grid.GetTileAtPos(MovePosition).UpdatePieceOnThis(this, true);
            PiecePosition = MovePosition;
            Grid.SelectedPiece = null;
        }
        else
        {
            Grid.GetTileAtPos(PiecePosition).UpdatePieceOnThis(null, false);
            this.transform.position = new Vector3(MovePosition.x, MovePosition.y, -1);
            Grid.GetTileAtPos(MovePosition).UpdatePieceOnThis(this, true);
            PiecePosition = MovePosition;
            Grid.SelectedPiece = null;
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
            case 'N':
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
                print("can't recognize piece when assigning sprite");
                break;
        }
        if (IsWhite)
        {
            SpriteIndex += 6;
        }

        Renderer.sprite = PieceSpriteList[SpriteIndex];
    }
    
    public void GetCaptured()
    {
        Grid.GetTileAtPos(PiecePosition).UpdatePieceOnThis(null, false);
        Object.Destroy(gameObject);
    }
}
