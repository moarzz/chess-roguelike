using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private Color BaseColor, OffsetColor;
    [SerializeField] private SpriteRenderer Renderer;
    [SerializeField] private GameObject IsHovered;
    [SerializeField] private GameObject IsMovable;
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
        IsHovered.SetActive(true);
        Grid.UpdateHoveredTile(this, true);
    }


    void OnMouseExit()
    {
        IsHovered.SetActive(false);
        Grid.UpdateHoveredTile(this, false);
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

    public Tile GetTileAtOffset(Vector2 offset)
    {
        return Grid.GetTileAtPos(new Vector2(GridPos.x + offset.x, GridPos.y + offset.y));
    }
}
