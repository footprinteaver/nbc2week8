﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _5week_assignment
{
    public class Monster
    {
        enum MonsterType
        {
            None,
            LeeHanSol,
            MonYeongOh,
            HanHyoseung,          // <--------------- 여기까지가 1단계에서 등장할 몬스터 목록
            LeeHanbyeol,          // <---------------- 6~10단계에 추가로 등장하는 몬스터
            KimHyunjeong,          // <---------------- 11~15단계에 추가로 등장하는 몬스터
            KimYeongHo            // <---------------- 4단계에 추가로 등장하는 몬스터
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

        public Monster(int currentStage)  // 몬스터 생성자
        {
            int randMonster = SummonByStage(currentStage);

            switch (randMonster)
            {
                case (int)MonsterType.LeeHanSol:
                    MonsterSetting("이한솔 매니저", 22, 6, 1, 1, 200, 5);
                    monsterDropItem = DropItem((int)MonsterType.LeeHanSol);
                    break;
                case (int)MonsterType.MonYeongOh:
                    MonsterSetting("문영오 매니저", 27, 8, 3, 2, 260, 7);
                    monsterDropItem = DropItem((int)MonsterType.MonYeongOh);
                    break;
                case (int)MonsterType.HanHyoseung:
                    MonsterSetting("한효승 매니저", 35, 12, 5, 3, 370, 12);
                    monsterDropItem = DropItem((int)MonsterType.HanHyoseung);
                    break;
                case (int)MonsterType.LeeHanbyeol:
                    MonsterSetting("이한솔 튜터", 50, 9, 9, 4, 600, 15);
                    monsterDropItem = DropItem((int)MonsterType.LeeHanbyeol);
                    break;
                case (int)MonsterType.KimHyunjeong:
                    MonsterSetting("김현정 튜터", 26, 20, 4, 5, 580, 17);
                    monsterDropItem = DropItem((int)MonsterType.KimHyunjeong);
                    break;
                case (int)MonsterType.KimYeongHo:
                    MonsterSetting("김영호 튜터", 52, 14, 7, 6, 720, 22);
                    monsterDropItem = DropItem((int)MonsterType.KimYeongHo);
                    break;
            }

        }
        public void MonsterInfo(bool withNumber = false, int index = 0)     // 전투가 시작됬을때 몬스터의 정보를 표시해 준다
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
            int minAtk = (int)Math.Ceiling(Atk * 0.9);     // 최소 공격력
            int maxAtk = (int)Math.Ceiling(Atk * 1.1);     // 최대 공격력
            int attack = rand.Next(minAtk, maxAtk + 1);

 
            int hit = rand.Next(1, 101);
            if (hit <= 10)
            {
                // 회피
                attack = 0;
                damaged = attack;
            }
            else if(hit <= 25)
            {
                // 치명타
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\r\n크리티컬 발동!!");
                Console.ResetColor();
                attack = (int)Math.Ceiling(attack * 1.6);
                character.currentHP -= (int)(attack * (5.0f / (character.Def + 5.0f)));
                damaged = (int)(attack * (5.0f / (character.Def + 5.0f)));
            }
            else
            {
                // 일반
                character.currentHP -= (int)(attack * (5.0f / (character.Def + 5.0f)));
                damaged = (int)(attack * (5.0f / (character.Def + 5.0f)));
            }
        }

        public Item DropItem(int type)          //몬스터 별 드롭 아이템 설정
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
                        new Item("핸드백", "작은 크기지만 그 무엇보다 많은게 들어있습니다", Item.ItemType.Weapon, 2, 0, 0, 300),
                        new Item("프랜치 코트", "방어구보단 패션 아이템 같습니다", Item.ItemType.Armor, 0, 1, 0, 200),
                        new Item("상처치료연고", "체력 10 회복", Item.ItemType.Restore, 0, 0, 10, 150),
                        null
                    };
                    monsterDropRate = new List<int>
                    {
                        30,
                        30,
                        30,
                        10
                    };
                    break;
                case (int)MonsterType.MonYeongOh:
                    monsterItem = new List<Item>
                    {
                        new Item("코딩 책", "사전에 비견되는 딱딱함과 묵직함을 지녔습니다", Item.ItemType.Weapon, 5, 0, 0, 700),
                        new Item("가죽 자켓", "좋은 브랜드라 약간의 방어력을 기대해도 될 것 같습니다", Item.ItemType.Armor, 0, 3, 0, 600),
                        new Item("압박붕대", "체력 20 회복", Item.ItemType.Restore, 0, 0, 20, 300),
                        null
                    };
                    monsterDropRate = new List<int>
                    {
                        20,
                        20,
                        25,
                        35
                    };
                    break;
                case (int)MonsterType.HanHyoseung:
                    monsterItem = new List<Item>
                    {
                        new Item("마우스", "\"딸깍\"", Item.ItemType.Weapon, 7, 0, 0, 1600),
                        new Item("롱패딩", "전신을 감싸지만 실상은 얇은 재질입니다", Item.ItemType.Armor, 0, 4, 0, 800),
                        new Item("봉합술 키트", "체력 30 회복", Item.ItemType.Restore, 0, 0, 30, 450),
                        null
                    };
                    monsterDropRate = new List<int>
                    {
                        15,
                        15,
                        20,
                        50
                    };
                    break;       
                case (int)MonsterType.LeeHanbyeol:
                    monsterItem = new List<Item>
                    {
                        new Item("고장난 키보드", "무기로 쓰기에 적합한 키보드", Item.ItemType.Weapon, 9, 0, 0, 2600),
                        new Item("발가락 수면양말", "굉장히 편안한 수면양말", Item.ItemType.Armor, 0, 5, 0, 1400),
                        new Item("회복 촉진제", "체력 50 회복", Item.ItemType.Restore, 0, 0, 50, 1000),
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
                case (int)MonsterType.KimHyunjeong:
                    monsterItem = new List<Item>
                    {
                        new Item("코딩 책", "사전에 비견되는 딱딱함과 묵직함을 지녔습니다", Item.ItemType.Weapon, 13, 0, 0, 3700),
                        new Item("탈모방지 모자", "통풍이 잘 되고 머리 건강에 좋습니다.", Item.ItemType.Armor, 0, 6, 0, 2000),
                        new Item("줄기 세포 배양술", "체력 80 회복", Item.ItemType.Restore, 0, 0, 80, 1700),
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
                case (int)MonsterType.KimYeongHo:
                    monsterItem = new List<Item>
                    {
                        new Item("커피 보틀", "모서리에 맞으면 아픈 보틀", Item.ItemType.Weapon, 17, 0, 0, 5500),
                        new Item("인형탈", "그 무엇도 내부에 침범할 수 없습니다", Item.ItemType.Armor, 0, 7, 0, 3000),
                        new Item("나노 로봇", "체력 100 회복", Item.ItemType.Restore, 0, 0, 100, 2000),
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



        private int SummonByStage(int currentStage)             // 스테이지 별 몬스터 소환메서드
        {
            Random rand = new Random();
            int randMonster;
            if (currentStage <= 3)
            {
                randMonster = rand.Next(1, 4);
            }
            else if (currentStage <= 6)
            {
                randMonster = rand.Next(2, 6);
            }
            else if (currentStage <= 10)
            {
                randMonster = rand.Next(3, 7);
            }
            else
            {
                randMonster = rand.Next(4, 7);
            }
            return randMonster;
        }

        private void MonsterSetting(string _Name, int _Hp, int _Atk, int _Def, int _Level,int _Gold,int _Exp) // 몬스터 옵션 세팅
        {
            Name = _Name;
            Hp = _Hp;
            currentHp = _Hp;
            Atk = _Atk;
            Def = _Def;
            Level = _Level;
            Gold = _Gold;
            Exp = _Exp;
        }
    }
}
