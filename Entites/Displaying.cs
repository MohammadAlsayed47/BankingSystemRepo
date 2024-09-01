using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace BankingSystem.Entites
{
    public static class Displaying
    {
        public static void DisplayHistoryLog(IEnumerable<TransactionLog> transactionLogs)
        {
            var totalPages = (int)Math.Ceiling((double)transactionLogs.Count() / 7);
            var selectedPage = 1;

            while (selectedPage <= totalPages)
            {
                var currentPage = transactionLogs.Skip((int)Math.Ceiling((decimal)(selectedPage - 1) * 7)).Take(7);

                Console.Clear();

                Console.WriteLine("Log History: ");
                Console.WriteLine(
                    $"┌────┬─────┬──────────────┬───────────┬───────────────────────┬──────────────┬──────────────┐\n" +
                    $"│ Id │ HID │   Amount     │   Type    │      Date & Time      │     From     │      To      │\n" +
                    $"├────┼─────┼──────────────┼───────────┼───────────────────────┼──────────────┼──────────────┤");

                foreach (var log in currentPage)
                {

                    Console.WriteLine(
                        $"│ {log.Id,-3}│{log.HolderAccountId, -5}│ {log.Amount,-13}│  {log.TransactionType,-9}│  {log.CreatedAt,-21}│ {log.From,-13}│ {log.To,-13}│");

                    if (log == currentPage.Last())
                        Console.WriteLine(
                            $"└────┴─────┴──────────────┴───────────┴───────────────────────┴──────────────┴──────────────┘");
                    else
                        Console.WriteLine(
                            $"├────┼─────┼──────────────┼───────────┼───────────────────────┼──────────────┼──────────────┤");
                }

                string pages = "";
                for (int i = 1; i <= totalPages; i++)
                {
                    if (i == 1)
                        pages += i;
                    else
                        pages += $" {i}";
                }
                foreach (var s in pages.Split())
                {
                    if (int.Parse(s) == selectedPage)
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                    else
                        Console.ForegroundColor = ConsoleColor.White;

                    Console.Write($"{s} ");
                }
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("\n<- left arrow   right arrow ->\n" +
                    "Press other any key to back..");

                if (Console.ReadKey().Key == ConsoleKey.RightArrow)
                {
                    if (selectedPage == totalPages)
                        selectedPage = 1;
                    else
                        selectedPage++;
                }
                else if (Console.ReadKey().Key == ConsoleKey.LeftArrow)
                {
                    if ((selectedPage == 1))
                        selectedPage = totalPages;
                    else
                        selectedPage--;
                }
                else
                    break;
            }
        }
    }
}
