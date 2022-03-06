using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayerDatabase
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Database database = new Database();
            int userInput;
            bool isOpen = true;

            while (isOpen)
            {
                Console.WriteLine($"{(int)MenuCommands.AddPlayer}. {MenuCommands.AddPlayer}" +
                                  $"\n{(int)MenuCommands.RemovePlayer}. {MenuCommands.RemovePlayer}" +
                                  $"\n{(int)MenuCommands.BanPlayer}. {MenuCommands.BanPlayer}" +
                                  $"\n{(int)MenuCommands.UnbanPlayer}. {MenuCommands.UnbanPlayer}" +
                                  $"\n{(int)MenuCommands.Exit}. {MenuCommands.Exit}");
                Console.Write("Выберите команду: ");
                userInput = GetNumber(Console.ReadLine());

                switch (userInput)
                {
                    case (int)MenuCommands.AddPlayer:
                        Console.Clear();
                        Console.WriteLine("Введите Имя игрока:");
                        string playerName = Console.ReadLine();
                        database.AddPlayer(new Player(playerName));
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case (int)MenuCommands.RemovePlayer:
                        Console.Clear();
                        database.ShowAllPlayers();
                        Console.WriteLine("Введите Номер игрока, которого хотите удалить:");
                        int playerNumber = GetNumber(Console.ReadLine());
                        database.RemovePlayer(playerNumber);
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case (int)MenuCommands.BanPlayer:
                        Console.Clear();
                        database.ShowAllPlayers();
                        Console.WriteLine("\nВведите Айди игрока, которого хотите забанить:");
                        int playerId = GetNumber(Console.ReadLine());
                        database.BanPlayer(playerId);
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case (int)MenuCommands.UnbanPlayer:
                        Console.Clear();
                        database.ShowAllPlayers();
                        Console.WriteLine("Введите Айди игрока, которого хотите разбанить:");
                        playerId = GetNumber(Console.ReadLine());
                        database.UnbanPlayer(playerId);
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case (int)MenuCommands.Exit:
                        isOpen = false;
                        Console.Clear();
                        break;
                    default:
                        Console.WriteLine("Я не знаю такой команды!");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                }
            }

            Console.WriteLine("Выход ...");
        }

        static int GetNumber(string numberText)
        {
            int number;

            while (int.TryParse(numberText, out number) == false)
            {
                Console.WriteLine("Введено не число.");
                Console.Write("Повторите ввод: ");
                numberText = Console.ReadLine();
            }

            return number;
        }
    }

    enum MenuCommands
    {
        AddPlayer = 1,
        RemovePlayer,
        BanPlayer,
        UnbanPlayer,
        Exit
    }

    class Database
    {
        private List<Player> _players = new List<Player>();

        public void AddPlayer(Player player)
        {
            _players.Add(player);
            Console.WriteLine("Добавлен новый игрок");
            player.ShowInfo();
        }

        public void RemovePlayer(int index)
        {
            if (_players.Contains(_players[index]))
            {
                Console.WriteLine("Удален игрок");
                _players[index].ShowInfo();
                _players.RemoveAt(index);
            }
            else
            {
                Console.WriteLine("Игрок не был найден!");
            }
        }

        public void BanPlayer(int id)
        {
            foreach (var player in _players)
            {
                if (player.Id == id)
                {
                    Console.WriteLine($"Игрок с Айди - {player.Id} и Имя - {player.Name} был заблокирован!");
                    player.Ban();
                }
            }
        }

        public void UnbanPlayer(int id)
        {
            foreach (var player in _players)
            {
                if (player.Id == id)
                {
                    Console.WriteLine($"Игрок с Айди - {player.Id} и Имя - {player.Name} был разблокирован!");
                    player.Unban();
                }
            }
        }

        public void ShowAllPlayers()
        {
            for (int i = 0; i < _players.Count; i++)
            {
                Console.Write(i + ". ");
                _players[i].ShowInfo();
            }
        }
    }

    class Player
    {
        private static int _ids;

        public int Id { get; private set; }
        public string Name { get; set; }
        public int Level { get; private set; }
        public bool isBan { get; private set; }

        public Player(string name)
        {
            Id = ++_ids;
            Name = name;
            Level = 0;
            isBan = false;
        }

        public void Ban()
        {
            isBan = true;
        }

        public void Unban()
        {
            isBan = false;
        }

        public void ShowInfo()
        {
            Console.WriteLine($"Порядковый номер - {Id} | Имя - {Name} | Уровень: {Level} | Забанен: {isBan}");
        }
    }
}