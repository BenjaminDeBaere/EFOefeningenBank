using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFOefeningenBank
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Geef het te verwijderen klantnr in");
            int klantnr;
            if (int.TryParse(Console.ReadLine().ToString(), out klantnr))
            {
                using (var entities = new BankEntities())
                {
                    Klant klant = entities.Klanten.Find(klantnr);
                    if(klant !=null)
                    {
                        if (klant.Rekeningen.Count==0)
                        {
                            entities.Klanten.Remove(klant);
                            entities.SaveChanges();
                        }
                        else
                        {
                            Console.WriteLine("De klant heeft nog rekeningen.");
                        }
                            
                    }
                    else
                    {
                        Console.WriteLine("Klant niet gevonden");
                    }
                }

            }
            else
            {
                Console.WriteLine("Geef een getal in. ");
            }
            Console.ReadLine();
        }
    
    }
}
