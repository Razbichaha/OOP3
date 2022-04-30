using System;
using System.Collections.Generic;

namespace OOP3
{
    //    Реализовать базу данных игроков и методы для работы с ней.
    //У игрока может быть уникальный номер, ник, уровень, флаг – забанен ли он(флаг - bool).
    //Реализовать возможность добавления игрока, бана игрока по уникальный номеру,
    //разбана игрока по уникальный номеру и удаление игрока.
    //Создание самой БД не требуется, задание выполняется инструментами,
    //которые вы уже изучили в рамках курса.Но нужен класс, который содержит игроков и её можно назвать "База данных".

    class Program
    {
        static void Main(string[] args)
        {
            Menu menu = new Menu();

            menu.OutputHeader();

            bool isContinueCycle = true;

            while (isContinueCycle)
            {
                string userInput = Console.ReadLine();

                switch (userInput)
                {
                    case "д":///////

                        menu.AddPlayer();

                        break;
                    case "удалить":



                        break;
                    case "бан":



                        break;
                    case "разбан":



                        break;
                    case "показать":



                        break;
                    default:

                        menu.OutputAWarning();

                        break;
                }
            }
        }
    }

    class Menu
    {
        public void OutputHeader()
        {
            Console.WriteLine("Присутствуют следующие команды");
            Console.WriteLine("Добавить игрока - добавить");
            Console.WriteLine("Удалить игрока - удалить");
            Console.WriteLine("Забанить игрока - бан");
            Console.WriteLine("разабанить игрока - разбан");
            Console.WriteLine("Список всех игроков - показать");
        }

        public void AddPlayer()
        {
            BdPlayers bdPlayers = new BdPlayers();

            Console.Write("Введите ник игнрока - ");
            string name = Console.ReadLine();

            bool isContinueCykle = true;

            while (isContinueCykle)
            {
                if (bdPlayers.SearhName(name) == false)
                {
                    int startLevelPlaer = 1;
                    int level = startLevelPlaer;
                    int startMoneyPlaer = 50;
                    int money = startMoneyPlaer;
                    bool banned = false;

                    bdPlayers.Add(name, level, money, banned);

                    isContinueCykle = false;
                }
                else
                {
                    Console.WriteLine("Имя уже занято выберите другое - ");
                }

            }

        }

        public void OutputAWarning()
        {
            Console.WriteLine("Введена не верная команда");
        }

        public void OutputCardsPlayer(Queue<string> cardsPlaer)
        {
            Console.WriteLine("\nВы выбрали карты");
            foreach (string card in cardsPlaer)
            {
                Console.WriteLine(card);
            }
        }

        public void OutputCardsPlayer(int sumCards)
        {
            Console.WriteLine("Ваши очки = " + sumCards);
        }
    }

    class BdPlayers
    {
       private Dictionary<int, Player> _playerBd = new Dictionary<int, Player>();

        public bool SearhName(string name)
        {
            bool thereIsName = false;

            for (int i = 0; i < _playerBd.Count; i++)
            {
                if (name == _playerBd[i].NamePlayer())
                {
                    thereIsName = true;
                }
            }
            return thereIsName;
        }
        
        public void Add(string name, int level, int money, bool banned)
        {
            Player player = new Player(name, level, money, banned);

            int count = _playerBd.Count;

            _playerBd.Add(count + 1, player);
        }



    }

    class Player
    {
        private string _name;
        private int _level;
        private int _money;
        private bool _banned;

        public Player(string name, int level, int money, bool banned)
        {
            _name = name;
            _level = level;
            _money = money;
            _banned = banned;
        }

        public string NamePlayer()
        {
            return _name;
        }
    }
}
