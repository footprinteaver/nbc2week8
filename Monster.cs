using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            #region stage기능이 추가되면 주석을 풀고 추가할 몬스터 타입
            //LeeHanbyeol,       // <---------------- 2단계에 추가로 등장하는 몬스터
            //KimHunjeong,       // <---------------- 3단계에 추가로 등장하는 몬스터
            //KimYeongHo         // <---------------- 4단계에 추가로 등장하는 몬스터
            #endregion
        }

        public string Name;
        public int Hp;
        public int currentHp;
        public int Atk;
        public int Def;
        public int Level;

        public bool isDead; // 죽었니 살았니?

        public Monster()  // 몬스터 생성자
        {
            Random rand = new Random();
            int randMonster = rand.Next(1, 4);
            switch (randMonster)
            {
                case (int)MonsterType.LeeHanSol:
                    MonsterSetting("이한솔 매니저", 10, 3, 0, 2);
                    break;
                case (int)MonsterType.MonYeongOh:
                    MonsterSetting("문영오 매니저", 15, 3, 1, 3);
                    break;
                case (int)MonsterType.HanHyoseung:
                    MonsterSetting("한효승 매니저", 25, 9, 3, 5);
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
                character.currentHP -= attack;
                damaged = attack;
            }
            else
            {
                // 일반
                character.currentHP -= attack;
                damaged = attack;
                
            }
        }

        #region 스테이지 기능이 추가되면 주석을 풀고 추가할 메서드
        private int SummonByStage(int Stage)             // 스테이지 별 캐릭터 소환메서드
        {
            //Random rand = new Random();
            //int randMonster;
            //if(Stage == 1)
            //{
            //    randMonster = rand.Next(1, 4);
            //}
            //else if(Stage == 2)
            //{
            //    randMonster = rand.Next(1, 6);
            //}
            //else if(stage == 3)
            //{
            //    randMonster = rand.Next(1, 7);
            //}
            //else
            //{
            //    randMonster = rand.Next(2, 7);
            //}
            return 0;
        }
        #endregion

        private void MonsterSetting(string _Name, int _Hp, int _Atk, int _Def, int _Level) // 몬스터 옵션 세팅 메서드
        {
            Name = _Name;
            Hp = _Hp;
            currentHp = _Hp;
            Atk = _Atk;
            Def = _Def;
            Level = _Level;
        }
    }
}
