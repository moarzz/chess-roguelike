using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;

public class Grid_Manager : MonoBehaviour
{
    [SerializeField] private int width, height;
    [SerializeField] private Tile TilePrefab;
    [SerializeField] private piece PiecePrefab;
    [SerializeField] private Transform CamTransf;
    private Dictionary<Vector2, Tile> Tiles;
    private Tile HoveredTile;
    [HideInInspector] public piece SelectedPiece;
    private string boardstate = "rnbqkbnr / pppppppp / 8 / 8 / 8 / 8 / PPPPPPPP / RNBQKBNR w KQkq -0 1";

    void Start()
    {
        GenerateGrid();
        GeneratePieces();
        /*piece x = Instantiate(PiecePrefab, new Vector3(2, 4), Quaternion.identity);
        x.Init(this, new Vector3(2, 4), 'Q', true);
        GetTileAtPos(new Vector3(2, 4)).PieceOnTile = x;
        GetTileAtPos(new Vector3(2, 4)).occupied = true;
        piece y = Instantiate(PiecePrefab, new Vector3(2, 7), Quaternion.identity);
        y.Init(this, new Vector3(2, 7), 'P', false);
        GetTileAtPos(new Vector3(2, 7)).PieceOnTile = y;
        GetTileAtPos(new Vector3(2, 7)).occupied = true;*/
    }

    


    public void UpdateHoveredTile(Tile updatedtile, bool hoverstart) //if hoverstart is false, it means the tile is no longer being hovered
    {
        if (hoverstart)
        {
            HoveredTile = updatedtile;
        }
        else if (!hoverstart && HoveredTile == updatedtile)
        {
            HoveredTile = null;
        }
    }

    void GenerateGrid() {
        Tiles = new Dictionary<Vector2, Tile>();

        for (int x = 1; x < width + 1; x++) {
            for ( int y = 1; y < height + 1; y++) {
                var spawnedTile = Instantiate(TilePrefab, new Vector3(x, y), Quaternion.identity);
                spawnedTile.name = $"Tile {x} {y}";

                var IsOffset = (x % 2 == 0 && y % 2 != 0) || (x % 2 != 0 && y % 2 == 0);
                spawnedTile.Init(IsOffset, this, new Vector2(x,y));

                Tiles[new Vector2 (x, y)] = spawnedTile;
            }
        }
        CamTransf.transform.position = new Vector3((float)width / 2 + 0.5f, ((float)height / 2) + 0.5f, -10);
    }
    
    void GeneratePieces()
    {
        SetBoardState(boardstate);
    }

    void SetBoardState(string newstate)
    {
        Vector2 currentlocation = new Vector2(1,1);
        bool iswhite;
        foreach(char c in newstate) /* c est le char dans le string qui est pris. le string est une notation de board de chess en string*/
        {
            char char_ = c;
            iswhite = false;
            if (c - 'a' >= 0 && c - 'z' <= 0) /* Basically, si c est une lettre majuscule*/
            {
                iswhite = true;
                char_ = (char)(c - 32);
            }

            if (char_ - 'A' >= 0 && char_ <= 'z') 
            { 
                AddPieceToBoard(char_, currentlocation, iswhite);
                currentlocation += new Vector2(1, 0);
            }
            
            else if(c == '/')
            {
                  if (currentlocation.y < 8)
                  {
                            currentlocation = new Vector2(1, currentlocation.y + 1);
                  }
                  else { print("wack skip une ligne apres 8"); }
            }
            else if((c-'0') < 9 && (c-'0' >= 0))
            {
                    currentlocation = new Vector2(currentlocation.x + c-'0', currentlocation.y); /*c - '0' est égal à la valeur de c en int si il est entre '0' et '0' + 9 qui est égal à '9'*/
                    if (GetTileAtPos(currentlocation) == null)
                    {
                        print("the location being checked in set board state is not a possible location on a chess board");
                    }
            }

            else { print("character not recognized in state string"); }
        }
    }
    void AddPieceToBoard(char pieceid, Vector2 location, bool iswhite)
    {
        piece piece;
        if (GetTileAtPos(location) != null) 
        {
            piece = Instantiate(PiecePrefab, location, Quaternion.identity);
            piece.Init(this, location, pieceid, iswhite);
            GetTileAtPos(location).PieceOnTile = piece;
            GetTileAtPos(location).occupied = true;
            print("Added " + pieceid + " At " + location);
        }
        else  {  print("can't add piece at " + location);  }
    }


    public Tile GetTileAtPos(Vector2 pos)
    {
        if (Tiles.TryGetValue(pos, out Tile tile))
        {
            return tile;
        }
        return null;
    }
    
    public List<Tile> GetAllTilesInLine(Vector2 source, string direction, int linelength) /* The source is where the line begins, usually where the piece is.   
The direction is the lines orientation from the source. It is displayed by 1 or 2 letters.  U and D for up and down, L and R for right and left*/
    {
        int currentlength = 0;
        Vector2 currentlineend = source;
        List<Tile> tilesinline = new List<Tile>();
        Vector2 offset = new Vector2(0,0);
        bool LineOver = false;

        foreach (char a in direction)
        {
            if      (a == 'U')          {   offset.y = 1;}
            else if (a == 'D')          {   offset.y = -1;}
            else if (a == 'L')          {   offset.x = -1;}
            else if (a == 'R')          {   offset.x = 1; }
            else { return null; }
        }
        while (!LineOver)
        {
            if(GetTileAtPos(currentlineend + offset)  != null && !LineOver && currentlength < linelength) 
            {
                currentlength++;
                currentlineend = currentlineend + offset;
                tilesinline.Add(GetTileAtPos(currentlineend));

                if (GetTileAtPos(currentlineend).occupied)
                {
                    LineOver = true;
                }
            }
            else
            {
                LineOver = true;
            }
            
        }
        return tilesinline;
    }

    public void SetTilesAvailable(List<Tile> availabletiles)
    {
        foreach (Tile tile in availabletiles)
        {
            tile.UpdateCanMoveOnThis(true);
        }
    }

    public List<Tile> GetLinesInDirections(Vector2 source, List<string> directions, int linelength)
    {
        List<Tile> tiles = new List<Tile>();

        foreach(string direction in directions)
        {
            tiles.AddRange(GetAllTilesInLine(source, direction, linelength));
        }
        return tiles;
    }

    public bool PieceAtPos(Vector2 pos)
    {
        if (GetTileAtPos(pos).occupied)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}

