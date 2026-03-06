using System;

class Board
{
    private Card[,] _board;
    private Card _openCard1;
    private Card _openCard2;
    public readonly int TotalSet;

    public Board(int length, int width)
    {
        int num = 0;
        int count = 0;
        _board = new Card[length, width];
        TotalSet = length * width / 2;
        for(int i = 0; i < _board.GetLength(0); i++)
        {
            for(int j = 0; j < _board.GetLength(1); j++)
            {
                if((count++) % 2 == 0)
                {
                    num++;
                }
                _board[i, j] = new Card(num);
            }
        }
    }

    public bool OpenCard1(int x, int y)
    {
        if (x < 0 || x > _board.GetLength(0) || y < 0 || y > _board.GetLength(1))
        {
            Console.WriteLine("잘못된 값이 입력되었습니다. 다시 입력해 주세요.");
            return true;
        }
        else
        {
            _openCard1 = _board[x, y];
            _openCard1.TempHidden = false;
            return false;
        }   
    }

    public bool OpenCard2(int x, int y)
    {
        if(_board[x, y] == null)
        {
            Console.WriteLine("잘못된 값이 입력되었습니다. 다시 입력해 주세요.");
            return true;
        }
        else
        {
            _openCard2 = _board[x, y];
            _openCard2.TempHidden = false;
            return false;
        }
    }

    public void Shuffle()
    {
        Console.WriteLine("카드를 섞는 중...\n");

        Random random = new Random();

        for (int i = 0; i < _board.GetLength(0); i++)
        {
            for (int j = 0; j < _board.GetLength(1); j++)
            {
                int randomwid = random.Next(_board.GetLength(0));
                int randomhei = random.Next(_board.GetLength(1));

                Card temp = _board[i, j];
                _board[i, j] = _board[randomwid, randomhei];
                _board[randomwid, randomhei] = temp;
            }

        }
    }

    public void ShowAll()
    {
        Console.WriteLine("    1열 2열 3열 4열");

        for (int i = 0; i < _board.GetLength(0); i++)
        {
            Console.Write($"{i + 1}행 ");

            for (int j = 0; j < _board.GetLength(1); j++)
            {
                _board[i, j].ShowCard();
            }

            Console.WriteLine("\n");
        }
    }

    public void PrintBoard()
    {
        Console.WriteLine("    1열 2열 3열 4열");
        
        for (int i = 0; i < _board.GetLength(0); i++)
        {
            Console.Write($"{i + 1}행 ");

            for (int j = 0; j < _board.GetLength(1); j++)
            {
                _board[i, j].PrintCard();
            }

            Console.WriteLine("\n");
        }
    }

    public bool CompareCard()
    {
        if(_openCard1.Number == _openCard2.Number)
        {
            _openCard1.TempHidden = true;
            _openCard2.TempHidden = true;
            _openCard1.IsHidden = false;
            _openCard2.IsHidden = false;
        }
        else
        {
            _openCard1.TempHidden = true;
            _openCard2.TempHidden = true;

        }

        return _openCard1.Number == _openCard2.Number;
    }
}