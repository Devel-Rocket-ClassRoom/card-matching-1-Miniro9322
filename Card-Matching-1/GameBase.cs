using System;
using System.Threading;

abstract class GameBase
{
    protected Board _board;
    protected bool _isWrong;
    protected bool _isGameOver;
    protected string _gameOverMessage;
    protected string _gameClearMessage;
    protected int _foundSet = 0;
    protected int _waiting;
    protected bool _isGameClear;

    protected abstract bool IsGameOver();
    protected abstract string GetStatusText();
    protected abstract void CardCompare();

    public void Play()
    {
        while (IsGameOver() == false)
        {
            _board.PrintBoard();
            Console.WriteLine(GetStatusText());

            do
            {
                Console.Write($"첫 번째 카드를 선택하세요 (행 열): ");
                _isWrong = OpenCard(0);
            } while (_isWrong == true);
            Console.Clear();
            _board.PrintBoard();

            do
            {
                Console.Write($"두 번째 카드를 선택하세요 (행 열): ");
                _isWrong = OpenCard(1);
            } while (_isWrong == true);
            Console.Clear();
            _board.PrintBoard();

            CardCompare();
            
            Thread.Sleep(1000);
            Console.Clear();
        }

        if (_isGameClear == false)
        {
            Console.WriteLine("=== 게임 오버! ===");
            _board.PrintBoard();
            Console.WriteLine(_gameOverMessage);
            Console.WriteLine($"찾은 쌍: {_foundSet}/{_board.TotalSet}\n");
        }
        else
        {
            Console.WriteLine("=== 게임 클리어! ===");
            _board.PrintBoard();
            Console.WriteLine(_gameClearMessage);
            Console.WriteLine();
        }
    }

    private bool OpenCard(int num)
    {
        char[] temp = { ' ' };
        string open = Console.ReadLine();
        string[] openNum = open.Trim().Split(temp, StringSplitOptions.RemoveEmptyEntries);
        if (openNum.Length < 2)
        {
            Console.WriteLine("잘못된 값이 입력되었습니다. 다시 입력해주시기 바랍니다.");
            Console.WriteLine();
            return true;
        }
        int x = Convert.ToInt32(openNum[0].Trim()) - 1;
        int y = Convert.ToInt32(openNum[1].Trim()) - 1;
        switch (num)
        {
            case 0:
                return _board.OpenCard1(x, y);
            case 1:
                return _board.OpenCard2(x, y);
            default:
                return false;
        }
    }
}