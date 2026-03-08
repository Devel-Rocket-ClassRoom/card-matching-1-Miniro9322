using System;
using System.Drawing;

class Card
{
    public string Text;
    public bool IsHidden;
    public bool TempHidden;
    private string _color;

    public Card(string color)
    {
        _color = color;
        IsHidden = true;
        TempHidden = true;
    }
    
    public void PrintCard()
    {
        if(TempHidden == false)
        {
            Console.ForegroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), _color);
            Console.Write($"{$"[{Text}]", 4}");
            Console.ResetColor();
        }
        else if (IsHidden == false)
        {
            Console.ForegroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), _color);
            Console.Write($"{$"{Text}", 4}");
            Console.ResetColor();
        }
        else
        {
            Console.Write($"{"**", 4}");
        }
    }

    public void ShowCard()
    {
        Console.ForegroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), _color);
        Console.Write($"{Text , 4}");
        Console.ResetColor();
    }
}