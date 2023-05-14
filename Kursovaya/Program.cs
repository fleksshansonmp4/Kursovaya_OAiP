using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursovaya
{
    internal class Program
    {
        public static double F(double x)
        {
            return x * x - 4 * x + 3;
        }

        static void DataFromFile(string filename, ref double a, ref double b, ref double eps)
        {
            using (StreamReader reader = new StreamReader(filename))
            {
                a = double.Parse(reader.ReadLine());
                b = double.Parse(reader.ReadLine());
                eps = double.Parse(reader.ReadLine());
            }
        }

        public static double FindMinimum(double a, double b, double eps)
        {
            int maxIter = 100;
            double c, fc;

            for (int i = 0; i < maxIter; i++)
            {
                c = (a + b) / 2;
                fc = F(c);

                if (Math.Abs(b - a) < eps)
                {
                    return (a + b) / 2;
                }

                if (F(a) * fc > 0)
                {
                    a = c;
                }
                else
                {
                    b = c;
                }
            }
            return (a + b) / 2;
        }

        static void Main(string[] args)
        {
            double a = 0, b = 0, eps = 0;
            while (true)
            {
                Console.WriteLine("Выберите способ ввода данных\n" +
                    "1. Вручную.\n" +
                    "2. Из файла.\n" +
                    "0.Выход");
                switch (Console.ReadLine())
                {
                    case "1":
                        {
                            try
                            {
                                Console.Write("Введите значение a:"); a = double.Parse(Console.ReadLine());
                                Console.Write("Введите значение b:"); b = double.Parse(Console.ReadLine());
                                if (a > b)
                                {
                                    Console.WriteLine("Неверно выбран отрезок\n");
                                    break;
                                }
                                Console.Write("Введите точность вычислений:"); eps = double.Parse(Console.ReadLine());
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message + "\n");
                                break;
                            }
                            double min = FindMinimum(a, b, eps);
                            Console.WriteLine("Минимум функции f(x) = x^2 - 4x + 3 на отрезке [{0}, {1}] равен {2}\n", a, b, min);
                        }
                        break;
                    case "2":
                        {
                            Console.Write("Введите название файла:");
                            string filename = Console.ReadLine();
                            try
                            {
                                DataFromFile(filename, ref a, ref b, ref eps);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message + "\n");
                                break;
                            }
                            if (a > b)
                            {
                                Console.WriteLine("Неверно выбран отрезок");
                                break;
                            }
                            Console.WriteLine($"Данные из текстового файла:\n a:{a}\n b:{b}\n eps:{eps}\n");
                            double min = FindMinimum(a, b, eps);
                            Console.WriteLine("Минимум функции f(x) = x^2 - 4x + 3 на отрезке [{0}, {1}] равен {2}\n", a, b, min);
                        }
                        break;
                    case "0":
                        {
                            Environment.Exit(0);
                        }
                        break;
                    default:
                        Console.WriteLine("Нет такого пункта.");
                        break;
                }
            }
        }
    }
}