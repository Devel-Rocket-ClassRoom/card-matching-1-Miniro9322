using System;
using System.Threading;

class Survival : GameBase
{
    private int _missChain = 0;
    private const int k_MaxMiss = 3;

    public Survival(Difficulty difficulty, CardSkin skin)
    {
        switch (difficulty)
        {
            case Difficulty.Easy:
                _board = new Board(difficulty, skin);
                _waiting = 5000;
                break;
            case Difficulty.Normal:
                _board = new Board(difficulty, skin);
                _waiting = 3000;
                break;
            case Difficulty.Hard:
                _board = new Board(difficulty, skin);
                _waiting = 2000;
                break;
        }

        _board.Shuffle();

        _board.ShowAll();

        Console.WriteLine($"잘 기억하세요! {_waiting / 1000}초 후 뒤집힙니다");
        Thread.Sleep(_waiting);
        Console.Clear();
    }

    protected override void CardCompare()
    {
        if (_board.CompareCard())
        {
            Console.WriteLine("짝을 찾았습니다!\n");
            _missChain = 0;
            _foundSet++;
        }
        else
        {
            Console.WriteLine("짝이 맞지 않습니다!\n");
            _missChain++;
        }
    }

    protected override string GetStatusText()
    {
        return $"\n연속 실패: {_missChain}/{k_MaxMiss} | 찾은 쌍: {_foundSet}/{_board.TotalSet}\n";
    }

    protected override bool IsGameOver()
    {
        if (_missChain >= k_MaxMiss)
        {
            _isGameClear = false;
            _gameOverMessage = "연속으로 3번 틀렸습니다.";
            return true;
        }
        if (_foundSet == _board.TotalSet)
        {
            _isGameClear = true;
            _gameClearMessage = string.Empty;
            return true;
        }
        return false;
    }
}