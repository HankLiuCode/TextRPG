using System;
using System.Collections.Generic;
using TextRPG.Common;
using TextRPG;

class Map
{
    char[,] map;
    char temp;

    public Map(string[] rawMap)
    {
        string tempStr = "";
        foreach (string s in rawMap)
        {
            tempStr += s + "\n";
        }

        map = StringToCharArray(tempStr);
        temp = '.';
    }

    public Map(string rawMap)
    {
        map = StringToCharArray(rawMap);
        temp = '.';
    }

    public void UnBind(GameEntity gameEntity)
    {
        gameEntity.OnMove -= GameEntity_OnMove;
        gameEntity.OnDestroy -= GameEntity_OnDestroy;
    }

    public void Bind(GameEntity gameEntity)
    {
        gameEntity.OnMove += GameEntity_OnMove;
        gameEntity.OnDestroy += GameEntity_OnDestroy;
    }

    private void GameEntity_OnDestroy(object sender, OnDestroyEventArgs e)
    {
        map[e.destroyTarget.x, e.destroyTarget.y] = temp;
        UnBind((GameEntity) sender);
    }

    private void GameEntity_OnMove(object sender, OnMoveEventArgs e)
    {
        Vector2 target = Find(e.symbol);
        if (target != e.fromVector)
            throw new Exception(string.Format("Symbol doesn't match! {0} {1} {2}", sender, target, e.fromVector));
        Move(e.fromVector, e.toVector);
    }

    public Vector2 Find(char target)
    {
        for (int i = 0; i < map.GetLength(0); i++)
        {
            for (int j = 0; j < map.GetLength(1); j++)
            {
                if (map[i, j] == target)
                {
                    return new Vector2(i, j);
                }
            }
        }

        return new Vector2(-1, -1);
    }

    public Vector2[] FindAll(char target)
    {
        List<Vector2> found = new List<Vector2>();
        for (int i = 0; i < map.GetLength(0); i++)
        {
            for (int j = 0; j < map.GetLength(1); j++)
            {
                if (map[i, j] == target)
                    found.Add(new Vector2(i,j));
            }
        }
        return found.ToArray();
    }

    public void Move(Vector2 from, Vector2 to)
    {
        if (from == to || from == -Vector2.One || to == -Vector2.One)
            return;

        char tempChar = temp;
        temp = map[to.x, to.y];
        map[to.x, to.y] = map[from.x, from.y];
        map[from.x, from.y] = tempChar;
    }


    public string[] GetStateStringArray()
    {
        return CharArrayToStringArray(map);
    }
    public char[,] GetStateCharArray()
    {
        return map;
    }
    public static string[] StringToStringArray(string map)
    {
        string[] lines = map.Split("\n", StringSplitOptions.RemoveEmptyEntries);
        return lines;
    }
    public static string[] CharArrayToStringArray(char[,] charArr)
    {
        string tempStr = CharArrayToString(charArr);
        return StringToStringArray(tempStr);
    }
    public static string CharArrayToString(char[,] charArr)
    {
        string temp = "";
        for (int j = 0; j < charArr.GetLength(1); j++)
        {
            for (int i = 0; i < charArr.GetLength(0); i++)
            {
                temp += charArr[i, j];
            }
            temp += "\n";
        }
        
        return temp;
    }
    public static char[,] StringToCharArray(string map)
    {
        string[] lines = map.Split("\n", StringSplitOptions.RemoveEmptyEntries);
        int width = lines[0].Length;
        int height = lines.Length;

        char[,] cells = new char[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                cells[x, y] = lines[y][x];
            }
        }
        return cells;
    }
    
    public static void PrintMap(char[,] charArr)
    {
        string temp = "";
        for (int j = 0; j < charArr.GetLength(1); j++)
        {
            for (int i = 0; i < charArr.GetLength(0); i++)
            {
                temp += charArr[i, j];
            }
            temp += "\n";
        }

        Console.WriteLine(temp);
    }
}

