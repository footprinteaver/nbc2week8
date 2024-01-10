
using System.Xml.Linq;
using System.Collections.Generic;
using System.Numerics;

namespace _5week_assignment
{
    //hi
    public class Character
    {
        public string Name { get; }
        public string Job { get; }
        public int Level { get; }
        public int Atk { get; }
        public int Def { get; }
        public int MaxHp { get; }
        public int CurrentHp { get; set; }
        public int Gold { get; }

        public Character(string name, string job, int level, int atk, int def, int maxhp, int gold)
        {
            Name = name;
            Job = job;
            Level = level;
            Atk = atk;
            Def = def;
            MaxHp = maxhp;
            CurrentHp = maxhp;
            Gold = gold;
        }
    }

    public class Monster
    {
        public string Name { get; }
        public int Lv { get; } 
        public int Atk { get; }
        public int MaxHp { get; }
        public int CurrentHp { get; set; }

        public Monster(string name, int lv, int atk, int maxhp)
        {
            Name = name;
            Lv = lv;
            Atk = atk;
            MaxHp = maxhp;
        }
    }


    public class Item
    {
        public string Name { get; }
        public string Description { get; }
        public int Type { get; }
        public int Atk { get; }
        public int Def { get; }
        public int Hp { get; }
        public bool isEquipped { get; set; }

        public Item(string name, string description, int type, int atk, int def, int hp, bool isEquipped = false)
        {
            Name = name;
            Description = description;
            Type = type;
            Atk = atk;
            Def = def;
            Hp = hp;
            isEquipped = isEquipped;
        }
       

       public static int ItemCnt = 0;

       public void PrintItemStatDescription(bool withNumber = false, int idx = 0)
       {
           Console.Write("- ");

           if (withNumber)
           {
               Console.ForegroundColor = ConsoleColor.DarkMagenta;
               Console.Write("{0} ", idx);
               Console.ResetColor();
           }

           if (isEquipped)
           {
               Console.Write("[");
               Console.ForegroundColor = ConsoleColor.Cyan;
               Console.Write("E");
               Console.ResetColor();
               Console.Write("]");
           }
           else
           {
               Console.Write(PadRightForMixedText(Name, 12));
           }

           Console.Write(" | ");

           if (Atk != 0)
           {
               Console.Write($"Atk {(Atk >= 0 ? "+" : "")}{Atk}");
           }
           if (Def != 0)
           {
               Console.Write($"Def {(Def >= 0 ? "+" : "")}{Def}");
           }
           if (Hp != 0)
           {
               Console.Write($"Hp {(Hp >= 0 ? "+" : "")}{Hp}");
           }

           Console.Write(" | ");

           Console.WriteLine(Description);

       }

       public static int GetPrintableLength(string str)
       {
           int length = 0;
           foreach (char c in str)
           {
               if (char.GetUnicodeCategory(c) == System.Globalization.UnicodeCategory.OtherLetter)
               {
                   length += 2;
               }
               else
               {
                   length += 1;
               }
           }

           return length;
       }

       public static string PadRightForMixedText(string str, int totalLength)
       {
           int currentLength = GetPrintableLength(str);
           int padding = totalLength - currentLength;
           return str.PadRight(str.Length + padding);
       }
    }



    internal class Program
    {
        private static Character _player;
        private static Item[] _items;
        private static Monster[] _monster;

        static void Main(string[] args)
        {
            GameDataSetting();
            PrintStartLogo();
            startMenu();

        }



        private static void GameDataSetting()
        {
            _player = new Character("Chad", "전사", 1, 10, 5, 10, 1500);
           _items = new Item[10];
           _monster = new Monster[10];

           AddItem(new Item("무쇠 갑옷", "무쇠로 만들어져 튼튼한 갑옷입니다.", 0, 0, 5, 0));
           AddItem(new Item("낡은 검", "쉽게 볼 수 있는 낡은 검입니다.", 1, 2, 0, 0));

        }

        static void AddItem(Item item)
        {
           if (Item.ItemCnt == 10)
           {
               return;
           }

           _items[Item.ItemCnt] = item;
           Item.ItemCnt++;
        }

        private static void PrintStartLogo()
        {
            Console.WriteLine("====================================================================");
            Console.WriteLine("                                __                     \r\n");
            Console.WriteLine("  ____________ _____  _______ _/  |_ _____             \r\n");
            Console.WriteLine(" /  ___/\\____ \\\\__  \\ \\_  __ \\\\   __\\\\__  \\            \r\n");
            Console.WriteLine(" \\___ \\ |  |_> >/ __ \\_|  | \\/ |  |   / __ \\_          \r\n");
            Console.WriteLine("/____  >|   __/(____  /|__|    |__|  (____  /          \r\n");
            Console.WriteLine("     \\/ |__|        \\/                    \\/           \r\n");
            Console.WriteLine("                                                       \r\n");
            Console.WriteLine("    .___                 ____                          \r\n");
            Console.WriteLine("  __| _/__ __   ____    / ___\\   ____   ____    ____   \r\n");
            Console.WriteLine(" / __ ||  |  \\ /    \\  / /_/  >_/ __ \\ /  _ \\  /    \\  \r\n");
            Console.WriteLine("/ /_/ ||  |  /|   |  \\ \\___  / \\  ___/(  <_> )|   |  \\ \r\n");
            Console.WriteLine("\\____ ||____/ |___|  //_____/   \\___  >\\____/ |___|  / \r\n");
            Console.WriteLine("     \\/            \\/               \\/             \\/  \r\n");
            Console.WriteLine("                                                       ");
            Console.WriteLine("====================================================================");
            Console.WriteLine("                        Press AnyKey To Start                       ");
            Console.WriteLine("====================================================================");
            Console.ReadKey();

        }

        static void startMenu()
        {
            Console.Clear();
            Console.WriteLine("■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■");
            Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
            Console.WriteLine("이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.");
            Console.WriteLine("■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■");
            Console.WriteLine("1. 상태 보기");
            Console.WriteLine("2. 인벤토리");
            Console.WriteLine("3. 던전으로");

            switch (CheckValidInput(1, 3))
            {
                case 1:
                    StatusMenu();
                    break;
                case 2:
                   InventoryMenu();
                    break;
                case 3:
                    BattleMenu();
                    break;
            }


        }


        private static int CheckValidInput(int min, int max)
        {
            int keyInput;
            bool result;

            do
            {
                Console.WriteLine("원하시는 행동을 입력해주세요.");
                result = int.TryParse(Console.ReadLine(), out keyInput);
            }
            while (result == false || CheckIfValid(keyInput, min, max) == false);
            
            return keyInput;
        }

        private static bool CheckIfValid(int keyInput, int min, int max)
        {
            if (min <= keyInput && keyInput <= max)
            {
                return true;
            }
            else
            {
                Console.WriteLine("喝!! 지정되지 않은 요청입니다.");
                return false;
            }
        }
        private static void ShowHighlightedText(string text)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(text);
            Console.ResetColor();
        }

        private static void PrintTextWithHighlights(string s1, string s2, string s3 = "")
        {
            Console.Write(s1);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(s2);
            Console.ResetColor();
            Console.WriteLine(s3);
        }

        private static void StatusMenu()
        {
            
            Console.Clear();

            ShowHighlightedText("■상 태  보 기■");
            Console.WriteLine("캐릭터의 정보가 표기됩니다.");

            PrintTextWithHighlights("Lv. ", _player.Level.ToString("00"));
            Console.WriteLine();
            Console.WriteLine("{0} ( {1} )", _player.Name, _player.Job);



           int bonusAtk = getSumBonusAtk();
           PrintTextWithHighlights("공격력 : ", (_player.Atk + bonusAtk).ToString(), bonusAtk > 0 ? string.Format(" (+{0})", bonusAtk) : "");

           int bonusDef = getSumBonusDef();
           PrintTextWithHighlights("방어력 : ", (_player.Def + bonusDef).ToString(), bonusDef > 0 ? string.Format(" (+{0})", bonusDef) : "");

           int bonusHp = getSumBonusHp();
           PrintTextWithHighlights("체력 : ", (_player.MaxHp + bonusHp).ToString(), bonusHp > 0 ? string.Format(" (+{0})", bonusHp) : "");


            PrintTextWithHighlights("골드 : ", _player.Gold.ToString());
            Console.WriteLine();
            Console.WriteLine("0. 뒤로 가기");
            Console.WriteLine();

            switch (CheckValidInput(0, 0))
            {
                case 0:
                    startMenu();
                    break;
            }
        }

        private static int getSumBonusAtk()
        {
           int sum = 0;
           for (int i = 0; i < Item.ItemCnt; i++)
           {
               if (_items[i].isEquipped)
               {
                   sum += _items[i].Atk;
               }
           }
           return sum;
        }

        private static int getSumBonusDef()
        {
           int sum = 0;
           for (int i = 0; i < Item.ItemCnt; i++)
           {
               if (_items[i].isEquipped)
               {
                   sum += _items[i].Def;
               }
           }
           return sum;
        }

        private static int getSumBonusHp()
        {
           int sum = 0;
           for (int i = 0; i < Item.ItemCnt; i++)
           {
               if (_items[i].isEquipped)
               {
                   sum += _items[i].Hp;
               }
           }
           return sum;
        }

        private static void InventoryMenu()
        {
           Console.Clear();

           ShowHighlightedText("인 벤  토 리");
           Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
           Console.WriteLine("");
           Console.WriteLine("[아이템 목록]");

           for (int i = 0; i < Item.ItemCnt; i++)
           {
               _items[i].PrintItemStatDescription();
           }
           Console.WriteLine("");
           Console.WriteLine("0. 나가기");
           Console.WriteLine("1. 장착관리");
           Console.WriteLine("");

           switch (CheckValidInput(0, 1))
           {
               case 0:
                   startMenu();
                   break;
               case 1:
                   EquipMenu();
                   break;
           }
        }

        private static void EquipMenu()
        {
           Console.Clear();

           ShowHighlightedText("인 벤  토 리 - 장 착  관 리");
           Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
           Console.WriteLine("");
           Console.WriteLine("[아이템 목록]");

           for (int i = 0; i < Item.ItemCnt; i++)
           {
               _items[i].PrintItemStatDescription(true, i + 1);
           }

           Console.WriteLine("");
           Console.WriteLine("0. 나가기");

           int keyInput = CheckValidInput(0, Item.ItemCnt);

           switch (keyInput)
           {
               case 0:
                   InventoryMenu();
                   break;
               default:
                   ToggleEquipStatus(keyInput - 1);
                   EquipMenu();
                   break;
           }
        }

        private static void ToggleEquipStatus(int idx)
        {
           _items[idx].isEquipped = !_items[idx].isEquipped;
        }

        private static void BattleMenu()
        {
            Console.Clear();
            
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("몬스터들과 마주했다!");
            Console.ResetColor();
            List < Monster > encounter = GetRandomMonster(1, 4);
            while (true)
            {
                DisplayBattle();

                Console.WriteLine("원하는 행동을 선택하세요.");
                Console.WriteLine("1. 공격");
                Console.WriteLine("0. 종료");

                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        PlayerAttack();
                        if (CheckVictory())
                        {
                            Console.WriteLine("모든 몬스터를 처치했습니다. 승리하셨습니다!");
                            return;
                        }
                        EnemyPhase();
                        if (CheckPlayerLose())
                        {
                            Console.WriteLine("플레이어가 전멸했습니다. 패배하셨습니다!");
                            return;
                        }
                        break;
                    case "0":
                        Console.WriteLine("게임을 종료합니다.");
                        return;
                    default:
                        Console.WriteLine("잘못된 입력입니다. 다시 선택하세요.");
                        break;
                }
            }


        }

        static List<Monster> GetRandomMonster(int minCount, int maxCount)
        {
            List<Monster> randomMonsters = new List<Monster>();
            Random random = new Random();

            // 몬스터 리스트에서 무작위로 선택
            for (int i = 0; i < random.Next(minCount, maxCount + 1); i++)
            {
                Monster randomMonster = monster[random.Next(monster.Count)];
                randomMonsters.Add(new Monster(randomMonster.Name, randomMonster.Lv, randomMonster.Atk, randomMonster.MaxHp));
            }

            return randomMonsters;
        }

        private static void DisplayBattle()
        {
            Console.WriteLine("[내 정보]");
            Console.WriteLine("{player.Name} (Lv.{player.Level})");
            Console.WriteLine("HP: {player.CurrentHP}/{player.MaxHP}");

            Console.WriteLine("[몬스터]");
            for (int i = 0; i < monster.Count; i++)
            {
                Console.WriteLine($"{i + 1} Lv.{monster[i].Lv} {monster[i].Name} HP: {monster[i].MaxHp}");
            }
        }


        private static bool CheckPlayerLose()
        {
            throw new NotImplementedException();
        }

        private static void EnemyPhase()
        {
            throw new NotImplementedException();
        }

        private static bool CheckVictory()
        {
            throw new NotImplementedException();
        }

        private static void PlayerAttack()
        {
            throw new NotImplementedException();
        }
    

        static List<Monster> monster = new List<Monster>
    {
        new Monster( "슬라임", 1, 1, 20 ),
        new Monster( "고블린", 3, 5, 30 ),
        new Monster( "시궁쥐", 2, 4, 20 ),
        new Monster( "늑대", 4, 8, 25 ),
        new Monster( "피폐해진 개발자", 1, 1, 10),
        new Monster( "흉폭해진 개발자", 5, 10, 30),

    };
    }


}
