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
                Console.Clear();

                switch (userInput)
                {
                    case (int)MenuCommands.AddPlayer:
                        database.AddPlayer();
                        break;
                    case (int)MenuCommands.RemovePlayer:
                        database.RemovePlayer();
                        break;
                    case (int)MenuCommands.BanPlayer:
                        database.BanPlayer();
                        break;
                    case (int)MenuCommands.UnbanPlayer:
                        database.UnbanPlayer();
                        break;
                    case (int)MenuCommands.Exit:
                        isOpen = false;
                        break;
                    default:
                        Console.WriteLine("Я не знаю такой команды!");
                        break;
                }

                Console.ReadKey();
                Console.Clear();
            }

            Console.WriteLine("Выход ...");
        }

        public static int GetNumber(string numberText)
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

        public void AddPlayer(Player player = null)
        {
            if(player == null)
            {
                Console.WriteLine("Введите Имя игрока:");
                string playerName = Console.ReadLine();
                player = new Player(playerName);
            }

            _players.Add(player);
            Console.WriteLine("Добавлен новый игрок");
            player.ShowInfo();
        }

        public void RemovePlayer()
        {
            ShowAllPlayers();
            Console.WriteLine("Введите Айди игрока, которого хотите удалить:");
            int id = Program.GetNumber(Console.ReadLine());
            Player player;

            if (TryGetPlayer(id, out player))
            {
                Console.WriteLine("Удален игрок");
                player.ShowInfo();
                _players.Remove(player);
            }
            else
            {
                Console.WriteLine("Неверный айди");
            }
        }

        public void BanPlayer()
        {
            ShowAllPlayers();
            Console.WriteLine("Введите Айди игрока, которого хотите удалить:");
            int id = Program.GetNumber(Console.ReadLine());
            Player player;

            if (TryGetPlayer(id, out player))
            {
                Console.WriteLine($"Игрок с Айди - {player.Id} | Имя - {player.Name} был заблокирован!");
                player.Ban();
            }
            else
            {
                Console.WriteLine("Неверный айди");
            }
        }

        public void UnbanPlayer()
        {
            ShowAllPlayers();
            Console.WriteLine("Введите Айди игрока, которого хотите удалить:");
            int id = Program.GetNumber(Console.ReadLine());
            Player player;

            if (TryGetPlayer(id, out player))
            {
                Console.WriteLine($"Игрок с Айди - {player.Id} | Имя - {player.Name} был разблокирован!");
                player.Unban();
            }
            else
            {
                Console.WriteLine("Неверный айди");
            }
        }

        public void ShowAllPlayers()
        {
            for (int i = 0; i < _players.Count; i++)
            {
                _players[i].ShowInfo();
            }
        }

        private bool TryGetPlayer(int id, out Player player)
        {
            player = null;

            for (int i = 0; i < _players.Count; i++)
            {
                if (_players[i].Id == id)
                {
                    player = _players[i];
                    return true;
                }
            }

            return false;
        }
    }

    class Player
    {
        private static int _ids;

        public int Id { get; private set; }
        public string Name { get; set; }
        public int Level { get; private set; }
        public bool IsBan { get; private set; }

        public Player(string name)
        {
            Id = ++_ids;
            Name = name;
            Level = 0;
            IsBan = false;
        }

        public void Ban()
        {
            IsBan = true;
        }

        public void Unban()
        {
            IsBan = false;
        }

        public void ShowInfo()
        {
            Console.WriteLine($"Порядковый номер - {Id} | Имя - {Name} | Уровень: {Level} | Забанен: {IsBan}");
        }
    }
}