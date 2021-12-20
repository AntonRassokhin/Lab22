using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lab22
{
    class Program
    {
        static int N =1000;
        static int[] array = new int[N];

        static void Main(string[] args)
        {
            Console.Write("Сколько ячеек в массиве: ");
            int N = Convert.ToInt32(Console.ReadLine());

            Action<object> action1 = new Action<object>(MakeArray);
            Task task1 = new Task(action1, N);
            Action<Task, object> action2 = new Action<Task, object>(MaxArray);
            Task task2 = task1.ContinueWith(action2, N);
            Action<Task, object> action3 = new Action<Task, object>(SumArray);
            Task task3 = task2.ContinueWith(action3, N);
            task1.Start();

            Console.WriteLine("Main закончил");
            Console.ReadKey();
        }

        static void MakeArray(object c)
        {
            Random random = new Random();
            int p = (int)c;
            for (int i = 0; i < p; i++)
            {
                array[i] = random.Next(0, 100);
                Console.Write($"{array[i],5}");
            }
            Console.WriteLine();
        }

        static void MaxArray(Task task, object b)
        {
            int max = array[0];
            int m = (int)b;
            for (int i = 0; i < m; i++)
            {
                if (array[i] > max)
                    max = array[i];
                Console.WriteLine($"Max= {max}");
                Thread.Sleep(100);
            }            
            Console.WriteLine($"Максимальное число в массиве: {max}");
        }
        static void SumArray(Task task, object a)
        {
            int sum = 0;
            int n = (int)a;
            for (int i = 0; i < n; i++)
            {
                sum += array[i];
                Console.WriteLine($"Sum= {sum}");
                Thread.Sleep(100);
            }              
            
            Console.WriteLine($"Сумма чисел массива: {sum}");
        }

    }
}
