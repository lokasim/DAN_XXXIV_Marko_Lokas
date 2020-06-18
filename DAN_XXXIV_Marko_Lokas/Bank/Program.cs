using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank
{
    class Program
    {
        static void Main(string[] args)
        {
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
                        //InsertNumberPeopleOfATM1
                        bool correctInput = true;
                        List<int> peopleOfNumber = new List<int>();

                        for (int i = 1; i < 3; i++)
                        {
                            do
                            {
                                //Console.Clear();
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
                                        peopleOfNumber.Add(ATM);
                                    }
                                }
                            } while (correctInput);
                        }
                        Console.Clear();
                        int counter = 0;
                        if(peopleOfNumber.Count == 2)
                        {
                            foreach (var item in peopleOfNumber)
                            {
                                counter++;
                                Console.WriteLine("People Number of ATM" + counter + ": " + item);
                            }
                        }
                        

                        //InsertNumberPeopleOfATM2


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

        public static int NumberOfPeopleATM(string ATM)
        {
            //Console.Clear();
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
    }
}
