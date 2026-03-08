using System;

class NumCard : Card
{
    private readonly string[] _texts = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12" };

    public NumCard(int makedCard) :base(color: Color.White.ToString())
    {
        base.Text = _texts[makedCard / 2];
    }
}