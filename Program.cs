using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization.Configuration;

/*
 *     + - добавить элемент в множество(Set + int)
 *     + - объединение множеств
 *     * - пересечение множеств
 * int() - мощность множества
 * false - проверка на принедлежнсоть размера массива определенному диапазону
 * Методы расширения:
 * 1) Добавление запятой после каждого слова
 * 2) Удаление повторяющихся из м множества
 */

namespace Lab3
{
    class Set
    {
        public Set() { }
        public List<double> items = new List<double>();//список элементов

        public static Set operator +(Set set, double newelem)
        {
            set.items.Add(newelem);
            return set;
        }
        public static Set operator +(double newelem, Set set)
        {
            set.items.Add(newelem);
            return set;
        }
        public static Set operator +(Set set, Set set1)
        {
            Set Union = new Set();
            Union.items.AddRange(set.items);
            for (int i = 0; i < set1.items.Count; i++)
            {
                int counter = 0;
                for (int j = 0; j < set.items.Count; j++)
                {
                    if (set1.items[i] != set.items[j])
                        counter++;
                }
                if (counter == set.items.Count)
                    Union.items.Add(set1.items[i]);
            }
            Union.items.Sort();
            return Union;
        }
        public static Set operator *(Set set, Set set1)
        {
            Set Intersection = new Set();
            for (int i = 0; i < set1.items.Count; i++)
            {
                bool counter = false;
                for (int j = 0; j < set.items.Count; j++)
                {
                    if (set1.items[i] == set.items[j])
                        counter = true;
                }
                if (counter)
                    Intersection.items.Add(set1.items[i]);
            }
            Intersection.items.Sort();
            return Intersection;
        }
        
        public static explicit operator int(Set set)
        {
            return set.items.Count;
        }

        public static bool operator false(Set array)
        {
            return array.items.Count <= 15 && array.items.Count >= 1 ? true : false;
        }
        public static bool operator true(Set array)
        {
            return array.items.Count <= 15 && array.items.Count >= 7 ? true : false;
        }

        //2
        public class Owner
        {
            int id;
            string name;
            public string Name
            {
                get
                {
                    return name;
                }
                set
                {
                    name = value;
                }
            }
            string organisation;
            public Owner(int id, string name, string organisation)
            {
                this.id = id;
                this.name = name;
                this.organisation = organisation;
            }
            public void PrintOwner()
            {
                Console.WriteLine($"{id}, {name}, {organisation}");
            }
        }
        //3
        public class Date
        {
            string date = "01.10.2020";
            public void PrintDate()
            {
                Console.WriteLine("Дата создания: {0}", date);
            }

        }
        //4


        public void PrintElem()
        {
            for (int i = 0; i < items.Count; i++)
            {
                Console.WriteLine(items[i]);
            }
        }
    }
    static class StatisticOperation
    {
        public static double SumList(Set set)
        {
            return set.items.Sum();
        }
        public static double DifferenceMax_Min(Set set)
        {
            return set.items.Max() - set.items.Min();
        }
        public static double LengthList(Set set)
        {
            return set.items.Count();
        }
        public static string Dots(this string input)//запятые после слов
        {
            char[] charInput = input.ToCharArray();
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == ' ')
                    charInput[i] = ',';
            }
            return new string(charInput);
        }
        public static Set Delete(this Set set)
        {
            set.items.Sort();
            for (int i = 1; i < set.items.Count; i++)
                if (set.items[i - 1] == set.items[i])
                    set.items.RemoveAt(i);
            return set;
        }
    }

    class Program
    {
        static int CheckNumber(string size)
        {
            return Int32.TryParse(size, out int result) ? result : -1;
        }
        static void Pause()
        {
            Console.Read();
        }
        static void Main(string[] args)
        {
            Set firstset= new Set();
            Random doublerand = new Random();
            Random factorrand = new Random();
            Console.WriteLine("Введите размер множества");
            int size = CheckNumber(Console.ReadLine());
            if (size == -1)
                return;
            for(int i =0;i<size;i++)
            {
                firstset += CheckNumber(Console.ReadLine());
            }
            //firstset.PrintElem();

            Console.WriteLine("Введите размер множества");
            size = CheckNumber(Console.ReadLine());
            Set secondset = new Set();
            for (int i = 0; i < size; i++)
            {
                secondset += CheckNumber(Console.ReadLine());
            }
            //secondset.PrintElem();

            Set thirdset = new Set();
            thirdset = firstset + secondset;//объединение
            Console.WriteLine("---------------------------------------");
            thirdset.PrintElem();
            Console.WriteLine("---------------------------------------");
            Set fourthset = firstset * secondset;//пересечение
            fourthset.PrintElem();
            Console.WriteLine("Размер третьего множества = {0}", (int)thirdset);
            if(thirdset)
                Console.WriteLine("Размер списка принадлежит промежутку от 7 до 15 ");
            else
                Console.WriteLine("Размер списка не принадлежит промежутку от 7 до 15 ");
            Set.Owner owner = new Set.Owner(18765484,"Dmitry","BSTU");
            owner.PrintOwner();
            Console.WriteLine(StatisticOperation.LengthList(thirdset));//вывод длины списка
            Console.WriteLine(StatisticOperation.SumList(firstset));//вывод суммы всех элементов списка
            Console.WriteLine(StatisticOperation.DifferenceMax_Min(secondset));//разница между максимальным и минимальным значением списка
            /////////////////////////////////////////////////
            string str = "abj oll qwe fgd qweo[pfha ropewifj";
            str = str.Dots();
            Console.WriteLine(StatisticOperation.Dots(str));
            /////////////////////////////////////////////////
            Set fifthset = firstset;
            fifthset = StatisticOperation.Delete(fifthset);
            fifthset.PrintElem();
            Pause();
        }
    }
}
