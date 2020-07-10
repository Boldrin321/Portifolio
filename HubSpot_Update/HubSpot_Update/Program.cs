using System;
using System.Collections.Generic;
using HubSpot.NET.Api.Deal.Dto;
using HubSpot.NET.Core;
using HubSpot.NET.Core.Interfaces;
using System.Linq;
using RestSharp;
using Flurl;
using HubSpot.NET.Core.Requests;
using HubSpot_Update.Model;
using System.Data.OleDb;
using System.Data;
using System.IO;
using ClosedXML.Excel;

namespace HubSpot_Update
{
    public class Program
    {
        static void Main(string[] args)
        {
          
            var api = new HubSpotApi("");
            var wb = new XLWorkbook(@"C:\Users\pedro\source\repos\slnHubSpot_Update\HubSpot_Update\HubSpot.xlsx");
            var planilha = wb.Worksheet(1);
            var deals = api.Deal.List<DealHubSpotModel>(true,
                    new ListRequestOptions { PropertiesToInclude = new List<string> { "dealId" } }).Deals;

            long dealId;
            var linha = 2;
            var hub = new List<HubSpotModel>();
            

            while (true)
            {
                
                var id_deal = planilha.Cell("A" + linha.ToString()).Value.ToString();
                var businnesstype = planilha.Cell("B" + linha.ToString()).Value.ToString();
                if (string.IsNullOrEmpty(id_deal)) break;

                hub.Add(new HubSpotModel(Convert.ToInt32(id_deal), businnesstype));
                linha++;

            }

            foreach (DealHubSpotModel de in deals)
            {
                dealId = (long)de.Id;
                var deal = api.Deal.GetById<DealHubSpotModel>(dealId);
                Console.WriteLine($"O Id do Deal é: {de.Id}" +
                    $"\nNo Hubspot o DealType é: {deal.DealType}");
                
                int dealId2 = (int)dealId;
                deal.DealType = hub.Single<HubSpotModel>(a => a.Id == dealId2).BusinessType;
                try
                {
                    api.Deal.Update(deal);
                    Console.WriteLine($"\no item foi atualizado com sucesso!!!" +
                        $"\nAgora o valor do Tipo de Negócio é: {deal.DealType}" +
                        $"\n\nAtualização do Tipo de Negócio para a venda: {deal.Name}. Foi concluido com sucesso!!!\n" +
                        $"\n-------------------------||-----------------------------\n");
                }
                catch
                {
                    Console.WriteLine("Não foi possivel atualizar os dados!!!\n");
                }
                
            }
            Console.WriteLine("\nFim!!!");
            Console.ReadLine();
        }
    }
}
