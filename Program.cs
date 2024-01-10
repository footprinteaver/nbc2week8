using System;
using System.Security.Cryptography.X509Certificates;

namespace _5week_assignment
{
    //lee
    public class Character
    {
        public string Name { get; }
        public string Job { get; }
        public int Level { get; }
        public int Atk { get; }
        public int Def { get; }
        public int Hp { get; }
        public int Gold { get; }

        public Character(string name, string job, int level, int atk, int def, int hp, int gold)
        {
            Name = name;
            Job = job;
            Level = level;
            Atk = atk;
            Def = def;
            Hp = hp;
            Gold = gold;
        }
    }

    //public class Item
    //{
    //    public string Name { get; }
    //    public string Description { get; }
    //    public int Type { get; }
    //    public int Atk { get; }
    //    public int Def { get; }
    //    public int Hp { get; }
    //    public bool isEquipped { get; set; }

    //    public Item(string name, string description, int type, int atk, int def, int hp, bool isEquipped = false)
    //    {
    //        Name = name;
    //        Description = description;
    //        Type = type;
    //        Atk = atk;
    //        Def = def;
    //        Hp = hp;
    //        isEquipped = isEquipped;
    //    }

    //    public static int ItemCnt = 0;

    //    public void PrintItemStatDescription(bool withNumber = false, int idx = 0)
    //    {
    //        Console.Write("- ");

    //        if(withNumber)
    //        {
    //            Console.ForegroundColor = ConsoleColor.DarkMagenta;
    //            Console.Write("{0} ", idx);
    //            Console.ResetColor();
    //        }

    //        if (isEquipped)
    //        {
    //            Console.Write("[");
    //            Console.ForegroundColor = ConsoleColor.Cyan;
    //            Console.Write("E");
    //            Console.ResetColor();
    //            Console.Write("]");
    //        }
    //        else
    //        {
    //            Console.Write(PadRightForMixedText(Name, 12));
    //        }

    //        Console.Write(" | ");

    //        if(Atk != 0)
    //        {
    //            Console.Write($"Atk {(Atk >= 0 ? "+" : "")}{Atk}");
    //        }
    //        if(Def != 0)
    //        {
    //            Console.Write($"Def {(Def >= 0 ? "+" : "")}{Def}");
    //        }
    //        if(Hp != 0)
    //        {
    //            Console.Write($"Hp {(Hp >= 0 ? "+" : "")}{Hp}");
    //        }

    //        Console.Write(" | ");

    //        Console.WriteLine(Description);

    //    }

    //    public static int GetPrintableLength(string str)
    //    {
    //        int length = 0;
    //        foreach (char c in str)
    //        {
    //            if (char.GetUnicodeCategory(c) == System.Globalization.UnicodeCategory.OtherLetter)
    //            {
    //                length += 2;
    //            }
    //            else
    //            {
    //                length += 1;
    //            }
    //        }

    //        return length;
    //    }

    //    public static string PadRightForMixedText(string str, int totalLength)
    //    {
    //        int currentLength = GetPrintableLength(str);
    //        int padding = totalLength - currentLength;
    //        return str.PadRight(str.Length + padding);
    //    }
    //}



    internal class Program
    {
        static Character _player = new Character("Chad", "전사", 1, 10, 5, 10, 1500);
        //private static Item[] _items;

        static void Main(string[] args)
        {
            GameDataSetting();
            //PrintStartLogo();
            startMenu();

        }


        private static void GameDataSetting()
        {
            _player = new Character("Chad", "전사", 1, 10, 5, 10, 1500);
            //_items = new Item[10];

            //AddItem(new Item("무쇠 갑옷", "무쇠로 만들어져 튼튼한 갑옷입니다.", 0, 0, 5, 0));
            //AddItem(new Item("낡은 검", "쉽게 볼 수 있는 낡은 검입니다.", 1, 2, 0, 0));

        }

        //static void AddItem(Item item)
        //{
        //    if (Item.ItemCnt == 10)
        //    {
        //        return;
        //    }

        //    _items[Item.ItemCnt] = item;
        //    Item.ItemCnt++;
        //}

        //private static void PrintStartLogo()
        //{
        //    Console.WriteLine("====================================================================");
        //    Console.WriteLine("                                __                     \r\n");
        //    Console.WriteLine("  ____________ _____  _______ _/  |_ _____             \r\n");
        //    Console.WriteLine(" /  ___/\\____ \\\\__  \\ \\_  __ \\\\   __\\\\__  \\            \r\n");
        //    Console.WriteLine(" \\___ \\ |  |_> >/ __ \\_|  | \\/ |  |   / __ \\_          \r\n");
        //    Console.WriteLine("/____  >|   __/(____  /|__|    |__|  (____  /          \r\n");
        //    Console.WriteLine("     \\/ |__|        \\/                    \\/           \r\n");
        //    Console.WriteLine("                                                       \r\n");
        //    Console.WriteLine("    .___                 ____                          \r\n");
        //    Console.WriteLine("  __| _/__ __   ____    / ___\\   ____   ____    ____   \r\n");
        //    Console.WriteLine(" / __ ||  |  \\ /    \\  / /_/  >_/ __ \\ /  _ \\  /    \\  \r\n");
        //    Console.WriteLine("/ /_/ ||  |  /|   |  \\ \\___  / \\  ___/(  <_> )|   |  \\ \r\n");
        //    Console.WriteLine("\\____ ||____/ |___|  //_____/   \\___  >\\____/ |___|  / \r\n");
        //    Console.WriteLine("     \\/            \\/               \\/             \\/  \r\n");
        //    Console.WriteLine("                                                       ");
        //    Console.WriteLine("====================================================================");
        //    Console.WriteLine("                        Press AnyKey To Start                       ");
        //    Console.WriteLine("====================================================================");
        //    Console.ReadKey();

        //}

        static void startMenu()
        {
            Console.Clear();
            Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
            Console.WriteLine("이제 전투를 시작할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("1. 상태 보기");
            Console.WriteLine("2. 전투 시작");
            Console.WriteLine();

            switch (CheckValidInput(1, 2))
            {
                case 1:
                    StatusMenu();
                    break;
                case 2:
                    BattleStart();
                    break;
            }


        }


        //스타트메뉴에서 1번눌렀을 시
        static void BattleStart()
        {
            Console.Clear();
            Console.WriteLine("Battle!");
            Console.WriteLine("");
            EnemySpawn();
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("[내 정보]");
            Console.WriteLine("Lv: {0}", _player.Level);
            Console.WriteLine("{0} ( {1} )", _player.Name, _player.Job);
            PrintTextWithHighlights("체력 : ", _player.Hp.ToString());
            Console.WriteLine();
            Console.WriteLine("1. 공격");
            Console.WriteLine();

            switch (CheckValidInput(1, 1))
            {
                case 1:
                    AttackMenu();
                    break;
            }
        }

        static enemy a = new enemy(2, "Minion", 15, 5);
        static enemy b = new enemy(3, "EmptyBug", 10, 9);
        static enemy c = new enemy(5, "Cannonminion", 25, 8);
        //적클래스 생성
        public class enemy
        {
            public string Ename { get; set; }
            public int Elevel { get; set; }
            public int Ehp { get; set; }
            public int Ead { get; set; }

            public bool Exist { get; set; }
            public enemy(int elevel, string ename, int ehp, int ead, bool exist=true)
            {
                Elevel = elevel;
                Ename = ename;
                Ehp = ehp;
                Ead = ead;
                Exist = exist;
            }

            public void EInfo()
            {
                if (Exist == true)
                {
                    Console.WriteLine($"Lv. {Elevel}  {Ename}  Hp: {Ehp}  Ad: {Ead}");
                }
                else
                {
                    Console.WriteLine($"Lv. {Elevel}  {Ename}  Dead");
                }

            }

        }

        static enemy[] enemies = new enemy[3]; // enemy 객체를 저장할 배열
        static Random rand = new Random();
        static int rnum = rand.Next(1, 5);
        //적생성
        static void EnemySpawn()
        {
           
            for (int i = 0; i < rnum; i++)
            {
                int num = rand.Next(0, 3);

                if (num == 0)
                {
                    enemies[i] = new enemy(2, "Minion", 15, 5);
                }
                else if (num == 1)
                {
                    enemies[i] = new enemy(3, "EmptyBug", 10, 9);
                }
                else
                {
                    enemies[i] = new enemy(5, "Cannonminion", 25, 8);
                }
            }

             // 배열에 저장된 적들을 출력
            for (int i = 0; i < rnum; i++)
            {
                enemies[i].EInfo();
            }    
        }

        static void AttackMenu()
        {
            Console.Clear();
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Battle!");
                Console.WriteLine("");

                // 배열에 저장된 적들을 출력
                for (int i = 0; i < rnum; i++)
                {
                    Console.Write("{0}. ", i + 1);
                    enemies[i].EInfo();
                }

                Console.WriteLine("");
                Console.WriteLine("[내 정보]");
                Console.WriteLine("Lv: {0}", _player.Level);
                Console.WriteLine("{0} ( {1} )", _player.Name, _player.Job);
                PrintTextWithHighlights("체력 : ", _player.Hp.ToString());

                int enemychoice = CheckValidInput(0, 3);

                if (enemychoice == -1 || enemies[enemychoice - 1].Ehp <= 0)
                {
                    Console.WriteLine("잘못된 입력");
                    //continue;

                }
           
                enemy selectedEnemy = enemies[enemychoice - 1];

                int playerad = Calculateplayerad();
                selectedEnemy.Ehp -= playerad;

                Console.WriteLine("{0}을 공격하여 {1}의 데미지를 입혔습니다.", selectedEnemy.Ename, playerad);
                if (selectedEnemy.Ehp <= 0)
                {
                    selectedEnemy.Exist = false;
                    selectedEnemy.EInfo();
                }
                else
                {
                    selectedEnemy.Exist = true;
                    Console.WriteLine("{0}의 남은 체력: {1}", selectedEnemy.Ename, selectedEnemy.Ehp);
                }

                Console.WriteLine("");
                Console.WriteLine("0. 넘어가기");
                Console.WriteLine("");
                if (enemychoice == 0)
                {
                    enemyattack();
                    break;
                }
                
            }
          
        }

        //적공격
        static void enemyattack()
        {
            Console.WriteLine("Enemy attacked");
        }

        //공격력 계산
        static int Calculateplayerad()
        {
            Random rnd = new Random();
            return rnd.Next(9,12);
        }

        private static int CheckValidInput(int min, int max)
        {
            int keyInput;
            bool result;

            do
            {
                Console.WriteLine("원하시는 행동을 입력해주세요.");
                result = int.TryParse(Console.ReadLine(), out keyInput);
            } while (result == false || CheckIfValid(keyInput, min, max) == false);

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
            ShowHighlightedText("상 태  보 기");
            Console.WriteLine("캐릭터의 정보가 표기됩니다.");

            Console.WriteLine("Lv: {0}", _player.Level);
            Console.WriteLine();
            Console.WriteLine("{0} ( {1} )", _player.Name, _player.Job);

            PrintTextWithHighlights("공격력 : ", _player.Atk.ToString());

            PrintTextWithHighlights("방어력 : ", _player.Def.ToString());

            PrintTextWithHighlights("체력 : ", _player.Hp.ToString());

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
    }
}
        //private static int getSumBonusAtk()
        //{
        //    int sum = 0;
        //    for(int i = 0; i < Item.ItemCnt; i++)
        //    {
        //        if (_items[i].isEquipped)
        //        {
        //            sum += _items[i].Atk;
        //        }
        //    }
        //    return sum;
        //}

        //private static int getSumBonusDef()
        //{
        //    int sum = 0;
        //    for (int i = 0; i < Item.ItemCnt; i++)
        //    {
        //        if (_items[i].isEquipped)
        //        {
        //            sum += _items[i].Def;
        //        }
        //    }
        //    return sum;
        //}

        //private static int getSumBonusHp()
        //{
        //    int sum = 0;
        //    for (int i = 0; i < Item.ItemCnt; i++)
        //    {
        //        if (_items[i].isEquipped)
        //        {
        //            sum += _items[i].Hp;
        //        }
        //    }
        //    return sum;
        //}

        //private static void InventoryMenu()
        //{
        //    Console.Clear();

        //    ShowHighlightedText("인 벤  토 리");
        //    Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
        //    Console.WriteLine("");
        //    Console.WriteLine("[아이템 목록]");

        //    for(int i = 0; i < Item.ItemCnt; i++)
        //    {
        //        _items[i].PrintItemStatDescription();
        //    }
        //    Console.WriteLine("");
        //    Console.WriteLine("0. 나가기");
        //    Console.WriteLine("1. 장착관리");
        //    Console.WriteLine("");

        //    switch(CheckValidInput(0, 1))
        //    {
        //        case 0:
        //            startMenu();
        //            break;
        //        case 1:
        //            EquipMenu();
        //            break;
        //    }
        //}

        //private static void EquipMenu()
        //{
        //    Console.Clear();

        //    ShowHighlightedText("인 벤  토 리 - 장 착  관 리");
        //    Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
        //    Console.WriteLine("");
        //    Console.WriteLine("[아이템 목록]");

        //    for (int i = 0; i < Item.ItemCnt; i++)
        //    {
        //        _items[i].PrintItemStatDescription(true, i+1);
        //    }

        //    Console.WriteLine("");
        //    Console.WriteLine("0. 나가기");

        //    int keyInput = CheckValidInput(0, Item.ItemCnt);

        //    switch(keyInput)
        //    {
        //        case 0:
        //            InventoryMenu();
        //            break;
        //        default:
        //            ToggleEquipStatus(keyInput - 1);
        //            EquipMenu();
        //            break;
        //    }
        //}

        //private static void ToggleEquipStatus(int idx)
        //{
        //    _items[idx].isEquipped = !_items[idx].isEquipped;
        //}
      