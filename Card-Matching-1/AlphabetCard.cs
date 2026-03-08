class AlphabetCard : Card
{
    private readonly string[] _texts = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L" };

    public AlphabetCard(int makedCard, string color) : base(color: color)
    {
        base.Text = _texts[makedCard / 2];
    }
}