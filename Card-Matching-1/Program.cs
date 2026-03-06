using System;

bool isNewGame = false;

Console.WriteLine("=== 카드 짝 맞추기 게임 ===");

CardMatching game = new CardMatching();

do
{
    isNewGame = game.Start();
} while (isNewGame == true);