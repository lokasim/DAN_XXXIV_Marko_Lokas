using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Bank
{
    class Program
    {
        public static int TotalMoney = 10000;
        public static int repeatLetter = 100;
        public static List<int> ListPeopleOfNumber = new List<int>();
        public static List<Thread> ListThreadATM1 = new List<Thread>();
        public static List<Thread> ListThreadATM2 = new List<Thread>();
        
        static void Main(string[] args)
        {
            CultureInfo current = CultureInfo.CurrentCulture;
            CultureInfo newCulture;
            newCulture = new CultureInfo("sr-SR");

            bool mainMenu = true;
            do
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Welcome to Bank App\n\n");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("1. Start ATMs");
                Console.WriteLine("\n0. Leave Bank App");
                Console.Write("\n============================================\n");
                Console.Write("Choose: ");
                string choseMainMenu = Console.ReadLine();
                switch (choseMainMenu)
                {
                    case "1":
                        //InsertNumberPeopleOfATM
                        bool correctInput = true;
                        

                        for (int i = 1; i < 3; i++)
                        {
                            do
                            {
                                int ATM = NumberOfPeopleATM(i.ToString());
                                if (ATM == -2)
                                {
                                    Console.Clear();
                                    correctInput = false;
                                    mainMenu = true;
                                }
                                else if (ATM == -1)
                                {
                                    correctInput = true;
                                    Console.Clear();
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("Wrong entry, the number you enter must be positive (zero or greater)\n");
                                    Console.ForegroundColor = ConsoleColor.Green;
                                }
                                else
                                {

                                    if (ATM > 1001)
                                    {
                                        Console.Clear();
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine("Enter a number between 0 and 1000");
                                        Console.ForegroundColor = ConsoleColor.Green;
                                        correctInput = true;
                                    }
                                    else
                                    {
                                        correctInput = false;
                                        ListPeopleOfNumber.Add(ATM);
                                    }
                                }
                            } while (correctInput);
                        }
                        Console.Clear();
                        int counter = 0;
                        if(ListPeopleOfNumber.Count == 2)
                        {
                            foreach (var item in ListPeopleOfNumber)
                            {
                                counter++;
                                Console.WriteLine("People Number of ATM" + counter + ": " + item);
                            }
                        }
                        CreateThreads1();
                        CreateThreads2();

                        foreach (var threadATM1 in ListThreadATM1)
                        {
                            threadATM1.Start();
                        }
                        foreach (var threadATM2 in ListThreadATM2)
                        {
                            threadATM2.Start();
                            threadATM2.Join();
                        }



                        break;
                    case "0":
                        bool yesNo = true;
                        do
                        {
                            Console.WriteLine("\n*  *  *  *  *  *  *  *  *  *  *");
                            Console.WriteLine("* Are you sure want to leave? *");
                            Console.WriteLine("*          Yes   /   No        *");
                            Console.WriteLine("*  *  *  *  *  *  *  *  *  *  *");
                            Console.Write("Choose: ");
                            string odgovor = Console.ReadLine();
                            if (odgovor.ToLower() == "yes")
                            {
                                Console.Clear();
                                yesNo = false;
                                mainMenu = false;
                                break;
                            }
                            else if (odgovor.ToLower() == "no")
                            {
                                Console.Clear();
                                yesNo = false;
                                mainMenu = true;
                                break;
                            }
                            else if (odgovor.ToLower() != "no" || odgovor.ToLower() != "yes")
                            {
                                Console.Clear();
                                Console.WriteLine("\n\tYou can only enter \"Yes\" or \"No\"\n\n");

                            }
                        } while (yesNo);
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Wrong input, please try again...");
                        break;
                }
            } while (mainMenu);
        }

        private static readonly object locker = new object();

        public static int NumberOfPeopleATM(string ATM)
        {
            Console.ForegroundColor = ConsoleColor.White;
            //Console.WriteLine("NOTICE: It is recommended to be up to 50 people,\ndue to the visibility of the application...");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Please enter the number of customers who can \naccess the ATM" + ATM);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Press \"X\" go to main menu:");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"\nNumber of people at the ATM{ATM}: ");
            string peopleNumber = Console.ReadLine();
            if (peopleNumber.ToUpper() == "X")
            {
                return -2;
            }
            int peopleNumberInt;
            bool peopleBool;
            peopleBool = int.TryParse(peopleNumber, out peopleNumberInt);
            if(peopleNumberInt < 0 || !peopleBool)
            {
                return -1;
            }
            else
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"ATM{ATM}, can be accessed by {peopleNumberInt} people");
                Console.ForegroundColor = ConsoleColor.Green;
                return peopleNumberInt;
            }
        }

        public static void CreateThreads1()
        {
            for (int i = 1; i <= ListPeopleOfNumber[0]; i++)
            {
                ListThreadATM1.Add(new Thread(new ThreadStart(ATM1)));
                string nameThread = "ATM1-";
                ListThreadATM1.LastOrDefault().Name = string.Format(nameThread + i);
            }
        }

        public static void ATM1()
        {
            lock (locker)
            {
                Thread thread = Thread.CurrentThread;
            string threadName = thread.Name;


            string AtmName = threadName.Substring(0, 4);
            //string PeopleName;
            //Console.WriteLine(threadName.PadLeft(30, '=') + new string('=', 30));
            Console.WriteLine(new string('=', repeatLetter));
            Console.WriteLine(AtmName);
            
                Console.WriteLine(new string('-', repeatLetter));
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Total money in BANK: " + TotalMoney);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(new string('-', repeatLetter));
                Thread.Sleep(RandomNumberSleep());
                int money = RandomNumber();
                if (TotalMoney > money)
                {
                    Console.WriteLine(AtmName);
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"Client number \"{threadName}\" withdrew money from an ATM \"{AtmName}\" in the amount of { money }.oo  RSD.");
                    Console.WriteLine(new string('-', repeatLetter));
                    TotalMoney = TotalMoney - money;
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Unfortunately, there is not enough money to pay.");
                    Console.WriteLine($"It is not possible to pay {money}.oo RSD to the client with card number {threadName}.");

                    Console.ForegroundColor = ConsoleColor.Green;
                }
                Console.WriteLine(new string('=', repeatLetter));
            }
        }

        public static void CreateThreads2()
        {
            for (int i = 1; i <= ListPeopleOfNumber[1]; i++)
            {
                ListThreadATM2.Add(new Thread(new ThreadStart(ATM2)));
                string nameThread = "ATM2-";
                ListThreadATM2.LastOrDefault().Name = string.Format(nameThread + i);
            }
        }

        public static void ATM2()
        {
            lock (locker)
            {
                Thread thread = Thread.CurrentThread;
            string threadName = thread.Name;
            
            
            string AtmName = threadName.Substring(0, 4);
            //string PeopleName;
            //Console.WriteLine(threadName.PadLeft(30, '=') + new string('=', 30));
            Console.WriteLine(new string('=', repeatLetter));
            Console.WriteLine(AtmName);
            
                Console.WriteLine(new string('-', repeatLetter));
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Total money in BANK: " + TotalMoney);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(new string('-', repeatLetter));
                Thread.Sleep(RandomNumberSleep());
                int money = RandomNumber();
                if (TotalMoney > money)
                {
                    Console.WriteLine(AtmName);
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"Client number \"{threadName}\" withdrew money from an ATM \"{AtmName}\" in the amount of { money }.oo  RSD.");
                    Console.WriteLine(new string('-', repeatLetter));
                    TotalMoney = TotalMoney - money;
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Unfortunately, there is not enough money to pay.");
                    Console.WriteLine($"It is not possible to pay {money}.oo RSD to the client with card number {threadName}.");

                    Console.ForegroundColor = ConsoleColor.Green;
                }
                Console.WriteLine(new string('=', repeatLetter));
            }
        }

        static int RandomNumber()
        {
            Random random = new Random();
            return random.Next(100, 10001);
        }

        static int RandomNumberSleep()
        {
            Random random = new Random();
            return random.Next(0, 100);
        }
    }
}
