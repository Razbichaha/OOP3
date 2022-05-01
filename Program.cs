﻿using System;
using System.Collections.Generic;

namespace OOP3
{
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
                    case "д":

                        menu.AddPlayer();

                        break;
                    case "у":

                        menu.DeletePlayer();

                        break;
                    case "б":

                        menu.BanPlaer();

                        break;
                    case "рб":

                        menu.UnBanPlaer();

                        break;
                    case "п":

                        menu.ShowBasePlayer();

                        break;
                    default:

                        menu.OutputWarning();

                        break;
                }
            }
        }
    }

    class Menu
    {
        private DataBase _dataBase = new DataBase();

        private int _startLevelPlaer = 1;
        private int _startMoneyPlaer = 50;
        private bool _startbanned = false;

        public void UnBanPlaer()
        {
            Console.WriteLine("Введите номер разблокированного игрока ");

            string keyString = Console.ReadLine();
            int key;

            if (int.TryParse(keyString, out key))
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
            int key;

            if (int.TryParse(keyString, out key))
            {
                if (_dataBase.BannedPlayer(key) == false)
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

            List<int> keyList = new List<int>(_dataBase.KeyList());

            Console.WriteLine();

            for (int i = 0; i < keyList.Count; i++)
            {
                Player player = _dataBase.GetPlayer(keyList[i]);
                Console.WriteLine("Номер - " + i + " | " + player.Show());
            }
        }

        public void DeletePlayer()
        {
            Console.WriteLine("Введите номер удаляемого игрока");

            string keyString = Console.ReadLine();
            int key;

            if (int.TryParse(keyString, out key))
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

            bool isContinueCykle = true;

            while (isContinueCykle)
            {
                if (_dataBase.SearhName(namePlayer) == false)
                {
                    int level = _startLevelPlaer;
                    int money = _startMoneyPlaer;
                    bool banned = _startbanned;

                    _dataBase.AddPlayer(namePlayer, level, money, banned);

                    isContinueCykle = false;
                }
                else
                {
                    Console.WriteLine("Имя уже занято выберите другое - ");
                    namePlayer = Console.ReadLine();
                }
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
        private Dictionary<int, Player> _dataBase = new Dictionary<int, Player>();

        public bool UnBannedPlayer(int key)
        {
            bool isValidKey = _dataBase.ContainsKey(key);

            if (isValidKey)
            {
                Player player = GetPlayer(key);

                player.UnBanned();
            }
            else
            {
                isValidKey = false;
            }

            return isValidKey;
        }

        public bool BannedPlayer(int key)
        {
            bool isValidKey = _dataBase.ContainsKey(key);

            if (isValidKey)
            {
                Player player = GetPlayer(key);

                player.Banned();
            }
            else
            {
                isValidKey = false;
            }

            return isValidKey;
        }

        public List<int> KeyList()
        {
            List<int> keyList = new List<int>();

            foreach (int key in _dataBase.Keys)
            {
                keyList.Add(key);
            }

            return keyList;
        }

        public bool SearhName(string name)
        {
            bool thereIsName = false;

            for (int i = 1; i <= _dataBase.Count; i++)
            {
                if (ContainsKey(i))
                {
                    if (name == _dataBase[i].Name())
                    {
                        thereIsName = true;
                        break;
                    }
                }

            }
            return thereIsName;
        }

        public bool ContainsKey(int key)
        {
            return _dataBase.ContainsKey(key);
        }

        public bool DeletePlayer(int key)
        {
            bool isValidKey = _dataBase.ContainsKey(key);

            if (isValidKey)
            {
                _dataBase.Remove(key);
            }
            else
            {
                isValidKey = false;
            }

            return isValidKey;
        }

        public int Count()
        {
            return _dataBase.Count;
        }

        public Player GetPlayer(int key)
        {
            Player player;

            _dataBase.TryGetValue(key, out player);

            return player;
        }

        public void AddPlayer(string name, int level, int money, bool banned)
        {
            Player player = new Player(name, level, money, banned);

            int count = _dataBase.Count;
            for (int i = 1; i < count; i++)
            {
                if (_dataBase.ContainsKey(i) == false)
                {
                    count = i;
                    break;
                }
            }

            _dataBase.Add(count, player);
        }
    }

    class Player
    {
        private string _name;
        private int _level;
        private int _money;
        private bool _banned;

        public Player()
        { }

        public Player(string name, int level, int money, bool banned)
        {
            _name = name;
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

        public string Name()
        {
            return _name;
        }

        public string Show()
        {
            string ban;

            if (_banned == true)
            {
                ban = "Забанен";
            }
            else
            {
                ban = "Не забанен";
            }
            string plaer = "Имя - " + _name + "  " + "Уровень - " + _level + "  " + "Золота - " + _money + "  " + "Статус - " + ban;

            return plaer;
        }
    }
}
