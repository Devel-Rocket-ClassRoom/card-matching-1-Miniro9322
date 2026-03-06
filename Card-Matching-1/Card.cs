using System;

class Card
{
    public int Number;
    public bool IsHidden;
    public bool TempHidden;

    public Card(int num)
    {
        Number = num;
        IsHidden = true;
        TempHidden = true;
    }

    public void PrintCard()
    {
        if(TempHidden == false)
        {
            Console.Write($" [{Number}] ");
        }
        else if (IsHidden == false)
        {
            Console.Write($" {Number} ");
        }
        else
        {
            Console.Write(" ** ");
        }
    }

    public void ShowCard()
    {
        Console.Write($" {Number} ");
    }
}