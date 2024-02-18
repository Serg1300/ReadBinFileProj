namespace ReadBinFile
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите путь для создания папки Students");
            string path = Console.ReadLine();
            path = path + "\\Students";
            var di = new DirectoryInfo(path);
            if (!di.Exists )
            {
                di.Create();                
            }
            else
            {
                Console.WriteLine($"{di.FullName} уже создана");
            }

            Console.WriteLine("Введите путь до файла students.dat");
            string pathStData = Console.ReadLine();

            //Создание списка студентов из файла .dat
            var students = new List<Student>();
            if (File.Exists(pathStData))
            {
                using (BinaryReader reader = new BinaryReader(File.Open(pathStData, FileMode.Open)))
                {
                    while (reader.PeekChar() > -1)
                    {
                        string nameSt = reader.ReadString();
                        string groupp = reader.ReadString();
                        long dataOfBir = reader.ReadInt64();
                        decimal averageScore = reader.ReadDecimal();

                        DateTime dataBir = DateTime.FromBinary(dataOfBir);
                        students.Add(new Student(nameSt, groupp, dataBir, averageScore));
                    }
                    Console.WriteLine("Файл считан");
                }
            }
            // Создание списка групп
            var groups = new List<string>();
            foreach (var st in students)
            {
                if (!groups.Contains(st.Group))
                {
                    groups.Add(st.Group);
                }
            }
            // Запись студентов в текстовый файл
            foreach (var group in groups)
            {
                string pathFileText = path + "\\" + group + ".txt";
                FileInfo fileInfo = new FileInfo(pathFileText);
                var studentByGroup = students.Where(s => s.Group == group);
                
                if (!fileInfo.Exists)
                {
                    using (StreamWriter sw = fileInfo.CreateText())
                    {
                        sw.WriteLine("Имя        Дата Рождения        Средний балл");
                        foreach (var student in studentByGroup)
                        {
                            sw.WriteLine($"{student.Name}       {student.DateOfBirth.ToLongDateString()}       {student.AverageScore}");
                        }
                    }
                    Console.WriteLine($"В папку Students добавлен новый файл {group}.txt");
                }
                else
                {
                    using (StreamWriter sw = fileInfo.AppendText())
                    {
                        foreach (var student in studentByGroup)
                        {
                            sw.WriteLine($"{student.Name}       {student.DateOfBirth.ToLongDateString()}       {student.AverageScore}");
                        }
                    }
                    Console.WriteLine($"В файл {group}.txt добавлена новая запись");
                }
                

            }

        }
    }
}
