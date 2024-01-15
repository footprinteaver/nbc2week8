using _5week_assignment;
using System.Numerics;
using static _5week_assignment.Character;

namespace _5week_assignment
{
    public class Character
    {
        public enum ClassType
        {
            None = 0,
            Knight = 1,     //전사
            Archer = 2,     // 궁수
            Mage = 3        //마법사
        }
        public string Name { get; set; }
        public string Job{ get; set; }
        public int Level { get; set; }
        public int Atk { get; set; }
        public int Def { get; set; }
        public int Hp { get; set; }

        public bool isDead { get; set; }
        public int currentHP { get; set; }
        public int Gold { get; set; }
        public int Exp { get; set; }

        public void CreatePlayer()     // 플레이어 생성
        {
            Console.WriteLine("이름을 입력해주세요.");
            Console.Write(">>");
            string input = Console.ReadLine();

            Name = input;
            Gold = 1500;
            isDead = false;
            Level = 1;
            Console.Clear();
            ChoiceClass();
            Console.Clear();
        }

        public ClassType ChoiceClass()  // 직업선택 메서드
        {
            Console.WriteLine("직업을 선택하세요!");
            Console.WriteLine("[1] 전사");
            Console.WriteLine("[2] 궁수");
            Console.WriteLine("[3] 마법사");
            ClassType choice = ClassType.None;

            switch(CheckValidInput(1,3))
            {
                case 1:
                    choice = ClassType.Knight;
                    Job = "전사";
                    Hp = 100;
                    currentHP = Hp;
                    Atk = 10;
                    Def = 20;
                    Exp = 0;
                    break;
                case 2:
                    choice = ClassType.Archer;
                    Job = "궁수";
                    Hp = 80;
                    currentHP = Hp;
                    Atk = 15;
                    Def = 15;
                    Exp = 0;
                    break;
                case 3:
                    choice = ClassType.Mage;
                    Job = "마법사";
                    Hp = 60;
                    currentHP = Hp;
                    Atk = 20;
                    Def = 8;
                    Exp = 0;
                    break;
                default:
                    Console.Clear();
                    ChoiceClass();
                    break; 

            }
            return choice;    
        }

        public void PlayerAttack(Monster monster, out int damaged)
        {
            Random rand = new Random();
            int minAtk = Atk - (int)Math.Ceiling(Atk * 0.1);
            int maxAtk = Atk + (int)Math.Ceiling(Atk * 0.1);
            int attack = rand.Next(minAtk, maxAtk + 1);
            monster.currentHp -= attack;

            damaged = attack;
        }

        private int CheckValidInput(int min, int max)
        {
            int keyInput;
            bool result;

            do
            {
                Console.WriteLine("직업 번호를 입력하세요!");
                Console.Write(">>");
                result = int.TryParse(Console.ReadLine(), out keyInput);
            } while (result == false || CheckIfValid(keyInput, min, max) == false);

            return keyInput;

        }

        private bool CheckIfValid(int keyInput, int min, int max)
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

        public void PlayerInfo()
        {
            
            Console.WriteLine($"Lv.{Level.ToString("00")} {Name} ({Job})");
            Console.WriteLine($"HP {currentHP}/{Hp}");
            Console.WriteLine($"Exp {Exp}");
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
            Type = type; //무기 : 0, 방어구 : 1, 회복 : 2
            Def = def;
            Hp = hp;
            isEquipped = isEquipped;
        }

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
                Console.Write("]  ");
                Console.Write(PadRightForMixedText(Name, 12));
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

    public class Monster
    {
        enum MonsterType
        {
            None,
            LeeHanSol,
            MonYeongOh,
            HanHyoseung,
        }

        public string Name;
        public int Hp;
        public int currentHp;
        public int Atk;
        public int Def;
        public int Level;
        public int Gold;
        public int Exp;

        public List<Item> monsterItem;
        public List<int> monsterDropRate;

        public Item monsterDropItem;
        

        public bool isDead; // 죽었니 살았니?

        public Monster()
        {
            Random rand = new Random();
            int randMonster = rand.Next(1, 4);
            switch (randMonster)
            {
                case (int)MonsterType.LeeHanSol:
                    Name = "이한솔 매니저";
                    Level = 2;
                    Hp = 10;
                    currentHp = Hp;
                    Atk = 3;
                    Def = 0;
                    Gold = 100;
                    Exp = 10;
                    monsterDropItem = DropItem((int)MonsterType.LeeHanSol);
                    break;
                case (int)MonsterType.MonYeongOh:
                    Name = "문영오 매니저";
                    Level = 3;
                    Hp = 15;
                    currentHp = Hp;
                    Atk = 6;
                    Def = 1;
                    Gold = 150;
                    Exp = 20;
                    monsterDropItem = DropItem((int)MonsterType.MonYeongOh);
                    break;
                case (int)MonsterType.HanHyoseung:
                    Name = "한효승 매니저";
                    Level = 5;
                    Hp = 25;
                    currentHp = Hp;
                    Atk = 9;
                    Def = 3;
                    Gold = 300;
                    Exp = 40;
                    monsterDropItem = DropItem((int)MonsterType.HanHyoseung);
                    break;
            }
        }

        public void MonsterInfo(bool withNumber = false, int index = 0)
        {
            Console.Write("- ");

            if (isDead)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine($"Lv.{Level.ToString("00")} {Name}  Dead");
                Console.ResetColor();
            }
            else if (withNumber)
            {
                Console.Write($"{index}  ");
                Console.WriteLine($"Lv.{Level.ToString("00")} {Name} HP {currentHp}");
            }
            else
            {
                Console.WriteLine($"Lv.{Level.ToString("00")} {Name} HP {currentHp}");
            }

        }

        public void MonsterAttack(Character character, out int damaged)
        {
            Random rand = new Random();
            int minAtk = Atk - (int)Math.Ceiling(Atk * 0.1);
            int maxAtk = Atk + (int)Math.Ceiling(Atk * 0.1);
            int attack = rand.Next(minAtk, maxAtk + 1);
            character.currentHP -= attack;

            damaged = attack;
        }

        public Item DropItem(int type)
        {
            Random rand = new Random();
            int dropRate = rand.Next(0, 100);

            int count = 0;
            int sum = 0;

            switch (type)
            {
                case (int)MonsterType.LeeHanSol:
                    monsterItem = new List<Item>
                    {
                        new Item("핸드백", "작은 크기지만 그 무엇보다 많은게 들어있습니다", 0, 4, 0, 0),
                        new Item("프랜치 코트", "방어구보단 패션 아이템 같습니다", 1, 0, 1, 0),
                        new Item("상처치료연고", "체력 10 회복", 2, 0, 0, 10),
                        null
                    };
                    monsterDropRate = new List<int>
                    {
                        20,
                        20,
                        30,
                        30
                    };
                    break;
                case (int)MonsterType.MonYeongOh:
                    monsterItem = new List<Item>
                    {
                        new Item("코딩 책", "사전에 비견되는 딱딱함과 묵직함을 지녔습니다", 0, 7, 0, 0),
                        new Item("가죽 자켓", "좋은 브랜드라 약간의 방어력을 기대해도 될 것 같습니다", 1, 0, 3, 0),
                        new Item("압박붕대", "체력 15 회복", 2, 0, 0, 15),
                        null
                    };
                    monsterDropRate = new List<int>
                    {
                        15,
                        15,
                        25,
                        45
                    };
                    break;
                case (int)MonsterType.HanHyoseung:
                    monsterItem = new List<Item>
                    {
                        new Item("마우스", "\"딸깍\"", 0, 10, 0, 0),
                        new Item("롱패딩", "전신을 감싸지만 실상은 얇은 재질입니다", 1, 0, 4, 0),
                        new Item("봉합술 키트", "체력 25 회복", 2, 0, 0, 25),
                        null
                    };
                    monsterDropRate = new List<int>
                    {
                        10,
                        10,
                        20,
                        60
                    };
                    break;
            }

            for (int i = 0; i < monsterDropRate.Count; i++)
            {
                sum += monsterDropRate[i];

                if (dropRate <= sum)
                {
                    break;
                }
                else
                {
                    count++;
                }
            }

            return monsterItem[count];
        }

    }

    internal class Program
    {
        static bool isBattle = false;

        private static Character _player = new Character();
        private static List<Monster> monsterPool = new List<Monster>();
        private static List<Item> playerInventory = new List<Item>();
        private static List<Item> merchantItem = new List<Item>();

        static void Main(string[] args)
        {
            PrintStartLogo();
            GameDataSetting();
            startMenu();
        }


        private static void GameDataSetting()
        {
            Console.Clear();
            Console.WriteLine("스파르타 마을에 오신걸 환영합니다!");
            _player.CreatePlayer();

        }

        static void AddItem(Item item)
        {
            playerInventory.Add(item);
        }

        static void AddMonster()
        {
            Random rand = new Random();
            int summonCnt = rand.Next(1, 5);
            for (int i = 0; i < summonCnt; i++)
            {
                monsterPool.Add(new Monster());
            }
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
            isBattle = false;
            monsterPool.Clear();
            AddMonster();

            Console.Clear();
            Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
            Console.WriteLine("이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("1. 상태 보기");
            Console.WriteLine("2. 인벤토리 열기");
            Console.WriteLine("3. 상점");
            Console.WriteLine("4. 전투 시작");
            Console.WriteLine();


            switch (CheckValidInput(1, 4))
            {
                case 1:
                    StatusMenu();
                    break;
                case 2:
                    InventoryMenu();
                    break;
                case 3:
                    MerchantMenu();
                    break;
                case 4:
                    BattleStart();
                    break;
            }
        }

        private static void BattleStart()
        {
            Console.Clear();
            ShowHighlightedText("Battle!!");
            Console.WriteLine();

            for (int i = 0; i < monsterPool.Count; i++)
            {
                monsterPool[i].MonsterInfo();
            }

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("[내정보]");
            _player.PlayerInfo();
            Console.WriteLine();

            Console.WriteLine("0. 도망가기");
            Console.WriteLine("1. 공격");
            Console.WriteLine("2. 아이템 사용");
            Console.WriteLine();

            switch (CheckValidInput(0, 2))
            {
                case 0:
                    startMenu();
                    break;
                case 1:
                    // 공격
                    Attack();
                    break;
                case 2:
                    isBattle = true;
                    InventoryMenu();
                    break;
            }
        }

        private static void Attack()
        {
            Console.Clear();
            ShowHighlightedText("Battle!!");
            Console.WriteLine();

            for (int i = 0; i < monsterPool.Count; i++)
            {
                monsterPool[i].MonsterInfo(true, i + 1);
            }
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("[내정보]");
            _player.PlayerInfo();

            Console.WriteLine();
            Console.WriteLine("0. 도망가기");

            int input = CheckValidInput(0, monsterPool.Count);
            switch (input)
            {
                case 0:
                    Console.Clear();
                    BattleStart();
                    break;
                case 1:
                    Console.Clear();
                    PlayerAttackResult(input - 1);
                    break;
                case 2:
                    Console.Clear();
                    PlayerAttackResult(input - 1);
                    break;
                case 3:
                    Console.Clear();
                    PlayerAttackResult(input - 1);
                    break;
                case 4:
                    Console.Clear();
                    PlayerAttackResult(input - 1);
                    break;
            }
        }

        private static void PlayerAttackResult(int input)
        {
            int damaged = 0;

            ShowHighlightedText("■ PlayerTurn ■");
            Console.WriteLine();

            _player.PlayerAttack(monsterPool[input], out damaged);

            Console.WriteLine($"{_player.Name} 의 공격!");
            Console.Write($"Lv.{monsterPool[input].Level} {monsterPool[input].Name} 를 맞췄습니다.");
            Console.WriteLine($" [데미지 : {damaged}]");

            if (monsterPool[input].currentHp <= 0)
            {
                monsterPool[input].isDead = true;
            }

            Console.WriteLine();


            Console.WriteLine();
            Console.WriteLine("0. 다음");
            Console.WriteLine();

            int inputKey = CheckValidInput(0, 0);

            if (inputKey == 0)
            {
                Console.Clear();
                MonsterTurn();
            }


        }

        private static void MonsterTurn()
        {
            int deadCount = 0;

            ShowHighlightedText("■ MonsterTurn ■");

            for (int i = 0; i < monsterPool.Count; i++)
            {
                int beforeHitHP = _player.currentHP;

                if (!monsterPool[i].isDead)
                {

                    monsterPool[i].MonsterAttack(_player, out int damaged);

                    Console.WriteLine($"Lv.{monsterPool[i].Level} {monsterPool[i].Name} 의 공격!");
                    Console.WriteLine($"{_player.Name} ({_player.Job})을(를) 맞췄습니다.  [데미지 : {damaged}]");
                    Console.WriteLine();

                    Console.WriteLine($"Lv. {_player.Level} {_player.Name} {_player.Job}");
                    Console.WriteLine($"HP {beforeHitHP} -> {_player.currentHP}");

                    Console.WriteLine();
                    Console.WriteLine("0.다음");
                    Console.WriteLine();




                    if (_player.currentHP <= 0)
                    {
                        _player.currentHP = 0;
                        _player.isDead = true;

                        if (_player.isDead)
                        {
                            Console.Clear();

                            Console.Clear();
                            ShowHighlightedText("■ You Lose :( ■");

                            Console.WriteLine();
                            Console.WriteLine($"Lv.{_player.Level} {_player.Name}");
                            Console.WriteLine($"HP {_player.Hp} -> {_player.currentHP}");

                            Console.WriteLine();
                            Console.WriteLine("0. 다음");

                            int inputKey2 = CheckValidInput(0, 0);

                            if (inputKey2 == 0)
                            {
                                isBattle = false;
                                playerInventory.Clear();
                                GameDataSetting();
                                startMenu();
                            }
                        }
                    }

                    int inputKey = CheckValidInput(0, 0);

                    if (inputKey == 0) { continue; }
                }
                else
                {
                    deadCount++;
                    if (deadCount == monsterPool.Count)
                    {
                        Console.Clear();
                        ShowHighlightedText("Battle!! - Result");
                        ShowHighlightedText("■ Victory :) ■");

                        Console.WriteLine();
                        Console.WriteLine($"던전에서 몬스터 {deadCount}마리를 잡았습니다.");

                        Console.WriteLine();
                        Console.WriteLine($"Lv.{_player.Level} {_player.Name}");
                        Console.WriteLine($"HP {_player.Hp} -> {_player.currentHP}");

                        ShowHighlightedText("[ 전리품 획득!! ]");

                        looting();

                        Console.WriteLine("0. 돌아가기");

                        int inputKey = CheckValidInput(0, 0);

                        if (inputKey == 0)
                        {
                            isBattle = false;
                            startMenu();
                        }



                    }
                }
            }

            Attack();
        }

        private static void looting()
        {
            for (int i = 0; i < monsterPool.Count; i++)
            {
                Random rand = new Random();
                int randomGold = rand.Next(-50, 51);

                Monster lootingMonster = monsterPool[i];
                int gold = lootingMonster.Gold + randomGold;

                Console.WriteLine($"exp : {monsterPool[i].Exp}");
                Console.WriteLine($"{gold}  Gold");

                if(monsterPool[i].monsterDropItem != null)
                {
                    Console.WriteLine($"{monsterPool[i].monsterDropItem.Name}");
                    AddItem(monsterPool[i].monsterDropItem);
                }
                
                Console.WriteLine();
                Console.WriteLine();

                _player.Exp += monsterPool[i].Exp;
                _player.Gold += gold;
            }

        }

        private static int CheckValidInput(int min, int max)
        {
            int keyInput;
            bool result;

            Console.WriteLine("원하시는 행동을 입력하세요");
            Console.Write(">>");

            do
            {
                result = int.TryParse(Console.ReadLine(), out keyInput);
                if (!result)
                {
                    Console.WriteLine("다시 입력하세요");
                    Console.Write(">>");
                    continue;
                }
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
                Console.WriteLine("다시 입력하세요");
                Console.Write(">>");
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

            PrintTextWithHighlights("Lv. ", _player.Level.ToString("00"));
            Console.WriteLine();
            Console.WriteLine("{0} ( {1} )", _player.Name, _player.Job);

            int[] bonusStat = getSumBonusStat();
            PrintTextWithHighlights("공격력 : ", (bonusStat[0]).ToString(), bonusStat[0] - _player.Atk > 0 ? string.Format(" (+{0})", bonusStat[0] - _player.Atk) : "");
            PrintTextWithHighlights("방어력 : ", (bonusStat[1]).ToString(), bonusStat[1] - _player.Def > 0 ? string.Format(" (+{0})", bonusStat[1] - _player.Def) : "");

            PrintTextWithHighlights("체력 : ", $"{_player.currentHP.ToString()} / {_player.Hp.ToString()}");

            PrintTextWithHighlights("골드 : ", _player.Gold.ToString());
            PrintTextWithHighlights("경험치 : ", $"{_player.Exp.ToString()}");
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
        private static int[] getSumBonusStat()
        {
            int Atk = 0;
            int Def = 0;
            for (int i = 0; i < playerInventory.Count; i++)
            {
                if (playerInventory[i].isEquipped)
                {
                    Atk += playerInventory[i].Atk;
                    Def += playerInventory[i].Def;
                }
            }

            int[] bonusStat = {_player.Atk + Atk, _player.Def + Def };

            return bonusStat;
        }


        private static void InventoryMenu()
        {
            Console.Clear();

            ShowHighlightedText("인 벤  토 리");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("[아이템 목록]");

            for (int i = 0; i < playerInventory.Count; i++)
            {
                playerInventory[i].PrintItemStatDescription(true, i + 1);
            }

            Console.WriteLine("");
            Console.WriteLine("0. 나가기");
            Console.WriteLine("1. 장착관리");
            Console.WriteLine("");

            switch (CheckValidInput(0, 1))
            {
                case 0:
                    if(isBattle == false)
                    {
                        startMenu();
                    }
                    else
                    {
                        BattleStart();
                    }
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

            for (int i = 0; i < playerInventory.Count; i++)
            {
                playerInventory[i].PrintItemStatDescription(true, i + 1);
            }

            Console.WriteLine("");
            Console.WriteLine("0. 나가기");

            int keyInput = CheckValidInput(0, playerInventory.Count);

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
            for (int i = 0; i < playerInventory.Count;i++)
            {
                if(playerInventory[i].isEquipped == true)
                {
                    if (playerInventory[i].Type == playerInventory[idx].Type && i != idx)
                    {
                        playerInventory[i].isEquipped = !playerInventory[i].isEquipped;
                    }
                }
            }

            if (playerInventory[idx].Type == 2)
            {
                _player.currentHP += playerInventory[idx].Hp;

                if(_player.currentHP > _player.Hp)
                {
                    _player.currentHP = _player.Hp;
                }

                playerInventory.RemoveAt(idx);

                return;
            }

            if (playerInventory[idx] == null)
            {
                return;
            }

            playerInventory[idx].isEquipped = !playerInventory[idx].isEquipped;

        }

        private static void MerchantMenu()
        {
            Console.Clear();

            ShowHighlightedText("상        점");
            Console.WriteLine("아이템을 판매 또는 구매할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("[아이템 목록]");

            AddMerchantItem();

            for (int i = 0; i < merchantItem.Count; i++)
            {
                merchantItem[i].PrintItemStatDescription(true, i + 1);
            }

            Console.WriteLine("");
            Console.WriteLine("0. 나가기");
            Console.WriteLine("1. 아이템 구매");
            Console.WriteLine("2. 아이템 판매");
            Console.WriteLine("");

            switch (CheckValidInput(0, 2))
            {
                case 0:
                    startMenu();
                    break;
                case 1:
                    buyMenu();
                    break;
                case 2:
                    salesMenu();
                    break;
            }
        }

        private static void AddMerchantItem()
        {
        }

        private static void buyMenu()
        {

        }

        private static void salesMenu()
        {

        }
    }

}
