using System;

class CardMatching
{
    private Board _board;
    private int _tryCount = 0;
    private int foundedSet = 0;
    private Difficulty _difficulty;
    private bool _isWrong = false;
    private int _maxTry;
    private bool isClear;

    public void Start()
    {
        _difficulty = SelectDifficulty();

        switch (_difficulty)
        {
            case Difficulty.Easy:
                _board = new Board(2, 4);
                _maxTry = 10;
                break;
            case Difficulty.Normal:
                _board = new Board(4, 4);
                _maxTry = 20;
                break;
            case Difficulty.Hard:
                _board = new Board(6, 4);
                _maxTry = 30;
                break;
        }

        _board.Shuffle();

        while (true)
        {
            _board.PrintBoard();
            Console.WriteLine($"\n시도 횟수: {_tryCount}/{_maxTry} | 찾은 쌍: {foundedSet}/{_board.TotalSet}\n");

            do
            {
                Console.Write($"첫 번째 카드를 선택하세요 (행 열): ");
                _isWrong = OpenCard(0);
            } while (_isWrong == true);
            _board.PrintBoard();

            do
            {
                Console.Write($"두 번째 카드를 선택하세요 (행 열): ");
                OpenCard(1);
            } while (_isWrong == true);

            _board.PrintBoard();

            if (_board.CompareCard())
            {
                Console.WriteLine("짝을 찾았습니다!\n");
                _tryCount++;
                foundedSet++;
            }
            else
            {
                Console.WriteLine("짝이 맞지 않습니다!\n");
                _tryCount++;
            }

            if(_tryCount == _maxTry)
            {
                isClear = false;
                break;
            }

            if (foundedSet == _board.TotalSet)
            {
                isClear = true;
                break;
            }
        }

        if(isClear == true)
        {
            Console.WriteLine("=== 게임 클리어! ===");
            Console.WriteLine($"총 시도 횟수: {_tryCount}\n");
        }
        else
        {
            Console.WriteLine("=== 게임 오버! ===");
            Console.WriteLine("시도 횟수를 모두 사용했습니다.");
            Console.WriteLine($"찾은 쌍: {foundedSet}/{_board.TotalSet}\n");
        }

        Console.Write("새 게임을 하시겠습니까? (Y/N): ");

        CheckNewGame();
    }

    private bool OpenCard(int num)
    {
        string open = Console.ReadLine();
        string[] openNum = open.Split(" ");
        if(openNum.Length < 2)
        {
            Console.WriteLine("잘못된 값이 입력되었습니다. 다시 입력해주시기 바랍니다.");
            return true;
        }
        int x = Convert.ToInt32(openNum[0]);
        int y = Convert.ToInt32(openNum[1]);
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
        Console.WriteLine("2. 쉬움 (4x4)");
        Console.WriteLine("3. 쉬움 (4x6)");
        while (true)
        {
            Console.Write("선택: ");
            int choice = Convert.ToInt32(Console.ReadLine().Trim());

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

    public void CheckNewGame()
    {
        string newGame = Console.ReadLine();

        newGame = newGame.ToUpper();

        if (newGame == "Y")
        {
            Console.Clear();
            Console.WriteLine("=== 새 게임 ===\n");
            Start();
        }
        else
        {
            Console.WriteLine("게임을 종료합니다.");
        }
    }
}