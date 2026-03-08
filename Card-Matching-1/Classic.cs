using System;
using System.Threading;

class Classic : GameBase
{
    private int _tryCount = 0;
    private int _maxTry;


    public Classic(Difficulty difficulty, CardSkin skin)
    {
        switch (difficulty)
        {
            case Difficulty.Easy:
                _board = new Board(difficulty, skin);
                _maxTry = 10;
                _waiting = 5000;
                break;
            case Difficulty.Normal:
                _board = new Board(difficulty, skin);
                _maxTry = 20;
                _waiting = 3000;
                break;
            case Difficulty.Hard:
                _board = new Board(difficulty, skin);
                _maxTry = 30;
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
            _tryCount++;
            _foundSet++;
        }
        else
        {
            Console.WriteLine("짝이 맞지 않습니다!\n");
            _tryCount++;
        }
    }

    protected override string GetStatusText()
    {
        return $"\n시도 횟수: {_tryCount}/{_maxTry} | 찾은 쌍: {_foundSet}/{_board.TotalSet}\n";
    }

    protected override bool IsGameOver()
    {
        if (_tryCount == _maxTry)
        {
            _isGameClear = false;
            _gameOverMessage = "시도 횟수를 모두 사용했습니다.";
            return true;
        }
        if(_foundSet == _board.TotalSet)
        {
            _isGameClear = true;
            _gameClearMessage = $"총 시도 횟수: {_tryCount}";
            return true;
        }
        return false;
    }
}