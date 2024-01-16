using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5week_assignment
{
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
        public int Repeat { get; set; }
        public bool Targeting { get; set; }
        public int SkillRange { get; set; }


        public playerSkill(string name, int cost, string description, double skillDmg, int repeat, bool targeting)
        {
            Name = name;
            Cost = cost;
            Description = description;
            SkillDmg = skillDmg;
            Repeat = repeat;
            Targeting = targeting;
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
            Console.WriteLine();
        }
    }
}

