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

            //10%확률
            int hit = rand.Next(1, 101);
            if (hit <= 10)
            {
                attack = 0;
                damaged = attack;
            }
            else
            {
                character.currentHP -= attack;
                damaged = attack;
                //치명타코드
            }
        }
    }
}
