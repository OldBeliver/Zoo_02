using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zoo_02
{
    class Program
    {
        static void Main(string[] args)
        {
            AnimalPark zoo = new AnimalPark();
            zoo.Work();
        }
    }

    class AnimalPark
    {
        private List<Enclosure> _enclosures;

        public string Name { get; private set; }

        public AnimalPark()
        {
            _enclosures = new List<Enclosure>();
            
            Name = GetNameZoo();

            int enclosureNumber = 10;
            CreateEnclosure(enclosureNumber);
        }

        public void Work()
        {
            bool isOpen = true;

            Console.WriteLine($"Добро пожаловать");
            Console.WriteLine($"в {Name}\n");

            while (isOpen)
            {
                Console.WriteLine($"Перед Вами план-схема зоопарка\n");
                ShowAllEnclosures();

                Console.Write($"\nВведите номер вольера или exit для выхода: ");
                string userInput = Console.ReadLine();

                if (userInput == "exit")
                {
                    isOpen = false;
                }
                else if (GetNumber(userInput, out int number))
                {
                    if (number >= 1 && number <= _enclosures.Count)
                    {
                        Console.WriteLine($"\nобитатель вольера: семейство {_enclosures[number - 1].Family}");
                        Console.WriteLine($"Всего {_enclosures[number - 1].CountAnimals()} особей");
                        _enclosures[number - 1].ShowAllEnimals();
                        
                        Console.WriteLine($"из них мальчиков {_enclosures[number - 1].MalesCount} и девочек {_enclosures[number - 1].FemalesCount}");

                        Console.WriteLine($"в период брачных игр или борьбы за территорию издают характерный звук: {_enclosures[number - 1].Cry()}");
                    }
                    else
                    {
                        Console.WriteLine($"вольер с таким номером еще не открыли");
                    }
                }
                else
                {
                    Console.WriteLine($"ошибка ввода");
                }

                Console.WriteLine($"любую для продолжения ...");
                Console.ReadKey();
                Console.Clear();
            }
        }

        private string GetNameZoo()
        {
            return "плавучий зоопарк редких животных\nпассажирского лайнера «Доктор наук профессор Шварценгольд»";
        }

        private void CreateEnclosure(int quanity)
        {
            for (int i = 0; i < quanity; i++)
            {
                _enclosures.Add(new Enclosure());
            }
        }

        private void ShowAllEnclosures()
        {
            for (int i = 0; i < _enclosures.Count; i++)
            {
                Console.WriteLine($"Вольер № {i + 1:d2} семейство {_enclosures[i].Family}");
            }
        }

        private bool GetNumber(string userInput, out int number)
        {
            return int.TryParse(userInput, out number);
        }
    }

    class Enclosure
    {
        private static Random _rand = new Random();

        private List<Animal> _animals;
        private AnimalCreator _animalCreator;

        public string Family { get; private set; }
        public int MalesCount { get; private set; }
        public int FemalesCount { get; private set; }

        public Enclosure()
        {
            _animals = new List<Animal>();
            _animalCreator = new AnimalCreator();

            int maxAnimals = 6;
            int animalIndex = GetRandomIndex(maxAnimals);

            int maxAnimalsInEnclosure = 6;
            int number = GetRandomIndex(maxAnimalsInEnclosure);
            for (int i = 1; i < number + 1; i++)
            {
                AddAnimal(animalIndex);
            }

            Family = GetEnclosureName();
            CountGender();
        }

        public void ShowAllEnimals()
        {
            for (int i = 0; i < _animals.Count; i++)
            {
                Console.Write($"{i+1:d2}. ");
                _animals[i].ShowInfo();
            }
        }

        public int CountAnimals()
        {
            return _animals.Count;
        }
        public string Cry()
        {
            return _animals[0].Cry;
        }

        private void AddAnimal(int index)
        {
            switch (index)
            {
                case 1:
                    _animals.Add(_animalCreator.AddDuck());
                    break;
                case 2:
                    _animals.Add(_animalCreator.AddBear());
                    break;
                case 3:
                    _animals.Add(_animalCreator.AddButterfly());
                    break;
                case 4:
                    _animals.Add(_animalCreator.AddCow());
                    break;
                case 5:
                    _animals.Add(_animalCreator.AddRabbit());
                    break;
                case 6:
                    _animals.Add(_animalCreator.AddWorm());
                    break;
                default:
                    _animals.Add(_animalCreator.AddBear("топтыгин"));
                    break;
            }
        }

        private int GetRandomIndex(int count)
        {
            return _rand.Next(1, count + 1);
        }

        private string GetEnclosureName()
        {
            return _animals[0].Family;
        }

        private void CountGender()
        {
            int nubmerMale = 0;
            int numberFemale = 0;

            for (int i = 0; i < _animals.Count; i++)
            {
                if (_animals[i].Gender == Gender.Male)
                {
                    nubmerMale++;
                }
                else if (_animals[i].Gender == Gender.Female)
                {
                    numberFemale++;
                }
            }

            MalesCount = nubmerMale;
            FemalesCount = numberFemale;
        }
    }

    class Animal
    {
        public string Family { get; private set; }
        public string Name { get; private set; }
        public Gender Gender { get; private set; }
        public string Cry { get; private set; }

        public Animal(string family, string name, Gender gender, string cry)
        {
            Family = family;
            Name = name;
            Gender = gender;
            Cry = cry;
        }

        public void ShowInfo()
        {
            string gender = ConvertGenderToText(Gender);
            
            Console.WriteLine($"{Name} {gender}");
        }

        private string ConvertGenderToText(Gender gender)
        {
            string genderText = "мальчик";

            if(gender == Gender.Female)
            {
                genderText = "девочка";
            }

            return genderText;
        }
    }

    enum Gender
    {
        Male,
        Female
    }

    class AnimalCreator
    {
        private static Random _rand = new Random();

        private string _family;
        private string _name;
        private Gender _gender;
        private string _cry;

        public Animal AddBear()
        {
            _family = "медвежьи";
            _name = "медведь-летун";
            _gender = GetRandomGender();
            _cry = "лечу-у-у-у";

            return new Animal(_family, _name, _gender, _cry);
        }

        public Animal AddBear(string name)
        {
            _family = "медвежьи";
            _name = name;
            _gender = GetRandomGender();
            _cry = "лечу-у-у-у";

            return new Animal(_family, _name, _gender, _cry);
        }

        public Animal AddCow()
        {
            _family = "полорогие";
            _name = "скунсовидная корова";
            _gender = GetRandomGender();
            _cry = "фу-у-у-у";

            return new Animal(_family, _name, _gender, _cry);
        }

        public Animal AddWorm()
        {
            _family = "почвенные";
            _name = "подкустовый выползень";
            _gender = GetRandomGender();
            _cry = "ш-ш-ш-ш";

            return new Animal(_family, _name, _gender, _cry);
        }

        public Animal AddRabbit()
        {
            _family = "зайцевые";
            _name = "кролик зануда";
            _gender = GetRandomGender();
            _cry = "фыр-фыр-фыр";

            return new Animal(_family, _name, _gender, _cry);
        }

        public Animal AddDuck()
        {
            _family = "утиные";
            _name = "серпень перепочатокрылый";
            _gender = GetRandomGender();
            _cry = "кря-кря";

            return new Animal(_family, _name, _gender, _cry);
        }

        public Animal AddButterfly()
        {
            _family = "чешуекрылые";
            _name = "одноразовая африканская бабочка";
            _gender = GetRandomGender();
            _cry = "бяк-бяк-бяк";

            return new Animal(_family, _name, _gender, _cry);
        }

        private Gender GetRandomGender()
        {
            Gender gender = Gender.Male;

            if (_rand.Next(0, 2) == 1)
            {
                gender = Gender.Female;
            }

            return gender;
        }
    }
}
