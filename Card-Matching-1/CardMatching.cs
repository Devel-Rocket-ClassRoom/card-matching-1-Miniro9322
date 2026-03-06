using System;

class CardMatching
{
    private Board _board;
    private int _tryCount = 0;
    private int foundedSet = 0;
    private Difficulty _difficulty;

    public void Start()
    {
        _difficulty = SelectDifficulty();

        switch (_difficulty)
        {
            case Difficulty.Easy:
                _board = new Board(2, 4);
                break;
            case Difficulty.Normal:
                _board = new Board(4, 4);
                break;
            case Difficulty.Hard:
                _board = new Board(6, 4);
                break;
        }

        _board.Shuffle();

        while (true)
        {
            _board.PrintBoard();
            Console.WriteLine($"\n시도 횟수: {_tryCount} | 찾은 쌍: {foundedSet}/{_board.TotalSet}\n");

            Console.Write($"첫 번째 카드를 선택하세요 (행 열): ");
            string open = Console.ReadLine();
            string[] openNum = open.Split(" ");
            int x = Convert.ToInt32(openNum[0]);
            int y = Convert.ToInt32(openNum[1]);
            _board.OpenCard1(x, y);
            _board.PrintBoard();

            Console.Write($"두 번째 카드를 선택하세요 (행 열): ");
            string open2 = Console.ReadLine();
            string[] openNum2 = open2.Split(" ");
            int x2 = Convert.ToInt32(openNum2[0]);
            int y2 = Convert.ToInt32(openNum2[1]);
            _board.OpenCard2(x2, y2);
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

            if (foundedSet == _board.TotalSet)
            {
                break;
            }
        }
        Console.WriteLine("=== 게임 클리어! ===");
        Console.WriteLine($"총 시도 횟수: {_tryCount}");

        Console.Write("새 게임을 하시겠습니까? (Y/N): ");

        CheckNewGame();
    }

    public Difficulty SelectDifficulty()
    {
        Console.WriteLine("난이도를 선택하세요:");
        Console.WriteLine("1. 쉬움 (2x4)");
        Console.WriteLine("2. 쉬움 (4x4)");
        Console.WriteLine("3. 쉬움 (4x6)");
        Console.Write("선택: ");
        int choice = Convert.ToInt32(Console.ReadLine());

        return (Difficulty)choice;
    }

    public void CheckNewGame()
    {
        string newGame = Console.ReadLine();

        newGame = newGame.ToUpper();

        if (newGame == "Y")
        {
            Console.Clear();
            Console.WriteLine("=== 새 게임 ===");
            Start();
        }
        else
        {
            Console.WriteLine("게임을 종료합니다.");
        }
    }
}