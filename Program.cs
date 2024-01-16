using System;
using System.Runtime.ExceptionServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using static _5week_assignment.Character;

namespace _5week_assignment
{
    internal class Program
    {
        static bool isBattle = false;

        private static Character _player = new Character();
        private static List<Monster> monsterPool = new List<Monster>();
        private static List<Item> playerInventory = new List<Item>();
        private static List<Item> merchantItem = new List<Item>();
        public static int currentStage;

        static void Main(string[] args)
        {
            PrintStartLogo();
            GameDataSetting();
            startMenu();

        }


        private static void GameDataSetting()
        {
            Console.Clear();
            currentStage = 1;           
            AddMonster();
            Console.WriteLine("스파르타 마을에 오신걸 환영합니다!");
            _player.CreatePlayer();
            
        }


        static void AddItem(Item item)  // 플레이어 아이템 획득
        {
            playerInventory.Add(item);
        }

        static void AddMonster()
        {
            Random rand = new Random();
            int summonCnt;


            if (currentStage <= 1)
            {
                summonCnt = rand.Next(1, 5);            // 1 ~ 5단계에선 1마리 ~ 4마리까지의 몬스터가 등장하게끔
                for (int i = 0; i < summonCnt; i++)
                {
                    monsterPool.Add(new Monster(currentStage));
                }
            }
            else if (currentStage <= 2)
            {
                summonCnt = rand.Next(2, 5);            // 6 ~ 10단계에서 1마리 ~ 5마리까지의 몬스터가 등장하게끔
                for (int i = 0; i < summonCnt; i++)
                {
                    monsterPool.Add(new Monster(currentStage));
                }
            }
            else if(currentStage <= 3)
            {
                summonCnt = rand.Next(2, 7);            // 7~ 단계에서 2마리 ~ 5마리의 몬스터가 등장하게끔
                for (int i = 0; i < summonCnt; i++)
                {
                    monsterPool.Add(new Monster(currentStage));
                }
            }
            else
            {
                summonCnt = rand.Next(3, 10);
                for(int i = 0; i<summonCnt; i++)
                {
                    monsterPool.Add(new Monster(currentStage));
                }
            }
           
        }
        
        private static void PrintStartLogo() //첫 시작 화면
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

            Console.Clear();
            Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
            Console.WriteLine("이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("1. 상태 보기");
            Console.WriteLine("2. 인벤토리");
            Console.WriteLine("3. 상점 방문");
            Console.Write("4. 전투 시작");
            Console.WriteLine($" (현재 진행 : {currentStage}층)");
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
                    merchantMenu();
                    break;
                case 4:
                    BattleStart();
                    break;
            }


        }

        private static void BattleStart() //전투 시작
        {
            Console.Clear();
            ShowHighlightedText("Battle!!");
            Console.WriteLine();
            Console.WriteLine($"Stage : {currentStage}");
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
            Console.WriteLine();

            Console.WriteLine("2. 인벤토리");
            Console.WriteLine();

            Console.WriteLine("0. 도망치기");
            Console.WriteLine();
            switch (CheckValidInput(0,2))
            {
                case 0:
                    startMenu(); // 도망치기
                    break;
                case 1:
                    // 공격
                    Attack();
                    break;
                case 2:
                    // 인벤토리
                    isBattle = true;
                    InventoryMenu();
                    break;
            }
        }

        private static void Attack() //플레이어 공격 선택
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

            First:                                                      //goto :  First
            int input = CheckValidInput(0, monsterPool.Count);
            
            switch (input)
            {
                case 0:
                    Console.Clear();
                    BattleStart();
                    break;
                case 1:
                    if (monsterPool[input - 1].isDead)                  // 내가 고른 번호의 몬스터가 이미 죽은 몬스터라면?
                    {
                        Console.WriteLine("잘못된 입력입니다.");
                        Console.WriteLine();
                        goto First;                                     // First로 돌아가 input값을 다시 받는다.
                    }
                    Console.Clear();
                    PlayerAttackResult(input - 1);
                    break;
                case 2:
                    Console.Clear();
                    if (monsterPool[input - 1].isDead)
                    {
                        Console.WriteLine("잘못된 입력입니다.");
                        Console.WriteLine();
                        goto First;
                    }
                    PlayerAttackResult(input - 1);
                    break;
                case 3:
                    Console.Clear();
                    if (monsterPool[input - 1].isDead)
                    {
                        Console.WriteLine("잘못된 입력입니다.");
                        Console.WriteLine();
                        goto First;
                    }
                    PlayerAttackResult(input - 1);
                    break;
                case 4:
                    Console.Clear();
                    if (monsterPool[input - 1].isDead)              
                    {
                        Console.WriteLine("잘못된 입력입니다.");
                        Console.WriteLine();
                        goto First;
                    }
                    PlayerAttackResult(input - 1);
                    break;
            }
        }

        private static void PlayerAttackResult(int input) //플레이어 공격 결과
        {
            int damaged = 0;

            ShowHighlightedText("■ PlayerTurn ■");
            Console.WriteLine();

            _player.PlayerAttack(monsterPool[input],out damaged);
            Console.WriteLine($"{_player.Name} 의 공격!");

            if(damaged == 0)
            {
                Console.WriteLine($"{monsterPool[input].Name} 을 공격했지만 회피하였습니다.");
            }
            else
            {
                Console.Write($"Lv.{monsterPool[input].Level} {monsterPool[input].Name} 를 맞췄습니다.");
                Console.WriteLine($" [데미지 : {damaged}]");
            }
            
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

        private static void MonsterTurn() //몬스터 공격
        {
            int deadCount = 0;

            ShowHighlightedText("■ MonsterTurn ■");
            Console.WriteLine();

            for (int i = 0; i < monsterPool.Count; i++)
            {
                int beforeHitHP = _player.currentHP;

                if (!monsterPool[i].isDead)
                {
                    monsterPool[i].MonsterAttack(_player,out int damaged);
                    Console.WriteLine($"Lv.{monsterPool[i].Level} {monsterPool[i].Name} 의 공격!");

                    if(damaged ==0)
                    {
                        Console.WriteLine($"{_player.Name} 을 공격했지만 회피하였습니다.");
                    }
                    else
                    {
                        Console.WriteLine($"{_player.Name} ({_player.Job})을(를) 맞췄습니다.  [데미지 : {damaged}]");
                    }
                    
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
                            ShowHighlightedText("■ You Lose :( ■");

                            Console.WriteLine();
                            Console.WriteLine($"Lv.{_player.Level} {_player.Name}");
                            Console.WriteLine($"HP {_player.Hp} -> {_player.currentHP}");

                            Console.WriteLine();
                            Console.WriteLine("0. 다음");

                            int inputKey2 = CheckValidInput(0, 0);

                            if (inputKey2 == 0)
                            {
                                monsterPool.Clear();
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
                        ShowHighlightedText("Stage Clear");
                        Console.WriteLine($"Stage : {currentStage} -> {currentStage + 1}");
                        currentStage++;

                        Console.WriteLine();
                        ShowHighlightedText("[ 전리품 획득 !! ]");

                        looting();

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

            BattleStart();
        }

         public static int ExperienceRequiredForLevelUp(int Level)
        {
            int arithmetic = 15;
            int commonDifference = 5;

            int _playerNeededExp = 10;

            for(int i = 2; i <= Level; i++)
            {
                _playerNeededExp += arithmetic + (commonDifference * (Level - 1));
            }
            
            return _playerNeededExp;
        }

        private static void looting() //아이템 루팅 함수
        {
            for (int i = 0; i < monsterPool.Count; i++)
            {
                Random rand = new Random();
                int randomGold = rand.Next(-50, 51);

                Monster lootingMonster = monsterPool[i];
                int gold = lootingMonster.Gold + randomGold;

                Console.WriteLine($"exp : {monsterPool[i].Exp}");
                Console.WriteLine($"{monsterPool[i].Gold}  Gold");

                if (monsterPool[i].monsterDropItem != null)
                {
                    Console.WriteLine($"{monsterPool[i].monsterDropItem.Name}");
                    AddItem(monsterPool[i].monsterDropItem);
                }
                
                Console.WriteLine();
                Console.WriteLine();

                _player.Exp += monsterPool[i].Exp;

                _player.MaxExp = ExperienceRequiredForLevelUp(_player.Level);

                if (_player.Exp >= _player.MaxExp)
                {
                    _player.Exp -= _player.MaxExp;
                    _player.Level++;

                    switch (_player.Job)
                    {
                        case "전사":
                            _player.Hp += 20;
                            _player.currentHP = _player.Hp;
                            _player.Atk += 1;
                            _player.Def += 3;
                            break;
                        case "궁수":
                            _player.Hp += 15;
                            _player.currentHP = _player.Hp;
                            _player.Atk += 2;
                            _player.Def += 2;
                            break;
                        case "마법사":
                            _player.Hp += 10;
                            _player.currentHP = _player.Hp;
                            _player.Atk += 3;
                            _player.Def += 1;
                            break;
                    }

                    Console.WriteLine($"축하합니다! {_player.Level - 1} -> {_player.Level} 로 레벨업 하였습니다.");

                }

                _player.Gold += gold;
                
            }

            


        }

        private static void StatusMenu() //상태창 화면
        {
            Console.Clear();

            ShowHighlightedText("상 태  보 기");
            Console.WriteLine("캐릭터의 정보가 표기됩니다.");

            PrintTextWithHighlights("Lv. ", _player.Level.ToString("00"));
            Console.WriteLine();
            Console.WriteLine("{0} ( {1} )", _player.Name, _player.Job);

            int[] bonusStat = getSumBonusStat();
            PrintTextWithHighlights("공격력 : ", (_player.Atk).ToString(), bonusStat[0] + _player.Atk > 0 ? string.Format(" (+{0})", bonusStat[0]) : "");
            PrintTextWithHighlights("방어력 : ", (_player.Def).ToString(), bonusStat[1] + _player.Def > 0 ? string.Format(" (+{0})", bonusStat[1]) : "");

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
        
        private static int[] getSumBonusStat() //아이템 장착 시, 해당 아이템의 스텟 추가
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

            int[] bonusStat = { Atk, Def };

            return bonusStat;
        }


        private static void InventoryMenu() //인벤토리 화면
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

        private static void EquipMenu() //장착 관리 화면
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

        private static void ToggleEquipStatus(int idx) //아이템 장착 함수
        {
            for (int i = 0; i < playerInventory.Count; i++)
            {
                if (playerInventory[i].isEquipped == true)
                {
                    if (playerInventory[i].Type == playerInventory[idx].Type && i != idx)
                    {
                        playerInventory[i].isEquipped = !playerInventory[i].isEquipped;

                        _player.Atk -= playerInventory[i].Atk;
                        _player.Def -= playerInventory[i].Def;
                    }
                }
            }

            if (playerInventory[idx].Type == Item.itemType.Restore)
            {
                _player.currentHP += playerInventory[idx].Hp;

                if (_player.currentHP > _player.Hp)
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

            if(playerInventory[idx].isEquipped == true)
            {
                _player.Atk += playerInventory[idx].Atk;
                _player.Def += playerInventory[idx].Def;
            }
            else if(playerInventory[idx].isEquipped == false)
            {
                _player.Atk -= playerInventory[idx].Atk;
                _player.Def -= playerInventory[idx].Def;
            }
            

        }

        private static void merchantMenu() //상점 화면
        {
            Console.Clear();

            ShowHighlightedText("   상        점   ");
            Console.WriteLine("아이템을 구매하거나 판매할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine();


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
                    BuyItem();
                    break;
                case 2:
                    SalesItem();
                    break;
            }
        }

        private static void BuyItem()
        {
            Console.Clear();

            ShowHighlightedText("아이템 구매");
            Console.WriteLine("다음 아이템들을 구매할 수 있습니다.");
            Console.WriteLine("");
            Console.WriteLine("[아이템 목록]");

            merchantItem = new List<Item>
            {
                //무기류
                new Item("핸드백", "작은 크기지만 그 무엇보다 많은게 들어있습니다", Item.itemType.Weapon, 4, 0, 0, 150),
                new Item("코딩 책", "사전에 비견되는 딱딱함과 묵직함을 지녔습니다", Item.itemType.Weapon, 7, 0, 0, 250),
                new Item("마우스", "\"딸깍\"", Item.itemType.Weapon, 10, 0, 0, 350),
                new Item("고장난 키보드", "무기로 쓰기에 적합한 키보드", Item.itemType.Weapon, 15, 0, 0, 850),
                new Item("코딩 책", "사전에 비견되는 딱딱함과 묵직함을 지녔습니다", Item.itemType.Weapon, 7, 0, 0, 950),
                new Item("커피 보틀", "모서리에 맞으면 아픈 보틀", Item.itemType.Weapon, 9, 0, 0, 1050),

                //방어구류
                new Item("프랜치 코트", "방어구보단 패션 아이템 같습니다", Item.itemType.Armor, 0, 1, 0, 130),
                new Item("가죽 자켓", "좋은 브랜드라 약간의 방어력을 기대해도 될 것 같습니다", Item.itemType.Armor, 0, 3, 0, 230),
                new Item("롱패딩", "전신을 감싸지만 실상은 얇은 재질입니다", Item.itemType.Armor, 0, 4, 0, 330),
                new Item("발가락 수면양말", "굉장히 편안한 수면양말", Item.itemType.Armor, 0, 7, 0, 830),
                new Item("발가락 수면양말", "굉장히 편안한 수면양말", Item.itemType.Armor, 0, 7, 0, 930),
                new Item("발가락 수면양말", "굉장히 편안한 수면양말", Item.itemType.Armor, 0, 7, 0, 1030),

                //회복 아이템류
                new Item("상처치료연고", "체력 10 회복", Item.itemType.Restore, 0, 0, 10, 100),
                new Item("압박붕대", "체력 15 회복", Item.itemType.Restore, 0, 0, 15, 200),
                new Item("봉합술 키트", "체력 25 회복", Item.itemType.Restore, 0, 0, 25, 300),
                new Item("회복 촉진제", "체력 50 회복", Item.itemType.Restore, 0, 0, 25, 500),
                new Item("줄기 세포 배양술", "체력 80 회복", Item.itemType.Restore, 0, 0, 25, 600),
                new Item("나노 로봇", "체력 100 회복", Item.itemType.Restore, 0, 0, 25, 800)

            };

            for(int i = 0; i < merchantItem.Count; i++)
            {
                if(i % (merchantItem.Count / 3) == 0 && i / (merchantItem.Count / 3) == 0)
                {
                    Console.WriteLine(Item.itemType.Weapon);
                }
                else if(i % (merchantItem.Count / 3) == 0 && i / (merchantItem.Count / 3) == 1)
                {
                    Console.WriteLine(Item.itemType.Armor);
                }
                else if(i % (merchantItem.Count / 3) == 0 && i / (merchantItem.Count / 3) == 2)
                {
                    Console.WriteLine(Item.itemType.Restore);
                }

                merchantItem[i].PrintItemStatDescription(true, i + 1);
            }

            Console.WriteLine("0. 나가기");
            Console.WriteLine("");

            int keyInput = CheckValidInput(0, merchantItem.Count);

            switch (keyInput)
            {
                case 0:
                    merchantMenu();
                    break;
                default:
                    if (_player.Gold < merchantItem[keyInput - 1].Gold)
                    {
                        Console.WriteLine("소지금이 부족하여 구매할 수 없습니다.");
                    }
                    else
                    {
                        Console.WriteLine("성공적으로 구매하였습니다.");

                        _player.Gold -= merchantItem[keyInput - 1].Gold;
                        playerInventory.Add(merchantItem[keyInput - 1]);
                    }

                    Console.ReadKey();

                    BuyItem();
                    break;
            }
        }

        private static void SalesItem()
        {
            Console.Clear();

            ShowHighlightedText("아이템 판매");
            Console.WriteLine("다음 아이템들을 판매할 수 있습니다.");
            Console.WriteLine("");
            Console.WriteLine("[아이템 목록]");

            for (int i = 0; i < playerInventory.Count; i++)
            {
                playerInventory[i].PrintItemStatDescription(true, i + 1);
            }

            int keyInput = CheckValidInput(0, playerInventory.Count);


            Console.WriteLine("0. 나가기");
            Console.WriteLine("");

            switch (keyInput)
            {
                case 0:
                    merchantMenu();
                    break;
                default:
                    _player.Gold += (int)(playerInventory[keyInput - 1].Gold / 3);
                    playerInventory.RemoveAt(keyInput - 1);

                    SalesItem();
                    break;
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

    }
}
