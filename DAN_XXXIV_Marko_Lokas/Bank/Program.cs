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
        public static int repeatLetter = 50;
        public static List<int> ListPeopleOfNumber = new List<int>();
        public static List<Thread> ListThreadATM1 = new List<Thread>();
        public static List<Thread> ListThreadATM2 = new List<Thread>();
        public static List<Thread> ListAllThread = new List<Thread>();

        static void Main(string[] args)
        {
            bool mainMenu = true;
            do
            {
                if (TotalMoney != 10000)
                {
                    TotalMoney = 10000;
                }
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
                        if (ListPeopleOfNumber.Count == 2)
                        {
                            foreach (var item in ListPeopleOfNumber)
                            {
                                counter++;
                                if (item == 0)
                                {
                                    Console.Write("ATM" + counter + ": ");
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.Write("Not working.");
                                    Console.ForegroundColor = ConsoleColor.Green;

                                }
                                else
                                {
                                    Console.WriteLine("People Number of ATM" + counter + ": " + item);
                                }
                                Console.Write("\n\n");
                            }
                        }

                        //Unable to run script
                        if (ListPeopleOfNumber[0] == 0 && ListPeopleOfNumber[1] == 0)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("ATMs is not working... Please try again");
                            Console.ForegroundColor = ConsoleColor.Green;
                            ListPeopleOfNumber.Clear();
                        }
                        //when at least one ATM is operational
                        else
                        {
                            //create threads
                            CreateThreads1();
                            CreateThreads2();
                            //Adding threads to one list
                            if (ListThreadATM1.Count > ListThreadATM2.Count)
                            {
                                for (int j = 0; j < ListThreadATM1.Count; j++)
                                {
                                    ListAllThread.Add(ListThreadATM1[j]);
                                    for (int k = j; k < ListThreadATM2.Count; k++)
                                    {

                                        ListAllThread.Add(ListThreadATM2[k]);
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                for (int j = 0; j < ListThreadATM2.Count; j++)
                                {
                                    ListAllThread.Add(ListThreadATM2[j]);
                                    for (int k = j; k < ListThreadATM1.Count; k++)
                                    {
                                        ListAllThread.Add(ListThreadATM1[k]);
                                        break;
                                    }
                                }
                            }
                            //Running threads
                            foreach (var thread in ListAllThread)
                            {
                                thread.Start();
                                thread.Join();
                            }
                            ListAllThread.LastOrDefault().Join();

                            Console.WriteLine("Press any key, go to Main Menu...");
                            Console.ReadKey();
                            Console.Clear();

                            //Delete the values in the lists, so that the script executes correctly again
                            ListPeopleOfNumber.Clear();
                            ListAllThread.Clear();
                            ListThreadATM1.Clear();
                            ListThreadATM2.Clear();
                        }

                        break;
                    case "0":
                        //Exit app
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
        /// <summary>
        /// Method with the help of which the number of threads is determined
        /// </summary>
        /// <param name="ATM">ATM Name</param>
        /// <returns></returns>
        public static int NumberOfPeopleATM(string ATM)
        {
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
            if (peopleNumberInt < 0 || !peopleBool)
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
        /// <summary>
        /// Creating threads for the first ATM
        /// </summary>
        public static void CreateThreads1()
        {
            for (int i = 1; i <= ListPeopleOfNumber[0]; i++)
            {
                ListThreadATM1.Add(new Thread(new ThreadStart(ATM1)));
                string nameThread = "ATM1-";
                ListThreadATM1.LastOrDefault().Name = string.Format(nameThread + i);
            }
        }

        /// <summary>
        /// Logic for the first ATM
        /// </summary>
        public static void ATM1()
        {
            lock (locker)
            {
                Thread thread = Thread.CurrentThread;
                string threadName = thread.Name;

                string AtmName = threadName.Substring(0, 4);
                Console.WriteLine(new string('=', repeatLetter));
                Console.WriteLine(new string(' ', 23) + AtmName);
                Console.WriteLine(new string('-', repeatLetter));

                //Current balance of money in the bank
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Total money in BANK: " + TotalMoney + ".oo RSD");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(new string('-', repeatLetter));

                Thread.Sleep(RandomNumberSleep());
                //amount to be paid
                int money = RandomNumber();
                if (TotalMoney >= money)
                {
                    //Message: when there is enough money in the bank
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"Client number: \"{threadName}\" \nAction: withdrew money \nATM: \"{AtmName}\" \nAmount: { money }.oo RSD.");
                    Console.WriteLine(new string('-', repeatLetter));
                    TotalMoney = TotalMoney - money;
                    Console.WriteLine("Total money in BANK: " + TotalMoney + ".oo RSD");
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                else
                {
                    //Message: when there is not enough money in the bank
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Unfortunately, there is not enough money to pay.\n");
                    Console.WriteLine($"Client number: \"{threadName}\" \nAction: withdrew money \nATM: \"{AtmName}\" \nAmount: { money }.oo RSD.");
                    Console.WriteLine("\nTransaction stopped.");
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                Console.WriteLine(new string('=', repeatLetter));
            }
        }

        /// <summary>
        /// Creating threads for the second ATM
        /// </summary>
        public static void CreateThreads2()
        {
            for (int i = 1; i <= ListPeopleOfNumber[1]; i++)
            {
                ListThreadATM2.Add(new Thread(new ThreadStart(ATM2)));
                string nameThread = "ATM2-";
                ListThreadATM2.LastOrDefault().Name = string.Format(nameThread + i);
            }
        }

        /// <summary>
        /// Logic for the second ATM
        /// </summary>
        public static void ATM2()
        {
            lock (locker)
            {
                Thread thread = Thread.CurrentThread;
                string threadName = thread.Name;

                string AtmName = threadName.Substring(0, 4);
                int spaceNumber = 50;
                Console.WriteLine(new string(' ', spaceNumber) + new string('=', repeatLetter));
                Console.WriteLine(new string(' ', spaceNumber) + new string(' ', 23) + AtmName);
                Console.WriteLine(new string(' ', spaceNumber) + new string('-', repeatLetter));
                Console.ForegroundColor = ConsoleColor.Yellow;

                //Current balance of money in the bank
                Console.WriteLine(new string(' ', spaceNumber) + "Total money in BANK: " + TotalMoney + ".oo RSD");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(new string(' ', spaceNumber) + new string('-', repeatLetter));
                Thread.Sleep(RandomNumberSleep());
                //amount to be paid
                int money = RandomNumber();
                if (TotalMoney >= money)
                {
                    //Message: when there is enough money in the bank
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine(new string(' ', spaceNumber) + $"Client number: \"{threadName}\" \n" + new string(' ', spaceNumber) +
                                    $"Action: withdrew money \n" + new string(' ', spaceNumber) +
                                    $"ATM: \"{AtmName}\" \n" + new string(' ', spaceNumber) +
                                    $"Amount: { money }.oo  RSD.");
                    Console.WriteLine(new string(' ', spaceNumber) + new string('-', repeatLetter));
                    TotalMoney = TotalMoney - money;
                    Console.WriteLine(new string(' ', spaceNumber) + "Total money in BANK: " + TotalMoney + ".oo RSD");
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                else
                {
                    //Message: when there is not enough money in the bank
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(new string(' ', spaceNumber) + "Unfortunately, there is not enough money to pay.\n");
                    Console.WriteLine(new string(' ', spaceNumber) + $"Client number: \"{threadName}\" \n" + new string(' ', spaceNumber) +
                                    $"Action: withdrew money \n" + new string(' ', spaceNumber) +
                                    $"ATM: \"{AtmName}\" \n" + new string(' ', spaceNumber) +
                                    $"Amount: { money }.oo  RSD.");
                    Console.WriteLine("\n" + new string(' ', spaceNumber) + "Transaction stopped.");
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                Console.WriteLine(new string(' ', spaceNumber) + new string('=', repeatLetter) + "\n");
            }
        }

        /// <summary>
        /// Random payout generation
        /// </summary>
        /// <returns></returns>
        static int RandomNumber()
        {
            Random random = new Random();
            return random.Next(100, 10001);
        }

        /// <summary>
        /// A method that determines how many threads will sleep, 
        /// to RandomNumber() generate different numbers
        /// </summary>
        /// <returns></returns>
        static int RandomNumberSleep()
        {
            Random random = new Random();
            return random.Next(0, 100);
        }
    }
}
