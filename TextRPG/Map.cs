using System;
using System.Collections.Generic;
using TextRPG;


public class OutOfMapBoundaryException : Exception
{
}

public class Map
{
    char[,] map;
    public Map(string rawMap)
    {
        map = StringToMap(rawMap);
    }
    public Map(string[] lines)
    {
        map = StringToMap(lines);
    }

    public void RoundSwitch(ref char a, Vector2 b, Vector2 c, Vector2 d)
    {
        char temp = a;
        a = GetChar(b);
        SetChar(b, GetChar(c));
        SetChar(c, GetChar(d));
        SetChar(c, temp);
    }
    public void RoundSwitch(ref char a, Vector2 b, Vector2 c)
    {
        char temp = a;
        a = GetChar(c);
        SetChar(c, GetChar(b));
        SetChar(b, temp);
    }
    public void Switch(Vector2 a, Vector2 b)
    {
        char tempA = GetChar(a);
        char tempB = GetChar(b);
        SetChar(a, tempB);
        SetChar(b, tempA);
    }
    public void SetChar(Vector2 position, char ch)
    {
        if (position.x < 0 || position.x >= map.GetLength(0) || position.y < 0 || position.y >= map.GetLength(1))
            throw new OutOfMapBoundaryException();

        map[position.x, position.y] = ch;
    }
    public char GetChar(Vector2 position)
    {
        if (position.x < 0 || position.x >= map.GetLength(0) || position.y < 0 || position.y >= map.GetLength(1))
            throw new OutOfMapBoundaryException();

        return map[position.x, position.y];
    }
    public Vector2 FindCharPosition(char target)
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
    public Vector2[] FindCharPositions(char target)
    {
        List<Vector2> found = new List<Vector2>();
        for (int i = 0; i < map.GetLength(0); i++)
        {
            for (int j = 0; j < map.GetLength(1); j++)
            {
                if (map[i, j] == target)
                {
                    found.Add(new Vector2(i, j));
                }
            }
        }
        return found.ToArray();
    }
    public char[,] GetState()
    {
        return map;
    }
    public string[] ToStringArray(int spacing = 0)
    {
        int width = map.GetLength(0);
        int height = map.GetLength(1);
        string[] mapStringArray = new string[height];

        for(int j=0; j < height; j++)
        {
            string line = "";
            for(int i=0; i < width; i++)
            {
                line += map[i, j] + new string(' ', spacing);
            }
            mapStringArray[j] = line;
        }

        return mapStringArray;
    }

    public static char[,] StringToMap(string rawMap)
    {
        string[] lines = rawMap.Split("\r\n");
        return StringToMap(lines);
    }
    public static char[,] StringToMap(string[] lines)
    {
        // check if every line has the same length
        bool isValid = true;
        int maxLengthX = lines[0].Length;
        for (int i = 0; i < lines.Length; i++)
        {
            string line = lines[i];
            if (maxLengthX < line.Length)
            {
                isValid = false;
                maxLengthX = line.Length;
            }
            else if (maxLengthX > line.Length)
            {
                isValid = false;
            }
        }


        if (isValid)
        {
            char[,] map = new char[maxLengthX, lines.Length];
            for (int j = 0; j < lines.Length; j++)
            {
                for (int i = 0; i < maxLengthX; i++)
                {
                    map[i, j] = lines[j][i];
                }
            }
            return map;
        }
        else
        {
            char[,] map = new char[maxLengthX, lines.Length];
            for (int j = 0; j < lines.Length; j++)
            {
                for (int i = 0; i < maxLengthX; i++)
                {
                    if (i < lines[j].Length)
                    {
                        map[i, j] = lines[j][i];
                    }
                    else
                    {
                        map[i, j] = ' ';
                    }
                }
            }
            return map;
        }
    }
    public override string ToString()
    {
        int width = map.GetLength(0);
        int height = map.GetLength(1);
        
        string mapString = "";

        for(int j=0; j < height; j++)
        {
            for (int i = 0; i < width; i++) 
            {
                mapString += map[i, j];
            }
            mapString += "\n";
        }
        return mapString;
    }
}

