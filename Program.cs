using System;
using System.Collections.Generic;

namespace OOP3
{
    class Program
    {
        static void Main(string[] args)
        {
            DatabaseManagement databaseManagement = new DatabaseManagement();

            databaseManagement.OutputHeader();

            bool isContinueCycle = true;

            while (isContinueCycle)
            {
                string userInput = Console.ReadLine();

                switch (userInput)
                {
                    case "д":

                        databaseManagement.AddPlayer();

                        break;
                    case "у":

                        databaseManagement.DeletePlayer();

                        break;
                    case "б":

                        databaseManagement.BanPlaer();

                        break;
                    case "рб":

                        databaseManagement.UnBanPlaer();

                        break;
                    case "п":

                        databaseManagement.ShowBasePlayer();

                        break;
                    default:

                        databaseManagement.OutputWarning();

                        break;
                }
            }
        }
    }

    class DatabaseManagement
    {
        private DataBase _dataBase = new DataBase();

        private int _startLevelPlaer = 1;
        private int _startMoneyPlaer = 50;
        private bool _startbanned = false;

        public void UnBanPlaer()
        {
            Console.WriteLine("Введите номер разблокированного игрока ");

            string keyString = Console.ReadLine();

            if (int.TryParse(keyString, out int key))
            {
                if (_dataBase.UnBannedPlayer(key) == false)
                {
                    Console.WriteLine("Введенный номер игрока отсутствует");
                }
            }
            else
            {
                Console.WriteLine("Введенный номер не является числом");
            }

            ShowBasePlayer();
        }

        public void BanPlaer()
        {
            Console.WriteLine("Введите номер заблокированного игрока ");

            string keyString = Console.ReadLine();

            if (int.TryParse(keyString, out int key))
            {
                if (_dataBase.IsBannedPlayer(key) == false)
                {
                    Console.WriteLine("Введенный номер игрока отсутствует");
                }
            }
            else
            {
                Console.WriteLine("Введенный номер не является числом");
            }

            ShowBasePlayer();
        }

        public void ShowBasePlayer()
        {
            Console.Clear();
            OutputHeader();

            Console.WriteLine();
            int length = _dataBase.GetKeyList();

            for (int i = 0; i < length; i++)
            {
                Player player = _dataBase.GetPlayer(i);
                Console.WriteLine("Номер - " + i + " | " + player.Show());
            }
        }

        public void DeletePlayer()
        {
            Console.WriteLine("Введите номер удаляемого игрока");

            string keyString = Console.ReadLine();

            if (int.TryParse(keyString, out int key))
            {
                _dataBase.DeletePlayer(key);
            }
            else
            {
                Console.WriteLine("Введенный номер не является числом");
            }

            ShowBasePlayer();
        }

        public void OutputHeader()
        {
            Console.WriteLine("Присутствуют следующие команды");
            Console.WriteLine("Добавить игрока - д");
            Console.WriteLine("Удалить игрока - у");
            Console.WriteLine("Забанить игрока - б");
            Console.WriteLine("разабанить игрока - рб");
            Console.WriteLine("Список всех игроков - п");
        }

        public void AddPlayer()
        {
            Console.Write("Введите ник игнрока - ");
            string namePlayer = Console.ReadLine();

            if (_dataBase.SearhName(namePlayer) == false)
            {
                int level = _startLevelPlaer;
                int money = _startMoneyPlaer;
                bool banned = _startbanned;

                _dataBase.AddPlayer(namePlayer, level, money, banned);
            }
            else
            {
                Console.WriteLine("Имя уже занято выберите другое - ");

            }
            ShowBasePlayer();
        }

        public void OutputWarning()
        {
            Console.WriteLine("Введена не верная команда");
        }
    }

    class DataBase
    {
        private List<Player> _players = new List<Player>();

        public bool UnBannedPlayer(int key)
        {
            bool isValidKey = _players.Count >= key;

            if (isValidKey)
            {
                Player player = GetPlayer(key);
                player.UnBanned();
            }

            return isValidKey;
        }

        public bool IsBannedPlayer(int key)
        {
            bool isValidKey = _players.Count >= key;

            if (isValidKey)
            {
                Player player = GetPlayer(key);

                player.Banned();
            }

            return isValidKey;
        }

        public int GetKeyList()
        {
            return _players.Count;
        }

        public bool SearhName(string name)
        {
            bool thereIsName = false;
            if (_players.Count != 0)
            {
                for (int i = 0; i < _players.Count; i++)
                {
                    if (_players[i].Name == name)
                    {
                        thereIsName = true;
                        break;
                    }
                }
            }

            return thereIsName;
        }

        public void DeletePlayer(int key)
        {
            if (_players.Count >= key)
            {
                _players.RemoveAt(key);
            }
        }

        public Player GetPlayer(int key)
        {
            return _players[key];
        }

        public void AddPlayer(string name, int level, int money, bool banned)
        {
            Player player = new Player(name, level, money, banned);

            _players.Add(player);
        }
    }

    class Player
    {
        private int _level;
        private int _money;
        private bool _banned;

        public string Name { get; private set; }

        public Player(string name, int level, int money, bool banned)
        {
            Name = name;
            _level = level;
            _money = money;
            _banned = banned;
        }

        public void UnBanned()
        {
            _banned = false;
        }

        public void Banned()
        {
            _banned = true;
        }

        public string Show()
        {
            string ban = _banned == true ? "Забанен" : "Не забанен";
            string plaer = "Имя - " + Name + "  " + "Уровень - " + _level + "  " + "Золота - " + _money + "  " + "Статус - " + ban;

            return plaer;
        }
    }
}
