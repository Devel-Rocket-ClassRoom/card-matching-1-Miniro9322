using System;

class Board
{
    private Card[,] _board;
    private Card _openCard1;
    private Card _openCard2;
    public readonly int TotalSet;
    private int _makedCard = 0;

    public Board(Difficulty difficulty, CardSkin cardSkin)
    {
        switch (difficulty)
        {
            case Difficulty.Easy:
                TotalSet = CreateBoard(2, 4, cardSkin);
                break;
            case Difficulty.Normal:
                TotalSet = CreateBoard(4, 4, cardSkin);
                break;
            case Difficulty.Hard:
                TotalSet = CreateBoard(6, 4, cardSkin);
                break;
        }
    }

    public int CreateBoard(int  length, int width, CardSkin skin)
    {
        Random rand = new Random();
        _board = new Card[length, width];
        int colorNum = 0;
        string color;

        switch (skin)
        {
            case CardSkin.Number:
                for (int i = 0; i < _board.GetLength(0); i++)
                {
                    for (int j = 0; j < _board.GetLength(1); j++)
                    {
                        _board[i, j] = new NumCard(_makedCard++);
                    }
                }
                break;
            case CardSkin.Alphabet:
                for (int i = 0; i < _board.GetLength(0); i++)
                {
                    for (int j = 0; j < _board.GetLength(1); j++)
                    {
                        if ((_makedCard) % 2 == 0)
                        {
                            colorNum = rand.Next(15);
                        }
                        color = ((Color)colorNum).ToString();
                        _board[i, j] = new AlphabetCard(_makedCard++, color);
                    }
                }
                break;
            case CardSkin.Sign:
                for (int i = 0; i < _board.GetLength(0); i++)
                {
                    for (int j = 0; j < _board.GetLength(1); j++)
                    {
                        if ((_makedCard) % 2 == 0)
                        {
                            colorNum = rand.Next(15);
                        }
                        color = ((Color)colorNum).ToString();
                        _board[i, j] = new SignCard(_makedCard++, color);
                    }
                }
                break;
        }
        

        return length * width / 2;
    }

    public bool OpenCard1(int x, int y)
    {
        if (x < 0 || x >= _board.GetLength(0) || y < 0 || y >= _board.GetLength(1))
        {
            Console.WriteLine("잘못된 값이 입력되었습니다. 다시 입력해 주세요.\n");
            return true;
        }
        else if (_board[x,y].IsHidden == false)
        {
            Console.WriteLine("이미 오픈된 카드입니다. 다시 입력해 주세요.\n");
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
        if(x < 0 || x >= _board.GetLength(0) || y < 0 || y >= _board.GetLength(1))
        {
            Console.WriteLine("잘못된 값이 입력되었습니다. 다시 입력해 주세요.\n");
            return true;
        }
        else if(_openCard1 == _board[x, y])
        {
            Console.WriteLine("이미 선택한 카드 입니다. 다시 입력해 주세요.\n");
            return true;
        }
        else if (_board[x, y].IsHidden == false)
        {
            Console.WriteLine("이미 오픈된 카드입니다. 다시 입력해 주세요.\n");
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
        Console.WriteLine($"{" " , 4}{"1열" , 3}{"2열" , 3}{"3열" , 3}{"4열" , 3}");

        for (int i = 0; i < _board.GetLength(0); i++)
        {
            Console.Write($"{$"{i + 1}행" , 2} ");

            for (int j = 0; j < _board.GetLength(1); j++)
            {
                _board[i, j].ShowCard();
            }

            Console.WriteLine("\n");
        }
    }

    public void PrintBoard()
    {
        Console.WriteLine($"{" ", 4}{"1열", 3}{"2열", 3}{"3열", 3}{"4열", 3}");

        for (int i = 0; i < _board.GetLength(0); i++)
        {
            Console.Write($"{$"{i + 1}행", 2} ");

            for (int j = 0; j < _board.GetLength(1); j++)
            {
                _board[i, j].PrintCard();
            }

            Console.WriteLine("\n");
        }
    }

    public bool CompareCard()
    {
        if(_openCard1.Text == _openCard2.Text)
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

        return _openCard1.Text == _openCard2.Text;
    }
}