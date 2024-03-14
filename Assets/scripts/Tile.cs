using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private Color BaseColor, OffsetColor;
    [SerializeField] private SpriteRenderer Renderer;
    [SerializeField] private GameObject IsHovered;
    [SerializeField] private GameObject IsMovable;
    private bool hovered = false;
    public bool occupied = false;
    public bool CanMoveHere = false;
    public piece PieceOnTile;
    private Grid_Manager Grid;
    public Vector2 GridPos;
    


    public void Init(bool IsOffset, Grid_Manager OwningGRid, Vector2 gridpos)
    {
        Renderer.color = IsOffset ? OffsetColor : BaseColor;
        Grid = OwningGRid;
        GridPos = gridpos;
    }

    void OnMouseEnter()
    {
        hovered = true;
        IsHovered.SetActive(true);
        Grid.UpdateHoveredTile(this, true);
    }
    void OnMouseExit()
    {
        hovered = false;
        IsHovered.SetActive(false);
        Grid.UpdateHoveredTile(this, false);
    }

    void OnMouseDown()
    {        
        if (hovered)
        {
            if (CanMoveHere)
            {
                foreach (Tile tile in Grid.SelectedPiece.AvailableMoves)
                {
                    tile.UpdateCanMoveOnThis(false);
                }
                Grid.SelectedPiece.AvailableMoves = null;
                Grid.SelectedPiece.MovePiece(GridPos);
            }
            else
            {
                if (Grid.SelectedPiece == null)
                {
                    if (occupied)
                    {
                        Grid.SelectedPiece = PieceOnTile;
                        Grid.SelectedPiece.GetAvailableMoves();
                    }
                }
            }   
        }
    }

    public void UpdateCanMoveOnThis(bool CanMove)
    {
        if (CanMove)
        {
            CanMoveHere = true;
            IsMovable.SetActive(true);
        }
        else
        {
            CanMoveHere = false;
            IsMovable.SetActive(false);
        }
    }

    public void UpdatePieceOnThis(piece piece, bool pieceenterstile)
    {
        if (pieceenterstile)
        {
            PieceOnTile = piece;
            occupied = true;
        }
        else
        {
            PieceOnTile = null;
            occupied = false;
        }
    }
}
