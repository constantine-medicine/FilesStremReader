using System;
using System.IO;

namespace FilesHomeWork
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            // Программа, считывающая из папки некоторое число Number и разбивает последовательность чисел от 0 до Number
            // на группы неделимых чисел.
           
            Console.Write("Введите путь к файлу: ");
            string pathFile = Convert.ToString(Console.ReadLine());
            string text = File.ReadAllText($@"{pathFile}");
            int number = Convert.ToInt32(text);

            Console.Write($"Полученное число: {number}");
            int countergroup = (int)Math.Log(number, 2) + 1;

            Console.WriteLine("\nВыберите режим работы: \n1 - Показать количество получившихся групп. \n2 - Получить заполненные группы в файле.");

            do
            {
                // Выбор режима работы программы
                bool worker = int.TryParse(Console.ReadLine(), out int result);

                // Первый режим работы (показать количество групп)
                if (result == 1 && worker)
                {
                    DateTime date = new DateTime();
                    date = DateTime.Now;

                    Console.Write($"\nКоличество групп равно {(int)Math.Log(number, 2) + 1}");

                    DateTime newDate = DateTime.Now;
                    TimeSpan span = newDate - date;

                    Console.Write($"\nПрограмма выполнилась за {span.TotalSeconds} секунд или {span.TotalMilliseconds} милисекунд.");

                    break;
                }

                // Второй режим работы (создает файлы и предлагает их архивировать)
                else if (result == 2 && worker)
                {
                    Console.Write("\nУкажите путь в котором будет создана папка для записи файлов: ");
                    string pathSave = Convert.ToString(Console.ReadLine());

                    DateTime date = new DateTime();
                    date = DateTime.Now;

                    Hakaton.SaveGroup(number, pathSave);

                    DateTime newDate = DateTime.Now;
                    TimeSpan span = newDate - date;

                    Console.Write($"\nПрограмма выполнилась за {span.TotalSeconds} секунд или {span.TotalMilliseconds} милисекунд.");

                    Console.Write("\nХотели бы вы заархивировать данные? д/н\n");

                    // Программа задает вопрос об архивации
                    while (true)
                    {
                        string answer = Convert.ToString(Console.ReadLine());

                        if (answer == "д")
                        {
                            Console.Write("\nУкажите путь для создания папки с файлами: ");
                            string pathZip = Convert.ToString(Console.ReadLine());

                            Hakaton.ArchiveZip(countergroup, pathSave, pathZip);
                            Console.WriteLine("\nДанные успешно сжаты.");
                            break;
                        }
                        else if (answer == "н")
                        {
                            Console.WriteLine("Конец программы.");
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Ответом должен быть 'н' или 'д'. Повторите ввод.");
                        }
                    }
                    break;
                }
                else Console.WriteLine("Чтобы выбрать режим нажмите 1 или 2. Повторите ввод.");
            } while (true);

            Console.ReadLine();
        }
    }
}