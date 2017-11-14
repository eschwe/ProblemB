using System;
using SportsBallService;

namespace getmascot
{
    class Program
    {
        static void Main(string[] args)
        {
            ISportsBallService sbs;

            try
            {
                sbs = SportsBallFactory.GetProvider();
            }
            catch (ArgumentException)
            {
                Console.WriteLine("Failed to determine the type of the data source.  Check app.config to ensure that \"type\" is defined and associated with a valid provider\n\nPress any key to exit...");
                Console.ReadKey();
                return;
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to get sportsball provider: {0}\n\nPress any key to exit...", e.Message);
                Console.ReadKey();
                return;
            }

            Console.Write("Enter team name with city: ");
            string teamName = Console.ReadLine();

            string mascot = sbs.GetMascotNameFromTeamName(teamName);
            Console.WriteLine("Mascot: {0}\n\nPress any key to exit...", mascot);
            Console.ReadKey();
        }

    }
}
