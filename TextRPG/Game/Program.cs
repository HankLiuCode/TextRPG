using System;
using System.Collections.Generic;
using System.IO;
using TextRPG.Common;


class Program
{
    public void Main(string[] args)
    {
        // wall : # black with brown background
        // ground: . white with dark purple background
        // player: @ yellow with darkpurple background
        // monster: m red with dark purple background

        Dictionary<char, Pixel> colorMapping = new Dictionary<char, Pixel>();
        colorMapping.Add('@', new Pixel(ConsoleColor.Black, ConsoleColor.Red));
        colorMapping.Add('#', new Pixel(ConsoleColor.Black, ConsoleColor.White));
        colorMapping.Add('.', new Pixel(ConsoleColor.Black, ConsoleColor.White));
        colorMapping.Add(',', new Pixel(ConsoleColor.DarkBlue, ConsoleColor.White));
        colorMapping.Add('\"', new Pixel(ConsoleColor.Black, ConsoleColor.Green));

        string rawMap = String.Join("",
                    "####################\n",
                    "#@...........,,,,,,#\n",
                    "#...........#,,,,,,#\n",
                    "#...........#,,,,,,#\n",
                    "#...........########\n",
                    "#......#...........#\n",
                    "#...#..............#\n",
                    "#..................#\n",
                    "####################\n"
                    );
        Map map = new Map(rawMap);
    }

}

