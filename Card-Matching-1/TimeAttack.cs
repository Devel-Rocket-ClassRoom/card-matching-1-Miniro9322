using System;
using System.Threading;

class TimeAttack : GameBase
{
    private int _maxTime = 0;
    private int _passedTime = 0;
    private DateTime startTime;
    public TimeAttack(Difficulty difficulty, CardSkin skin)
    {
        switch (difficulty)
        {
            case Difficulty.Easy:
                _board = new Board(difficulty, skin);
                _maxTime = 60;
                _waiting = 5000;
                break;
            case Difficulty.Normal:
                _board = new Board(difficulty, skin);
                _maxTime = 90;
                _waiting = 3000;
                break;
            case Difficulty.Hard:
                _board = new Board(difficulty, skin);
                _maxTime = 120;
                _waiting = 2000;
                break;
        }


        _board.Shuffle();

        _board.ShowAll();

        Console.WriteLine($"잘 기억하세요! {_waiting / 1000}초 후 뒤집힙니다");
        Thread.Sleep(_waiting);
        Console.Clear();
        startTime = DateTime.Now;

    }

    protected override void CardCompare()
    {
        if (_board.CompareCard())
        {
            Console.WriteLine("짝을 찾았습니다!\n");
            _foundSet++;
        }
        else
        {
            Console.WriteLine("짝이 맞지 않습니다!\n");
        }
    }

    protected override string GetStatusText()
    {
        return $"\n경과 시간: {_passedTime}초/{_maxTime}초 | 찾은 쌍: {_passedTime}/{_board.TotalSet}\n";
    }

    protected override bool IsGameOver()
    {
        _passedTime = (int)(DateTime.Now - startTime).TotalSeconds;

        if (_passedTime >= _maxTime)
        {
            _isGameClear = false;
            _gameOverMessage = "제한 시간을 초과했습니다.";
            return true;
        }
        if (_foundSet == _board.TotalSet)
        {
            _gameClearMessage = $"클리어 시간: {_passedTime}";
            _isGameClear = true;
            return true;
        }
        return false;
    }
}