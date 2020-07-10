using HubSpot.NET.Api.Deal.Dto;
using HubSpot.NET.Api.Owner.Dto;
using HubSpot.NET.Core;
using hubspot_test.Data;
using hubspot_test.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;


namespace hubspot_test
{
    class Program
    {
        static void Main(string[] args)
        {
            
            Deals d = new Deals();
            var hubKey = System.Configuration.ConfigurationManager.AppSettings["hubKey"];
            var api = new HubSpotApi(hubKey);


            // ids(1434839429)(1441579255)(1463049837)
            var deals = api.Deal.List<DealHubSpotModel>(true,
                    new ListRequestOptions { PropertiesToInclude = new List<string> { "dealId" } }).Deals;

            int contDeals = 0;

            foreach (DealHubSpotModel de in deals)
            {
                var deal = api.Deal.GetById<DealHubSpotModel>((long)de.Id);
                var owners = api.Owner.GetAll<OwnerHubSpotModel>();
                var owner = owners.First(o => o.Id == deal.OwnerId);

                string namefull = $"{owner.FirstName} {owner.LastName}";
                d.ResponsavelNegocio = namefull;
                d.ValorNegocio = (double)deal.Amount;
                d.DataNegocio = deal.CloseDate;

                if (deal.DealType == "newbusiness")
                {
                    d.TipoNegocio = "New Business";
                }
                else if (deal.DealType == "existingbusiness")
                {
                    d.TipoNegocio = "Existing Business";
                }
                else if (deal.DealType == null)
                {
                    d.TipoNegocio = null;
                }

                using (var context = new DbContext())
                {
                    context.deals.Add(d);
                    context.SaveChanges();
                    d.Id = 0;
                }
                contDeals ++;
            }

            Console.WriteLine($"Foram salvos {contDeals} Deals." +
            "\nFim!!!");
            /*Console.WriteLine("Digite um Id disponivel: ");
            string id;
            id = Console.ReadLine();
            Console.WriteLine($"\nO Id selecionado foi: {id}");
            try
            {
                long dealId = long.Parse(id);

                var deal = api.Deal.GetById<DealHubSpotModel>(dealId);
                var owners = api.Owner.GetAll<OwnerHubSpotModel>();
                var owner = owners.First(o => o.Id == deal.OwnerId);
                
                string namefull = $"{owner.FirstName} {owner.LastName}";
                d.ResponsavelNegocio = namefull;
                d.ValorNegocio = (double)deal.Amount;
                d.DataNegocio = deal.CloseDate;

                if (deal.DealType == "newbusiness")
                {
                    d.VendaNova = "New Business";
                    d.Recompra = null;
                }
                else if (deal.DealType == "existingbusiness")
                {
                    d.Recompra = "Existing Business";
                    d.VendaNova = null;
                }

                Console.WriteLine($"Responsavel pelo Negócio: {namefull}" +
                    $"\nValor do Negócio: {d.ValorNegocio}" +
                    $"\nData do Negócio: {d.DataNegocio}" +
                    $"\nVenda Nova: {d.VendaNova}" +
                    $"\nRecompra: {d.Recompra}");
            }
            catch (Exception e)
            {
                Console.WriteLine("o Id digitado não existe" +
                    "\nFim!!!");
            }
            finally
            {
                try
                {
                    using (var context = new DbContext())
                    {
                        context.deals.Add(d);
                        context.SaveChanges();
                        Console.WriteLine("\nFim!!!");
                    }
                }
                catch(Exception e)
                {
                    Console.WriteLine($"A Conexão com banco falhou!!!!!!!" +
                        "\nFim!!!");
                }
            }*/
            Console.ReadLine();
            
        }
    }
}
