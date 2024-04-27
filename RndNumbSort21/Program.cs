using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RndNumbSort21
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] possibleSizes = new int[] { 12, 24, 48, 96, 192, 384, 768 };
            while (true)
            {
                string sortType = "";
                bool sorttypeSel = false;
                int arraySize = 0;
                bool arraySizeSel = false;
                Console.Clear();
                Console.WriteLine("Задание №2 для технологии программирования, вариант - 21 , выполнил студент гр. Усольцев Илья Алексеевич");
                Console.WriteLine("Выберите тип сортировки: 1 - вставка, 2 - отбор, 3 - оба варианта (для сравнения)");
                while (!sorttypeSel)
                {
                    var sel = Console.ReadLine();
                    if(sel != "1" && sel != "2" && sel != "3")
                    {
                        sorttypeSel = false;
                        Console.WriteLine("Введенно некорректное значение для типа сортировки");
                    } else
                    {
                        sorttypeSel = true;
                        sortType = sel;
                    }
                }

                Console.WriteLine("Введите размера массива. Предлагаемые размеры массива:" + string.Join(", ", possibleSizes.Select(i => i.ToString()).ToArray()));
                while (!arraySizeSel)
                {

                    var sel = Console.ReadLine();
                    int intVal = 0;
                    bool parsed = Int32.TryParse(sel, out intVal);
                    if (parsed)
                    {
                        arraySize = intVal;
                        arraySizeSel = true;
                    } else
                    {
                        arraySizeSel = false;
                        Console.WriteLine("Введенное некорректное значение для размера массива");
                    }
  
                }
                var rnd = new Random();
                var array1 = new int[arraySize];
                for (int i = 0; i < arraySize; i++)
                {
                    array1[i] = rnd.Next(-100, 100); //Максимальное и минимальное значение в массиве
                }
                Console.WriteLine("Ваш массив:");
                Console.WriteLine(buildTwoColOutputTable(array1));
                if (sortType == "1") //Вставка
                {
                    var insertion = InsertionSort(array1);
                    Console.WriteLine("Отсортированный массив методом вставки:");
                    Console.WriteLine(buildTwoColOutputTable(insertion.Item1));
                    Console.WriteLine(string.Format("Метод вставки: Кол-во сравнений: {0}, кол-во вставок {1}", insertion.Item2, insertion.Item3));
                } else if (sortType == "2") //отбор
                {
                    var selection = SelectionSort(array1);
                    Console.WriteLine("Отсортированный массив методом отбора:");
                    Console.WriteLine(buildTwoColOutputTable(selection.Item1));
                    Console.WriteLine(string.Format("Метод отбора: Кол-во сравнений: {0}, кол-во вставок {1}", selection.Item2, selection.Item3));
                } else //оба варианта
                {
                    var insertion = InsertionSort(array1);
                    Console.WriteLine("Отсортированный массив методом вставки:");
                    Console.WriteLine(buildTwoColOutputTable(insertion.Item1));
                    var selection = SelectionSort(array1);
                    Console.WriteLine("Отсортированный массив методом отбора:");
                    Console.WriteLine(buildTwoColOutputTable(selection.Item1));
                    Console.WriteLine(string.Format("Метод вставки: Кол-во сравнений: {0}, кол-во вставок {1}", insertion.Item2, insertion.Item3));
                    Console.WriteLine(string.Format("Метод отбора: Кол-во сравнений: {0}, кол-во вставок {1}", selection.Item2, selection.Item3));
                }
                


                Console.ReadKey();

                

            }
        }

        public static (int[], int, int) InsertionSort(int[] inputArray)
        {
            var array = new int[inputArray.Length];
            inputArray.CopyTo(array, 0);
            int compareCount = 0; 
            int swapCount = 0;

            for (int i = 1; i < array.Length; i++)
            {
                int key = array[i];
                int j = i - 1;
                compareCount++; //Счетчик сравнений
                while (j >= 0 && array[j] > key)
                {
                    array[j + 1] = array[j];
                    j--;
                    compareCount++; //Счетчик сравнений
                    swapCount++; //счетчик перестановок
                }
                array[j + 1] = key;
            }
            return (array, compareCount, swapCount);

            
        }
        public static (int[], int, int) SelectionSort(int[] inputArray)
        {
            var array = new int[inputArray.Length];
            inputArray.CopyTo(array, 0);
            int compareCount = 0;
            int swapCount = 0;
            for (int i = 0; i < array.Length - 1; i++)
            {
                int minIdx = i;
                for (int j = i + 1; j < array.Length; j++)
                {
                    compareCount++; //Счетчик сравнений
                    if (array[j] < array[minIdx])
                    {
                        minIdx = j;
                    }
                }
                if (minIdx != i)
                {
                    int temp = array[i];
                    array[i] = array[minIdx];
                    array[minIdx] = temp;
                    swapCount++; //счетчик перестановок

                }
            }
            return (array, compareCount, swapCount);
        }


        public static string buildTwoColOutputTable(int[] values)
        {
            int maxSymbolsFirst = values.Length.ToString().Length;
            int maxSymbolsSecon = (values.Max().ToString().Length > values.Min().ToString().Length ? values.Max().ToString().Length : values.Min().ToString().Length);

            string output = "";

            for (int i = 0; i < values.Length; i++)
            {
                int firstColumnSymbols = maxSymbolsFirst - (i + 1).ToString().Length;
                int secondColumnSymbols = maxSymbolsSecon - values[i].ToString().Length;
                output += "│" + (i + 1).ToString() + (firstColumnSymbols != 0 ? new String(' ', firstColumnSymbols) : "") + "│" + values[i].ToString() + (secondColumnSymbols != 0 ? new String(' ', secondColumnSymbols) : "") + "│" + "\n";
            }
            return output;
        }
    }
}
