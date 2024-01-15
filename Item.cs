using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5week_assignment
{
    public class Item
    {
        public enum itemType
        {
            None,
            Weapon,
            Armor,
            Restore
        }
        public string Name { get; }
        public string Description { get; }
        public itemType Type { get; }
        public int Atk { get; }
        public int Def { get; }
        public int Hp { get; }
        public int Gold {  get; }
        public bool isEquipped { get; set; }
        public bool isMerchant { get; set; }

        public Item(string name, string description, itemType type, int atk, int def, int hp, int gold, bool isEquipped = false)
        {
            Name = name;
            Description = description;
            Type = type;
            Atk = atk;
            Def = def;
            Hp = hp;
            Gold = gold;
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
                Console.Write(PadRightForMixedText(Name, 18));
            }
            else
            {
                Console.Write(PadRightForMixedText(Name, 18));
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

            Console.Write(" |   ");

            Console.Write($"{PadRightForMixedText(Gold.ToString(), 5)}Gold    ");
            

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
}

