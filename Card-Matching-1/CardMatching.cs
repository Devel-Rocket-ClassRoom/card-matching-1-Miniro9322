using System;
using System.Threading;

class CardMatching
{
    private Difficulty _difficulty;
    private CardSkin _skin;
    private GameMode _mode;


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
                GameBase classic = new Classic(_difficulty, _skin);
                classic.Play();
                break;
            case GameMode.TimeAttack:
                GameBase timeAttack = new TimeAttack(_difficulty, _skin);
                timeAttack.Play();
                break;
            case GameMode.Survival:
                GameBase survival = new Survival(_difficulty, _skin);
                survival.Play();
                break;
        }

        Console.Write("새 게임을 하시겠습니까? (Y/N): ");

        return CheckNewGame();
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