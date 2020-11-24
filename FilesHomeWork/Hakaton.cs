using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilesHomeWork
{
    class Hakaton
    {
        /// <summary>
        /// Метод, разбивающий последовательность целых от 0 но N на группы неделимых чисел, группы в последствии записываются 
        /// в файлы во вновь созданную папку. Метод принимает в себя int N в качестве параметра, переменную типа string как путь для создания папки
        /// в которую будут сохраняться файлы, возвращает переменную типа int как количество созданных групп.
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static int SaveGroup(int n, string path)
        {
            int count = 0;
            int[] arr = new int[0];
            int counterGroup = (int)Math.Log(n, 2) + 1;

            while (counterGroup > 0)
            {
                if (n % 2 != 0) arr = new int[n / 2 + 1];
                if (n % 2 == 0) arr = new int[n / 2];

                for (int i = n / 2 + 1; i <= n; i++)
                {
                    arr[count] = i;
                    count++;
                }

                Directory.CreateDirectory($@"{path}");

                using (StreamWriter sw = new StreamWriter($@"{path}\group{counterGroup}.txt"))
                {
                    foreach (var item in arr)
                    {
                        sw.WriteLine(item);
                    }
                }

                count = 0;
                n /= 2;
                counterGroup--;
            }
            
            counterGroup = (int)Math.Log(n, 2) + 1;
            return counterGroup;
        }

        /// <summary>
        /// Метод, позволяющий архивировать данные во вновь созданную директорию.
        /// Принимает в качестве агрумента int n как количество файлов, доступных для архивации, 
        /// переменную типа string как путь к файлам, которые необоходимо сжимать,
        /// переменную типа string как путь для создания папки со сжатыми файлами
        /// </summary>
        /// <param name="n"></param>
        public static void ArchiveZip(int n, string pathRead, string pathSave)
        {
            Directory.CreateDirectory($@"{pathSave}");

            while (n > 0)
            {
                using (FileStream fs = new FileStream($@"{pathRead}\group{n}.txt", FileMode.OpenOrCreate))
                {
                    using (FileStream fsSec = File.Create($@"{pathSave}\group{n}.zip"))
                    {
                        using (GZipStream gs = new GZipStream(fsSec, CompressionMode.Compress))
                        {
                            fs.CopyTo(gs);
                        }
                    }
                }
                n--;
            }
        }
    }
}
