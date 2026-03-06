using System;

Console.WriteLine("=== 카드 짝 맞추기 게임 ===");

CardMatching game = new CardMatching();

game.Start();

enum Difficulty
{
    Easy,
    Normal,
    Hard
}