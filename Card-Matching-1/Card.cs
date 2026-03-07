using System;

class Card
{
    public string Text;
    public bool IsHidden;
    public bool TempHidden;
    public static int CardCount = 0;
    public static int Num;
    public static int ColorNum;
    public string _color;

    public Card(int num, string color)
    {
        if (num < 'A')
        {
            Text = num.ToString();
            _color = "White";
        }
        else
        {
            Text = ((char)num).ToString();
            _color = color;
        }
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