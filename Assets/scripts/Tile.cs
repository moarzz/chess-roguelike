using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private Color BaseColor, OffsetColor;
    [SerializeField] private SpriteRenderer Renderer;
    [SerializeField] private GameObject IsHovered;
    [SerializeField] private GameObject IsMovable;
    private bool occupied = false;
    private piece PieceOnTile;


    public void Init(bool IsOffset)
    {
        Renderer.color = IsOffset ? OffsetColor : BaseColor;
    }

    void OnMouseEnter()
    {
        IsHovered.SetActive(true);
    }


    void OnMouseExit()
    {
        IsHovered.SetActive(false);
    }


    public piece GetPieceOnThis()
    {
        return PieceOnTile;
    }
    public void SetPieceOnThis(piece piece)
    {
        PieceOnTile = piece;
    }
    public bool IsOccupied()
    {
        return occupied;
    }

}
