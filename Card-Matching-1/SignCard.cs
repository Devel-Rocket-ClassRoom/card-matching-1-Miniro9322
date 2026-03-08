class SignCard : Card
{
    private readonly string[] _texts = { "☆", "★", "○", "●", "◎", "◇", "◆", "□", "■", "△", "▲", "▽" };

    public SignCard(int makedCard, string color) : base(color: color)
    {
        base.Text = _texts[makedCard / 2];
    }
}