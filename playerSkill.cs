﻿using System;
using System.Collections.Generic;
using System.Data;
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

        public playerSkill(string name, int cost, string description, double skillDmg)
        {
            Name = name;
            Cost = cost;
            Description = description;
            SkillDmg = skillDmg;
        }


        public void SkillInfo(bool skillNum = false, int index = 0)         // 스킬 정보표시
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

