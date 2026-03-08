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

        _board.Reset();
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

    public Difficulty SelectDifficulty()
    {
        Console.WriteLine("난이도를 선택하세요:");
        Console.WriteLine("1. 쉬움 (2x4)");
        Console.WriteLine("2. 보통 (4x4)");
        Console.WriteLine("3. 어려움 (4x6)");
        while (true)
        {
            Console.Write("선택: ");
            int choice = 0;
            Int32.TryParse(Console.ReadLine().Trim(), out choice);

            if (Enum.IsDefined(typeof(Difficulty), choice))
            {
                return (Difficulty)choice;
            }
            else
            {
                Console.WriteLine("잘못된 값이 입력되었습니다. 다시 입력해 주시기 바랍니다.");
            }
        }
    }

    public CardSkin SelectCardSkin()
    {
        Console.WriteLine("카드 스킨을 선택하세요:");
        Console.WriteLine("1. 숫자 (기본)");
        Console.WriteLine("2. 알파벳 (컬러)");
        Console.WriteLine("3. 기호 (컬러)");
        while (true)
        {
            Console.Write("선택: ");
            int choice = 0;
            Int32.TryParse(Console.ReadLine().Trim(), out choice);

            if (Enum.IsDefined(typeof(Difficulty), choice))
            {
                return (CardSkin)choice;
            }
            else
            {
                Console.WriteLine("잘못된 값이 입력되었습니다. 다시 입력해 주시기 바랍니다.");
            }
        }
    }

    public GameMode SelectGameMode()
    {
        Console.WriteLine("게임 모드를 선택하세요:");
        Console.WriteLine("1. 클래식");
        Console.WriteLine("2. 타임어택");
        Console.WriteLine("3. 서바이벌");
        while (true)
        {
            Console.Write("선택: ");
            int choice = 0;
            Int32.TryParse(Console.ReadLine().Trim(), out choice);

            if (Enum.IsDefined(typeof(GameMode), choice))
            {
                return (GameMode)choice;
            }
            else
            {
                Console.WriteLine("잘못된 값이 입력되었습니다. 다시 입력해 주시기 바랍니다.");
            }
        }
    }

    public bool CheckNewGame()
    {
        string newGame = Console.ReadLine();

        newGame = newGame.ToUpper();

        if (newGame == "Y")
        {
            _board.Reset();
            Console.Clear();
            Console.WriteLine("=== 새 게임 ===\n");
            return true;
        }
        else
        {
            Console.WriteLine("게임을 종료합니다.");
            return false;
        }
    }
}