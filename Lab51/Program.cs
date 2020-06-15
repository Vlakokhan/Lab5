using System;
using System.IO;
using System.Text;

namespace Lab51
{
    public class STOCK
    {
        string Company ;
        string Code;

        public string company
        {
            get { return Company; }
            set { Company = value; }
        }

        public string code
        {
            get { return Code; }
            set { Code = value; }
        }

        private string Checkdate(string date)
        {
            DateTime dateTime;
            while (!DateTime.TryParse(date, out dateTime))
            {
                Console.WriteLine("Невiрне значення");
                Console.Write("Дата(дд.мм.рр):");
                date = Console.ReadLine();
            }

            dateTime = Convert.ToDateTime(date);
            date = dateTime.ToShortDateString();
            return date;
        }

        private int Checkhours(string opencourse)
        {
            int result = 0;
            while (!int.TryParse(opencourse, out result))
            {
                Console.WriteLine("Невiрне значення");
                Console.Write("Введіть курс відкриття ще раз:");
                opencourse = Console.ReadLine();
            }
            result = Convert.ToInt32(opencourse);
            return result;
        }

        public void Add()
        {
            Console.InputEncoding = Encoding.Unicode;
            Rate add = new Rate();
            Console.WriteLine("Назву компанiї :");
            string company = Console.ReadLine();
            while (string.IsNullOrEmpty(company))
            {
                Console.WriteLine("Введіть назву компанiї ще раз:");
                company = Console.ReadLine();
            }

            add.Company = company;
            Console.WriteLine("Введіть код на бiржi:");
            string code = Console.ReadLine();
            while (string.IsNullOrEmpty(code))
            {
                Console.WriteLine("Введіть код на бiржi ще раз:");
                code = Console.ReadLine();
            }

            add.code = code;
            Console.WriteLine("Введіть дату:");
            string date = Console.ReadLine();
            add.date = Checkdate(date);
            Console.WriteLine("Введіть курс відкриття:");
            string openate = Console.ReadLine();
            add.opencourse   = Checkhours(openate);
            Console.WriteLine("Введіть курс закриття:");
            string closingrate = Console.ReadLine();
            while (string.IsNullOrEmpty(closingrate))
            {
                Console.WriteLine("Введіть курс закриття ще раз:");
                closingrate = Console.ReadLine();
            }
            add.closingrate = closingrate;
            Console.WriteLine("\nЯкщо ви бажаєте зберегти змiни то натиснiть Enter, якщо нi, то будь-яку iншу клавiшу.");

            if (Console.ReadKey().Key == ConsoleKey.Enter)
            {
                using (StreamWriter f = new StreamWriter("C:\\Users\\vladi\\RiderProjects\\Lab51\\STOCK.txt",
                    true))
                    f.WriteLine("{0,-31}{1,-16}{2,-15}{3,-5}{4,-20}", add.Company , add.code, add.date, add.opencourse,
                        add.closingrate);
                Console.WriteLine("Змiни збережено\n");
            }
            else
            {
                Console.WriteLine("\nЗмiни не збережено\n");
            }


        }

        private int Checknum(string number1)
        {
            int result = 0;
            while (!int.TryParse(number1, out result))
            {
                Console.Write("Введіть номер ще раз:");
                number1 = Console.ReadLine();
            }

            result = Convert.ToInt32(number1);
            return result;
        }

        public void Edit()
        {
            Rate edit = new Rate();
            int length = File.ReadAllLines("C:\\Users\\vladi\\RiderProjects\\Lab51\\STOCK.txt").Length;
            Console.WriteLine("Номер рядку:");
            int number = Checknum(Console.ReadLine());
            while (number > length || number <= 0)
            {
                Console.WriteLine("Номер рядку не може бути менше нуля або більше " + length );
                number = Checknum(Console.ReadLine());
            }

            string[] str = File.ReadAllLines("C:\\Users\\vladi\\RiderProjects\\Lab51\\STOCK.txt");
            string line = str[number - 1];
            edit.Company = line.Substring(0, 30);
            edit.code = line.Substring(31, 15);
            edit.date = line.Substring(47, 13);
            edit.opencourse = Convert.ToInt32(line.Substring(61, 4));
            edit.closingrate = line.Substring(67, 20);
            Console.WriteLine("Введiть номер елементу стовпчика, який потрібно змінити: ");
            int number1 = Checknum(Console.ReadLine());
            while (number1 > 5 || number1 <= 0)
            {
                Console.WriteLine("Номер стовпчика не може бути менше нуля або більше п'яти");
                number1 = Checknum(Console.ReadLine());
            }

            if (number1 == 1)
            {
                Console.WriteLine("Введіть назву компанії:");
                string company = Console.ReadLine();
                while (string.IsNullOrEmpty(company))
                {
                    Console.WriteLine("Введіть назву компанії ще раз:");
                    company = Console.ReadLine();
                }

                edit.Company = company;
            }

            if (number1 == 2)
            {
                Console.WriteLine("Введіть код:");
                string code = Console.ReadLine();
                while (string.IsNullOrEmpty(code))
                {
                    Console.WriteLine("Введіть код ще раз:");
                    code = Console.ReadLine();
                }
                edit.code = code;
            }

            if (number1 == 3)
            {
                Console.WriteLine("Введіть дату:");
                string date = Console.ReadLine();
                edit.date = Checkdate(date);
            }

            if (number1 == 4)
            {
                Console.WriteLine("Введіть курс відкриття:");
                string opencourse = Console.ReadLine();
                edit.opencourse = Checkhours( opencourse);
            }

            if (number1 == 5)
            {
                Console.WriteLine("Введіть курс закриття:");
                string closingrate = Console.ReadLine();
                while (string.IsNullOrEmpty(closingrate))
                {
                    Console.WriteLine("Введіть курс закриття:");
                    closingrate = Console.ReadLine();
                }

                edit.closingrate = closingrate;
            }

            Console.WriteLine("\nЗбереження змін - Enter, відміна - будь-яка інша клавіша.");
            if (Console.ReadKey().Key == ConsoleKey.Enter)
            {
                using (StreamWriter f = new StreamWriter("C:\\Users\\vladi\\RiderProjects\\Lab51\\STOCK.txt"))
                    for (int i = 0; i < length; i++)
                    {
                        if (i != number - 1) f.WriteLine(str[i]);
                        else
                            f.WriteLine("{0,-31}{1,-16}{2,-15}{3,-5}{4,-20}", edit.Company, edit.code, edit.date,
                                edit.opencourse, edit.closingrate);
                    }

                Console.WriteLine("Змiни збережено\n");
            }
            else
            {
                Console.WriteLine("\nЗмiни не збережено\n");
            }
        }

        public void Delete()
        {
            //Rate del = new Rate();
            int length = File.ReadAllLines("C:\\Users\\vladi\\RiderProjects\\Lab51\\STOCK.txt").Length;
            Console.WriteLine("Номер рядку:");
            int number = Checknum(Console.ReadLine());
            while (number > length || number <= 0)
            {
                Console.WriteLine("Номер рядку не може бути менше нуля або більше " + (length));
                number = Checknum(Console.ReadLine());
            }

            string[] str = File.ReadAllLines("C:\\Users\\vladi\\RiderProjects\\Lab51\\STOCK.txt");
            Console.WriteLine("\nЗбереження змін - Enter, відміна - будь-яка інша клавіша.");
            if (Console.ReadKey().Key == ConsoleKey.Enter)
            {
                using (StreamWriter f = new StreamWriter("C:\\Users\\vladi\\RiderProjects\\Lab51\\STOCK.txt"))
                    for (int i = 0; i < length; i++)
                    {
                        if (i != number - 1) f.WriteLine(str[i]);
                    }

                Console.WriteLine("Змiни збережено\n");
            }
            else
            {
                Console.WriteLine("\nЗмiни не збережено\n");
            }

        }

        public void Output()
        {
            Rate output = new Rate();
            int length = File.ReadAllLines("C:\\Users\\vladi\\RiderProjects\\Lab51\\STOCK.txt").Length;
            string[] str = File.ReadAllLines("C:\\Users\\vladi\\RiderProjects\\Lab51\\STOCK.txt");
            Console.WriteLine("{0,-31}{1,-16}{2,-15}{3,-5}{4,-20}", "Назва компанії","Код","Дата","курс відкриття","курс закриття");
            for (int i = 0; i < length; i++)
            {
                Console.WriteLine(str[i]);
            }
        }

        public void Calculation()
        {
            Rate calc = new Rate();
            int length = File.ReadAllLines("C:\\Users\\vladi\\RiderProjects\\Lab51\\STOCK.txt").Length;
            string[] str = File.ReadAllLines("C:\\Users\\vladi\\RiderProjects\\Lab51\\STOCK.txt");
            Console.WriteLine("{0,-31}{1,-16}{2,-24}{3,-12}{4,-10}", "Назва компанії","Код","Дата","курс відкриття","курс закриття");
            
            
            
        }

        public class Rate : STOCK
        {
            private string Date;
            private int Opencourse;
            private string Closingrate;

            public string date
            {
                get { return Date; }
                set { Date = value; }
            }

            public int opencourse
            {
                get { return Opencourse; }
                set { Opencourse = value; }
            }

            public string closingrate
            {
                get { return Closingrate; }
                set { Closingrate = value; }
            }

            public Rate()
            {
                
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            STOCK cw = new STOCK();
            while (true)
            {
                Console.OutputEncoding = Encoding.Unicode;
                Console.WriteLine("\nВибір режиму роботи: ");
                Console.WriteLine("Додавання записiв - Enter");
                Console.WriteLine("Редагування записiв - E");
                Console.WriteLine("Знищення записiв - Delete");
                Console.WriteLine("Виведення iнформацiї з файла на екран - Tab");
                Console.WriteLine("Індивідуальне завдання - С");
                ConsoleKeyInfo choice;
                choice = Console.ReadKey(true);
                if (choice.Key == ConsoleKey.Enter)
                    cw.Add();
                if (choice.Key == ConsoleKey.E)
                {
                    cw.Edit();
                }

                if (choice.Key == ConsoleKey.Delete)
                {
                    cw.Delete();
                }

                if (choice.Key == ConsoleKey.Tab)
                {
                    cw.Output();
                }

                if (choice.Key == ConsoleKey.C)
                {
                    cw.Calculation();
                }

            }
        }

    }
} 

