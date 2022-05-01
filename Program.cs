namespace Homework4
{
    class RPG
    {
        static void Main(string[] args)
        {
            Random rand = new Random();

            //Выбор имени команды игрока
            Console.Write("Введите имя вашей команды: ");
            string yourTeamName = Console.ReadLine();

            //Выбор имени команды компьютера
            List<string> names = (new string[] { "Куряты", "Перуанцы", "Муравьеды", "Великие ленивцы", "Краказябры", "Дартаньяны" }).ToList();
            string enemyTeamName = names[rand.Next(names.Count)];
            Console.WriteLine($"\nКомпьютер выбрал имя команды: {enemyTeamName}\n\nНажмите любую клавишу, чтобы продолжить");
            Console.ReadKey();

            //Создание системы выборки уникальных персонажей для каждой команды
            List<Characters> listOfCharacter = new List<Characters>() { new Mage(), new Warrior(), new Monk(), new Berserk(), new Banshee(), new Hero()};
            List<Characters> yourCharacters = new List<Characters>();
            List<Characters> enemyCharacters = new List<Characters>();

            //Набор в команду игрока
            while (yourCharacters.Count != 3)
            {
                Console.Clear();
                Console.WriteLine("Выберите персонажей в команду:");
                foreach(Characters c in listOfCharacter)
                {
                    Console.WriteLine($"{listOfCharacter.IndexOf(c) + 1}. {c.Name}");
                }
                ConsoleKeyInfo choose = Console.ReadKey(true);
                switch (choose.Key)
                {
                    case ConsoleKey.D1:
                        yourCharacters.Add(listOfCharacter[0]);
                        listOfCharacter.RemoveAt(0);
                       break;
                    case ConsoleKey.D2:
                        if(listOfCharacter.Count > 1)
                        {
                            yourCharacters.Add(listOfCharacter[1]);
                            listOfCharacter.RemoveAt(1);
                        }
                        break;
                    case ConsoleKey.D3:
                        if (listOfCharacter.Count > 2)
                        {
                            yourCharacters.Add(listOfCharacter[2]);
                            listOfCharacter.RemoveAt(2);
                        }
                        break;
                    case ConsoleKey.D4:
                        if (listOfCharacter.Count > 3)
                        {
                            yourCharacters.Add(listOfCharacter[3]);
                            listOfCharacter.RemoveAt(3);
                        }
                        break ;
                    case ConsoleKey.D5:
                        if (listOfCharacter.Count > 4)
                        {
                            yourCharacters.Add(listOfCharacter[4]);
                            listOfCharacter.RemoveAt(4);
                        }
                        break;
                    case ConsoleKey.D6:
                        if (listOfCharacter.Count > 5)
                        {
                            yourCharacters.Add(listOfCharacter[5]);
                            listOfCharacter.RemoveAt(5);
                        }
                        break;
                    default: continue;
                }
            }

            //Набор в команду противника
            while (enemyCharacters.Count != 3)
            {
                int CharacterIndex = rand.Next(listOfCharacter.Count);
                enemyCharacters.Add(listOfCharacter[CharacterIndex]);
                listOfCharacter.RemoveAt(CharacterIndex);
            }
            Console.Clear();

            //Разработка системы боя
            while (true)
            {
                Console.WriteLine($"\n----------------------------------- \nСписок персонажей в команде {yourTeamName}:");
                foreach (Characters yc in yourCharacters)
                    Console.WriteLine($"{yc.Name}({yc.HP} HP), базовый урон: {yc.Damage} ");

                Console.WriteLine($"\nСписок персонажей в команде {enemyTeamName}:");
                foreach (Characters ec in enemyCharacters)
                    Console.WriteLine($"{ec.Name}({ec.HP} HP), базовый урон: {ec.Damage} ");

                //Атака игрока
                int whoIsAttackingPlayer;
                int whomIsAttackingPlayer;
                Console.WriteLine($"\n-----------------------------------");
                Console.Write($"Выбери кем будешь атаковать: ");
                bool result1 =int.TryParse(Console.ReadLine(), out whoIsAttackingPlayer);
                if (result1)
                {
                    if(whoIsAttackingPlayer <= yourCharacters.Count)
                        Console.WriteLine($"Ты выбрал: {yourCharacters[whoIsAttackingPlayer - 1].Name}");
                    else
                    {
                        Console.WriteLine("Нет такого персонажа");
                        Console.ReadKey();
                        continue;
                    }
                }
                else
                {
                    Console.WriteLine("Введите число");
                    Console.ReadKey();
                    continue;
                }


                Console.Write($"Выбери кого будешь атаковать: ");
                bool result2 = int.TryParse(Console.ReadLine(), out whomIsAttackingPlayer);
                if (result2)
                {
                    if (whomIsAttackingPlayer <= enemyCharacters.Count)
                        Console.WriteLine($"Ты выбрал: {enemyCharacters[whomIsAttackingPlayer - 1].Name}");
                    else
                    {
                        Console.WriteLine("Нет такого персонажа");
                        Console.ReadKey();
                        continue;
                    }
                }
                else
                {
                    Console.WriteLine("Введите число");
                    Console.ReadKey();
                    continue;
                }

                enemyCharacters[whomIsAttackingPlayer - 1].HP -= yourCharacters[whoIsAttackingPlayer - 1].Damage;
                Console.Write($"\n{yourCharacters[whoIsAttackingPlayer - 1].Name}({yourTeamName})");
                yourCharacters[whoIsAttackingPlayer - 1].typeOfAttack();
                Console.WriteLine($"и нанёс {yourCharacters[whoIsAttackingPlayer - 1].Damage} " +
                    $"единиц урона {enemyCharacters[whomIsAttackingPlayer - 1].Name}({enemyTeamName})\n\n-----------------------------------\nНажмите любую клавишу, чтобы продолжить");
                Console.ReadKey();
                if (enemyCharacters[whomIsAttackingPlayer - 1].HP == 0)
                    enemyCharacters.RemoveAt(whomIsAttackingPlayer - 1);
                if (enemyCharacters.Count < 1)
                {
                    Console.Clear();
                    Console.WriteLine($"Победила команда {yourTeamName}");
                    break;
                }

                //Атака компьютера
                int whoIsAttackingEnemy = rand.Next(enemyCharacters.Count);
                int whomIsAttackingEnemy = rand.Next(yourCharacters.Count);
                yourCharacters[whomIsAttackingEnemy].HP -= enemyCharacters[whoIsAttackingEnemy].Damage;
                Console.Write($"\nХод противника\n{enemyCharacters[whoIsAttackingEnemy].Name}({enemyTeamName})");
                enemyCharacters[whoIsAttackingEnemy].typeOfAttack();  
                Console.WriteLine($"и нанёс {enemyCharacters[whoIsAttackingEnemy].Damage} " +
                    $"единиц урона {yourCharacters[whomIsAttackingEnemy].Name}({yourTeamName})\n\n-----------------------------------\nНажмите любую клавишу, чтобы продолжить");
                Console.ReadKey();
                Console.Clear();
                if (yourCharacters[whomIsAttackingEnemy].HP == 0)
                    yourCharacters.RemoveAt(whomIsAttackingEnemy);
                if(yourCharacters.Count < 1)
                {
                    Console.Clear();
                    Console.WriteLine($"Победила команда {enemyTeamName}");
                    break;
                }


            }  
        }

        public abstract class Characters
        {
            private int _hp;

            public string Name;
            public int Damage;
            protected Characters(string name, int hp, int damage)
            {
                Name = name;
                HP = hp;
                Damage = damage;
            }

            public int HP 
            {
                get
                {
                    return _hp;
                }
                set
                {
                    if(value < 0)
                        _hp = 0;
                    else
                        _hp = value;
                } 
            }
            
            public abstract void typeOfAttack();
        }

        class Mage: Characters 
        {
            public Mage() : base("Маг", 70, 30) {}

            public override void typeOfAttack()
            {
                Console.Write("Кидает огненный шар ");
            }
        }
        class Warrior: Characters
        {
            public Warrior() : base("Воин", 130, 10){}

            public override void typeOfAttack()
            {
                Console.Write("Врывается с двух ног ");
            }
        }
        class Monk: Characters
        {
            public Monk() : base("Монах",100, 20){}

            public override void typeOfAttack()
            {
                Console.Write("Бьет палкой по голове ");
            }
        }
        class Berserk : Characters
        {
            public Berserk() : base("Берсерк", 30, 50) { }

            public override void typeOfAttack()
            {
                Console.Write("Отгразает ухо ");
            }
        }
        class Banshee : Characters
        {
            public Banshee() : base("Банши", 60, 20) { }

            public override void typeOfAttack()
            {
                Console.Write("Тупо орет ");
            }
        }
        class Hero : Characters
        {
            public Hero() : base("Герой боевиков", 200, 5) { }

            public override void typeOfAttack()
            {
                Console.Write("Стреляет холостыми ");
            }
        }
    }

}