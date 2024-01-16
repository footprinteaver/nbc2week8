using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public string Job { get; set; }
        public int Level { get; set; }
        public int Atk { get; set; }
        public int Def { get; set; }
        public int Hp { get; set; }
        public int Mp { get; set; }//마나 추가

        public bool isDead { get; set; }
        public int currentHP { get; set; }
        public int currentMP { get; set; }
        public int Gold { get; set; }
        public int Exp { get; set; }
        public int MaxExp;

        public void CreatePlayer()     // 플레이어 생성
        {
            Console.WriteLine("이름을 입력해주세요.");
            Console.Write(">>");
            string input = Console.ReadLine();

            Name = input;
            Gold = 500;
            isDead = false;
            Level = 1;
            Exp = 0;
            MaxExp = 10;
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

            switch (CheckValidInput(1, 3))
            {
                case 1:
                    choice = ClassType.Knight;
                    Job = "전사";
                    Hp = 70;
                    Mp = 45;
                    currentHP = Hp;
                    currentMP = Mp;
                    Atk = 8;
                    Def = 10;
                    break;
                case 2:
                    choice = ClassType.Archer;
                    Job = "궁수";
                    Hp = 60;
                    Mp = 70;
                    currentHP = Hp;
                    currentMP = Mp;
                    Atk = 10;
                    Def = 8;
                    break;
                case 3:
                    choice = ClassType.Mage;
                    Job = "마법사";
                    Hp = 45;
                    Mp = 100;
                    currentHP = Hp;
                    currentMP = Mp;
                    Atk = 15;
                    Def = 6;
                    break;
                default:
                    Console.Clear();
                    ChoiceClass();
                    break;

            }
            return choice;
        }


        public void PlayerAttack(Monster monster, out int damaged)  // 플레이어 공격기능
        {
            Random rand = new Random();
            int minAtk = (int)Math.Ceiling(Atk * 0.9);
            int maxAtk = (int)Math.Ceiling(Atk * 1.1);
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
                monster.currentHp -= (int)(attack * (5.0f / (monster.Def + 5.0f)));
                damaged = (int)(attack * (5.0f / (monster.Def + 5.0f)));
            }
            else
            {
                //일반
                monster.currentHp -= (int)(attack * (5.0f / (monster.Def + 5.0f)));
                damaged = (int)(attack * (5.0f / (monster.Def + 5.0f)));
            }
        }



        private int CheckValidInput(int min, int max)  // 플레이어에서 사용하는 CheckValidInput
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

        public void PlayerInfo()        // 전투모드 돌입시 Player의 정보 표시
        {

            Console.WriteLine($"Lv.{Level.ToString("00")} {Name} ({Job})");
            Console.WriteLine($"HP {currentHP} / {Hp}");
            Console.WriteLine($"MP {currentMP} / {Mp}");
            Console.WriteLine($"Exp : {Exp} / {MaxExp}");
        }
    }
}
