using System;
using System.Threading;

class CardMatching
{
    private Board _board;
    private int _tryCount = 0;
    private int foundedSet = 0;
    private Difficulty _difficulty;
    private CardSkin _skin;
    private GameMode _mode;
    private bool _isWrong = false;
    private bool isClear;
    private int _waiting;
    private string _gameOverMessage;

    public bool Start()
    {
        _difficulty = SelectDifficulty();
        Console.WriteLine();
        _skin = SelectCardSkin();
        Console.WriteLine();
        _mode = SelectGameMode();
        Console.Clear();

        switch (_mode)
        {
            case GameMode.Classic:
                Classic();
                break;
            case GameMode.TimeAttack:
                TimeAttack();
                break;
            case GameMode.Survival:
                Survival();
                break;
        }

        if(isClear == true)
        {
            Console.WriteLine("=== 게임 클리어! ===");
            Console.WriteLine($"총 시도 횟수: {_tryCount}\n");
        }
        else
        {
            Console.WriteLine("=== 게임 오버! ===");
            Console.WriteLine(_gameOverMessage);
            Console.WriteLine($"찾은 쌍: {foundedSet}/{_board.TotalSet}\n");
        }

        Console.Write("새 게임을 하시겠습니까? (Y/N): ");

        return CheckNewGame();
    }

    private void Survival()
    {
        int missChain = 0;
        const int maxMiss = 3;

        switch (_difficulty)
        {
            case Difficulty.Easy:
                _board = new Board(_difficulty, _skin);
                _waiting = 5000;
                break;
            case Difficulty.Normal:
                _board = new Board(_difficulty, _skin);
                _waiting = 3000;
                break;
            case Difficulty.Hard:
                _board = new Board(_difficulty, _skin);
                _waiting = 2000;
                break;
        }

        _board.Shuffle();

        _board.ShowAll();

        Console.WriteLine($"잘 기억하세요! {_waiting / 1000}초 후 뒤집힙니다");
        Thread.Sleep(_waiting);
        Console.Clear();
        DateTime startTime = DateTime.Now;

        while (true)
        {
            _board.PrintBoard();
            Console.WriteLine($"\n연속 실패: {missChain}/{maxMiss} | 찾은 쌍: {foundedSet}/{_board.TotalSet}\n");

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

            if (_board.CompareCard())
            {
                Console.WriteLine("짝을 찾았습니다!\n");
                foundedSet++;
                missChain = 0;
            }
            else
            {
                Console.WriteLine("짝이 맞지 않습니다!\n");
                missChain++;
            }

            if (missChain >= maxMiss)
            {
                isClear = false;
                _gameOverMessage = "연속으로 3번 틀렸습니다.";
                break;
            }

            if (foundedSet == _board.TotalSet)
            {
                isClear = true;
                break;
            }

            Thread.Sleep(1000);
            Console.Clear();
        }
    }

    private void TimeAttack()
    {
        
        int maxTime = 0;
        int passedTime = 0;

        switch (_difficulty)
        {
            case Difficulty.Easy:
                _board = new Board(_difficulty, _skin);
                maxTime = 60;
                _waiting = 5000;
                break;
            case Difficulty.Normal:
                _board = new Board(_difficulty, _skin);
                maxTime = 90;
                _waiting = 3000;
                break;
            case Difficulty.Hard:
                _board = new Board(_difficulty, _skin);
                maxTime = 120;
                _waiting = 2000;
                break;
        }

        _board.Shuffle();

        _board.ShowAll();

        Console.WriteLine($"잘 기억하세요! {_waiting / 1000}초 후 뒤집힙니다");
        Thread.Sleep(_waiting);
        Console.Clear();
        DateTime startTime = DateTime.Now;

        while (true)
        {
            _board.PrintBoard();
            Console.WriteLine($"\n경과 시간: {passedTime}초/{maxTime}초 | 찾은 쌍: {foundedSet}/{_board.TotalSet}\n");

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

            if (_board.CompareCard())
            {
                Console.WriteLine("짝을 찾았습니다!\n");
                foundedSet++;
            }
            else
            {
                Console.WriteLine("짝이 맞지 않습니다!\n");
                _tryCount++;
            }

            passedTime = (int)(DateTime.Now - startTime).TotalSeconds;
            if (passedTime >= maxTime)
            {
                isClear = false;
                _gameOverMessage = "제한 시간을 초과했습니다.";
                break;
            }

            if (foundedSet == _board.TotalSet)
            {
                isClear = true;
                break;
            }

            Thread.Sleep(1000);
            Console.Clear();
        }
    }

    private void Classic()
    {
        int maxTry = 0;

        switch (_difficulty)
        {
            case Difficulty.Easy:
                _board = new Board(_difficulty, _skin);
                maxTry = 10;
                _waiting = 5000;
                break;
            case Difficulty.Normal:
                _board = new Board(_difficulty, _skin);
                maxTry = 20;
                _waiting = 3000;
                break;
            case Difficulty.Hard:
                _board = new Board(_difficulty, _skin);
                maxTry = 30;
                _waiting = 2000;
                break;
        }

        _board.Shuffle();

        _board.ShowAll();

        Console.WriteLine($"잘 기억하세요! {_waiting / 1000}초 후 뒤집힙니다");
        Thread.Sleep(_waiting);
        Console.Clear();

        while (true)
        {
            _board.PrintBoard();
            Console.WriteLine($"\n시도 횟수: {_tryCount}/{maxTry} | 찾은 쌍: {foundedSet}/{_board.TotalSet}\n");

            do
            {
                Console.Write($"첫 번째 카드를 선택하세요 (행 열): ");
                _isWrong = OpenCard(0);
            } while (_isWrong == true);
            Console.Clear() ;
            _board.PrintBoard();

            do
            {
                Console.Write($"두 번째 카드를 선택하세요 (행 열): ");
                _isWrong = OpenCard(1);
            } while (_isWrong == true);
            Console.Clear();
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

            if (_tryCount == maxTry)
            {
                isClear = false;
                break;
            }

            if (foundedSet == _board.TotalSet)
            {
                isClear = true;
                _gameOverMessage = "시도 횟수를 모두 사용했습니다.";
                break;
            }

            Thread.Sleep(1000);
            Console.Clear();
        }
    }

    private bool OpenCard(int num)
    {
        char[] temp = {' '};
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