using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using static System.Net.Mime.MediaTypeNames;

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
        public int Mp { get; set; }//마나 추가

        public bool isDead { get; set; }
        public int currentHP { get; set; }
        public int currentMP { get; set; }
        public int Gold { get; set; }

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
                    Mp = 30;
                    currentHP = Hp;
                    currentMP = Mp;
                    Atk = 10;
                    Def = 20;
                    break;
                case 2:
                    choice = ClassType.Archer;
                    Job = "궁수";
                    Hp = 80;
                    Mp = 40;
                    currentHP = Hp;
                    currentMP = Mp;
                    Atk = 15;
                    Def = 15;
                    break;
                case 3:
                    choice = ClassType.Mage;
                    Job = "마법사";
                    Hp = 60;
                    Mp = 80;                  
                    currentHP = Hp;
                    currentMP = Mp;
                    Atk = 20;
                    Def = 8;
                    break;
                default:
                    Console.Clear();
                    ChoiceClass();
                    break;

            }
            return choice;    
        }

        public void PlayerAttack(Monster monster, out int damaged, double skillDmg)
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
            Console.WriteLine($"HP {Hp}/{currentHP}");
            Console.WriteLine($"MP {Mp}/{currentMP}");
        }
        //public List<playerSkill> jobSkills = new List<playerSkill>();

        //public void GetSkills()
        //{
        //    // 각 직업에 따른 스킬 조건을 만족하는 경우에만 스킬을 리스트에 추가
        //    if (Class == ClassType.Knight)
        //    {
        //        jobSkills.Add(new KnightSkill());
        //    }
        //    else if (Class == ClassType.Archer)
        //    {
        //        jobSkills.Add(new ArcherSkill());
        //    }
        //    else if (Class == ClassType.Mage)
        //    {
        //        jobSkills.Add(new MageSkill());
        //    }
        //    else { 
        //    }
        //} 쿨래스별 스킬구현은 잠시 보류
    }

    public class playerSkill
    {
        enum Type
        {
            none = 0,
            Attack = 1,
            Heal = 2,
            Buff = 3,
        }
        public int SkillNum { get; set; }
        public string Name { get; set; }
        public int Cost { get; set; }
        public double SkillDmg { get; set; } 
        public string Description { get; set; }
        public bool IsAbailable { get; set; }

        public playerSkill( string name, int cost, string description, double skillDmg)
        {
            Name = name;
            Cost = cost;
            Description = description;
            SkillDmg = skillDmg;
        }


        public void SkillInfo(bool skillNum = false, int index = 0)
        {
            Console.Write("- ");

            if (skillNum)
            {
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.Write($"{index} ");
                Console.ResetColor();
            }

            Console.WriteLine($"{Name} mp.{Cost}");
            Console.WriteLine($"{Description} 위력보정:{SkillDmg}");
        }

    }

    #region 아이템
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

    //        if (withNumber)
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

    //        if (Atk != 0)
    //        {
    //            Console.Write($"Atk {(Atk >= 0 ? "+" : "")}{Atk}");
    //        }
    //        if (Def != 0)
    //        {
    //            Console.Write($"Def {(Def >= 0 ? "+" : "")}{Def}");
    //        }
    //        if (Hp != 0)
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
    #endregion

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

        public bool isDead; // 죽었니 살았니?

        
        public Monster()
        {
            Random rand = new Random();
            int randMonster = rand.Next(1,4);
            switch(randMonster)
            {
                case (int)MonsterType.LeeHanSol:
                    Name = "이한솔 매니저";
                    Level = 2;
                    Hp = 10;
                    currentHp = Hp;
                    Atk = 3;
                    Def = 0;
                    break;
                case (int)MonsterType.MonYeongOh:
                    Name = "문영오 매니저";
                    Level = 3;
                    Hp = 15;
                    currentHp = Hp;
                    Atk = 6;
                    Def = 1;
                    break;
                case (int)MonsterType.HanHyoseung:
                    Name = "한효승 매니저";
                    Level = 5;
                    Hp = 25;
                    currentHp = Hp;
                    Atk = 9;
                    Def = 3;
                    break;
            }

        }

        public void MonsterInfo(bool withNumber = false, int index = 0)
        {
            Console.Write("- ");
            
            if(isDead)
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

        public void MonsterAttack(Character character,out int damaged)
        {
            Random rand = new Random();
            int minAtk = Atk - (int)Math.Ceiling(Atk * 0.1);
            int maxAtk = Atk + (int)Math.Ceiling(Atk * 0.1);
            int attack = rand.Next(minAtk, maxAtk + 1);
            character.currentHP -= attack;

            damaged = attack;
        }

    }

    internal class Program
    {
        private static Character _player = new Character();
        private static List<Monster> monsterPool = new List<Monster>();
        private static List<playerSkill> skillPool = new List<playerSkill>();

        static void Main(string[] args)
        {
            PrintStartLogo();
            GameDataSetting();
            startMenu();

        }


        private static void GameDataSetting()
        {
            Console.Clear();
            AddMonster();
            GetSkill();
            Console.WriteLine("스파르타 마을에 오신걸 환영합니다!");
            _player.CreatePlayer();
            

        }

        #region 아이템 추가 예전코드
        //static void AddItem(Item item)
        //{
        //    if (Item.ItemCnt == 10)
        //    {
        //        return;
        //    }

        //    _items[Item.ItemCnt] = item;
        //    Item.ItemCnt++;
        //}
        #endregion

        static void AddMonster()
        {
            Random rand = new Random();
            int summonCnt = rand.Next(1, 5);
            for(int i = 0;  i < summonCnt; i++)
            {
                monsterPool.Add(new Monster());
            }
           
        }
        static void GetSkill()
        {
            skillPool = new List<playerSkill>();
            {
                skillPool.Add(new playerSkill("강화공격", 15, "직업의 특징을 살려 공격합니다",2));
                skillPool.Add(new playerSkill("더블어택", 20, "무작위로 두번 공격합니다", 1.5));
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
            Console.Clear();
            Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
            Console.WriteLine("이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.");
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

        private static void BattleStart()
        {
            Console.Clear();
            ShowHighlightedText("Battle!!");
            Console.WriteLine();

            for(int i = 0; i < monsterPool.Count; i++)
            {
                monsterPool[i].MonsterInfo();
            }

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("[내정보]");
            _player.PlayerInfo();
            Console.WriteLine();

            Console.WriteLine("1. 공격");
            Console.WriteLine("2. 스킬");

            switch(CheckValidInput(1,2))
            {
                case 1:
                    // 공격
                    Attack();
                    break;
                case 2:                
                    Skill();
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
                monsterPool[i].MonsterInfo(true,i+1);
            }
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("[내정보]");
            _player.PlayerInfo();

            Console.WriteLine();
            Console.WriteLine("0. 취소");

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

            _player.PlayerAttack(monsterPool[input],out damaged, input);

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

        private static void Skill()
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
            Console.WriteLine("0. 취소");
            for (int i = 0; i < skillPool.Count; i++)
            {
                skillPool[i].SkillInfo(true, i + 1);
            }
            int input = CheckValidInput(0, skillPool.Count);

            if (input == 0)
            {
                Console.Clear();
                BattleStart();
            }
            else
            {
                // 필요한 MP를 확인
                int requiredMP = skillPool[input - 1].Cost;

                // MP 체크
                if (_player.currentMP < requiredMP)
                {
                    Console.WriteLine("MP가 부족하여 스킬을 사용할 수 없습니다.");
                    Console.WriteLine("아무키나 눌러 돌아갑니다");
                    // 사용자가 확인하기 전까지 대기
                    Console.ReadKey();
                    Console.Clear();
                    Skill(); // 스킬 선택으로 돌아감
                }
                else
                {
                    UseSkill(skillPool[input - 1]);
                }
            }

        }

        private static void UseSkill(playerSkill skill)
        {   
                Console.Clear();
                ShowHighlightedText("■ PlayerTurn ■");
                Console.WriteLine();
                // MP 차감
                _player.currentMP -= skill.Cost;
                Console.WriteLine($"{_player.Name} 의 {skill.Name}! ");
                int damaged = 0;

                // 스킬에 따라 데미지 계산
                _player.PlayerAttack(monsterPool[0], out damaged, skill.SkillDmg);
                Console.Write($"Lv.{monsterPool[0].Level} {monsterPool[0].Name} 를 맞췄습니다.");
                Console.WriteLine($" [데미지 : {damaged}]");
                if (monsterPool[0].currentHp <= 0)
                {
                monsterPool[0].isDead = true;
                }
            Console.WriteLine();
            Console.WriteLine("0. 다음");

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

                    monsterPool[i].MonsterAttack(_player,out int damaged);

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

                        if(_player.isDead)
                        {
                            Console.Clear();

                            Console.Clear();
                            ShowHighlightedText("■ 전투에 패배하였습니다 ■");

                            Console.WriteLine();
                            Console.WriteLine($"Lv.{_player.Level} {_player.Name}");
                            Console.WriteLine($"HP {_player.Hp} -> {_player.currentHP}");

                            Console.WriteLine();
                            Console.WriteLine("0. 다음");

                            int inputKey2 = CheckValidInput(0, 0);

                            if (inputKey2 == 0)
                            {
                                monsterPool.Clear();
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
                    if(deadCount == monsterPool.Count)
                    {
                        Console.Clear();
                        ShowHighlightedText("■ Victory :) ■");

                        Console.WriteLine();
                        Console.WriteLine($"던전에서 몬스터 {deadCount}마리를 잡았습니다.");

                        Console.WriteLine();
                        Console.WriteLine($"Lv.{_player.Level} {_player.Name}");
                        Console.WriteLine($"HP {_player.Hp} -> {_player.currentHP}");

                        Console.WriteLine();
                        Console.WriteLine("0. 다음");

                        int inputKey = CheckValidInput(0, 0);

                        if (inputKey == 0)
                        {
                            monsterPool.Clear();
                            AddMonster();
                            startMenu();
                        }
                    }
                }
            }

            Attack();
        }


        private static int CheckValidInput(int min, int max)
        {
            int keyInput;
            bool result;

            #region 이전코드
            //do
            //{
            //    Console.WriteLine("원하시는 행동을 입력해주세요.");
            //    result = int.TryParse(Console.ReadLine(), out keyInput);
            //} while (result == false || CheckIfValid(keyInput, min, max) == false);
            #endregion

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

            PrintTextWithHighlights("공격력 : ", $"{_player.Atk.ToString()}");
            PrintTextWithHighlights("방어력 : ", $"{_player.Def.ToString()}");
            PrintTextWithHighlights("HP : ", $"{_player.Hp.ToString()}");
            PrintTextWithHighlights("MP : ", $"{_player.Mp.ToString()}");

            #region 예전 코드
            //int bonusAtk = getSumBonusAtk();
            //PrintTextWithHighlights("공격력 : ", (_player.Atk + bonusAtk).ToString(), bonusAtk > 0 ? string.Format(" (+{0})", bonusAtk) : "");

            //int bonusDef = getSumBonusDef();
            //PrintTextWithHighlights("방어력 : ", (_player.Def + bonusDef).ToString(), bonusDef > 0 ? string.Format(" (+{0})", bonusDef) : "");

            //int bonusHp = getSumBonusHp();
            //PrintTextWithHighlights("체력 : ", (_player.Hp + bonusHp).ToString(), bonusHp > 0 ? string.Format(" (+{0})", bonusHp) : "");
            #endregion


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
        #region 예전 코드
        //private static int getSumBonusAtk()
        //{
        //    int sum = 0;
        //    for (int i = 0; i < Item.ItemCnt; i++)
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

        //    for (int i = 0; i < Item.ItemCnt; i++)
        //    {
        //        _items[i].PrintItemStatDescription();
        //    }
        //    Console.WriteLine("");
        //    Console.WriteLine("0. 나가기");
        //    Console.WriteLine("1. 장착관리");
        //    Console.WriteLine("");

        //    switch (CheckValidInput(0, 1))
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
        //        _items[i].PrintItemStatDescription(true, i + 1);
        //    }

        //    Console.WriteLine("");
        //    Console.WriteLine("0. 나가기");

        //    int keyInput = CheckValidInput(0, Item.ItemCnt);

        //    switch (keyInput)
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
        #endregion
    }




}
