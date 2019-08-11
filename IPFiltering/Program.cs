using System;

namespace IPFiltering
{
    class Program
    {
        static void Main(string[] args)
        {
            bool keepRunning = true;
            string ipToAdd = "";
            Library.IPFilter.CreateTestList();
            Console.WriteLine(" A test list has been created using the range between 192.168.0.1, and 192.168.0.156");
            while (keepRunning)
            {
                Console.WriteLine("press a to add an address, r to add a range, c to check an address and q to quit");


                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.A:
                        Console.WriteLine();
                        Console.WriteLine("Enter an IP Address to add: (e.g. 192.168.0.1) and press return.");
                        ipToAdd = Console.ReadLine();
                        Library.IPFilter.AddIPAddress(ipToAdd);
                        Console.WriteLine("You can now check if the address has been added.");
                        break;

                    case ConsoleKey.R:
                        Console.WriteLine();
                        Console.WriteLine("Enter an IP Address to add: (e.g. 192.168.0.1/24) and press return.");
                        ipToAdd = Console.ReadLine();
                        int cidr = Library.IPFilter.ParseAddressToGetCIDR(ipToAdd);
                        if (ipToAdd.Split("/").Length == 2)
                            Library.IPFilter.AddIPRange(ipToAdd);
                        else
                            Library.IPFilter.AddIPRange(ipToAdd, cidr);
                        Console.WriteLine("You can now check if the range has been added.");
                        break;

                    case ConsoleKey.C:
                        Console.WriteLine();
                        Console.WriteLine("Enter an IP Address check: (e.g. 192.168.0.1) and press return.");
                        ipToAdd = Console.ReadLine();
                        if (Library.IPFilter.iplist.CheckNumber(ipToAdd))
                            Console.WriteLine("Ip Address {0} exists in the list.", ipToAdd);
                        else
                            Console.WriteLine("Ip Address {0} does not exists in the list.", ipToAdd);
                        break;

                    case ConsoleKey.Q:
                        Console.WriteLine();
                        Console.WriteLine("Thank you for trying the program.");
                        keepRunning = false;
                        break;
                    default:
                        continue;
                }
            }
        }
    }
}
