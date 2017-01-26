using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace EFOefeningenBank
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Geef het rekeningnummer van de rekening waar de overschrijving vertrekt.");
            var rekeningVan = Console.ReadLine().ToString();
            var transactionOptions = new TransactionOptions
            {
                IsolationLevel = IsolationLevel.RepeatableRead
            };
            using (var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions))
            {
                using (var entities = new BankEntities())
                {
                    var vanRekening = entities.Rekeningen.Find(rekeningVan);
                    if (vanRekening != null)
                    {
                        Console.WriteLine("Geef het rekeningnummer in van de ontvangersrekening.");
                        var naarRekening = entities.Rekeningen.Find(Console.ReadLine().ToString());
                        if (naarRekening != null)
                        {
                            Console.WriteLine("Geef het bedrag in dat overgeschreven wordt");
                            decimal bedrag;
                            if (decimal.TryParse(Console.ReadLine().ToString(), out bedrag))
                            {
                                if (bedrag >= 0m)
                                {
                                    if (vanRekening.Saldo >= bedrag)
                                    {
                                        vanRekening.Overschrijven(naarRekening, bedrag);
                                        entities.SaveChanges();
                                        transactionScope.Complete();
                                    }
                                    else
                                    {
                                        Console.WriteLine("Niet genoeg saldo op de rekening");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Geef een bedrag in groter dan nul.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Geef een getal in.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Rekening naar niet gevonden.");
                        }

                    }
                    else
                    {
                        Console.WriteLine("Rekening van niet gevonden.");
                    }
                    
                }
            }
            Console.ReadLine();
        }
    
    }
}
