using System;
using System.Collections.Generic;
using System.Linq;

namespace TransferOfFighters
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Army army = new Army();
            army.Work();
        }
    }

    class Army
    {
        private List<Soldier> _firstSquad = new List<Soldier>();
        private List<Soldier> _secondSquad = new List<Soldier>();

        public Army()
        {
            int amountSoldier = 10;

            CreatorSoldier creatorSoldier = new CreatorSoldier();

            for (int i = 0; i < amountSoldier; i++)
            {
                _firstSquad.Add(creatorSoldier.GenerateSoldier());
                _secondSquad.Add(creatorSoldier.GenerateSoldier());
            }
        }

        public void Work()
        {
            ShowSquad();

            Console.WriteLine();

            TransferSoldiers();

            ShowSquad();
        }

        private void ShowSquad()
        {
            Console.WriteLine("Первый отряд");
            ShowSoldiers(_firstSquad);
            Console.WriteLine("\nВторой отряд");
            ShowSoldiers(_secondSquad);
        }

        private void ShowSoldiers(List<Soldier> soldiers)
        {
            foreach (Soldier soldier in soldiers)
                soldier.ShowInfo();
        }

        private void TransferSoldiers(char letter = 'Б')
        {
            _secondSquad = _secondSquad.Union(_firstSquad.Where(soldier => soldier.LastName[0] == letter)).ToList();

            _firstSquad = _firstSquad.Except(_secondSquad).ToList();
        }
    }

    class CreatorSoldier
    {
        public Soldier GenerateSoldier()
        {
            int minLimitServiceTimeMonth = 10;
            int maxLimitServiceTimeMonth = 121;

            return new Soldier(GenerateLastName(), GenerateWeapons(), GenerateRank(), UserUtils.GenerateRandomNumber(minLimitServiceTimeMonth, maxLimitServiceTimeMonth));
        }

        private string GenerateRank()
        {
            List<string> ranks = new List<string>() { "сержант", "полковник", "капитан", "лейтенант" };

            return ranks[UserUtils.GenerateRandomNumber(0, ranks.Count)];
        }

        private string GenerateWeapons()
        {
            List<string> weapons = new List<string>() { "Автомат", "Пулемёт", "Снайперская винтовка", "Пистолет" };

            return weapons[UserUtils.GenerateRandomNumber(0, weapons.Count)];
        }

        private string GenerateLastName()
        {
            List<string> names = new List<string>() { "Буркевич", "Мельников", "Иванов", "Серпов", "Блатных", "Беликов" };

            return names[UserUtils.GenerateRandomNumber(0, names.Count)];
        }
    }

    class Soldier
    {
        private string _weapons;
        private int _serviceTimeMonth;

        public Soldier(string lastName, string weapons, string rank, int serviceTimeMonth)
        {
            this.LastName = lastName;
            _weapons = weapons;
            Rank = rank;

            _serviceTimeMonth = serviceTimeMonth;
        }

        public string LastName { get; private set; }
        public string Rank { get; private set; }

        public void ShowInfo()
        {
            Console.WriteLine($"{Rank}-{LastName} вооружён: {_weapons}, служит {_serviceTimeMonth} месяцев.");
        }
    }

    class UserUtils
    {
        private static Random s_random = new Random();

        public static int GenerateRandomNumber(int minLimitRandom, int maxLimitRandom)
        {
            return s_random.Next(minLimitRandom, maxLimitRandom);
        }
    }
}
