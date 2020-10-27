using System;

using RestSharp;

using Newtonsoft.Json;
using System.Threading;
using System.IO;

namespace StockClient

{
    public class PriceData
    {
        public int time { get; set; }
        public double price { get; set; }
        public string buySell { get; set; }
        public int amount { get; set; }
    }

    public class Offer
    {
        public string stockExchange { get; set; }
        public string share { get; set; }
        public int amount { get; set; }
        public double price { get; set; }
        public string buySell { get; set; }
    }
    class Program

    {


        static string[] Shareslist(string miasto, RestClient client)
        {
            var request = new RestRequest("shareslist/"+miasto, DataFormat.Json);
            var responseJson = client.Get(request);
            string[] akcje = JsonConvert.DeserializeObject<string[]>(responseJson.Content);
            return akcje;
        }
        static PriceData[] Pricelist(string miasto, string wybranaAkcja,RestClient client)
        {
            var request = new RestRequest("shareprice/" + miasto+"?share="+wybranaAkcja, DataFormat.Json);
            var responseJson = client.Get(request);
            PriceData[] ceny = JsonConvert.DeserializeObject<PriceData[]>(responseJson.Content);
            return ceny;
        }

        static string KupSprzedaj(string wybor, int ilosc,string akcja, string miasto, string cena)
        {
            var client = new RestClient("https://stockserver20201009223011.azurewebsites.net/offer");
            var request = new RestRequest(Method.POST);
            request.AddHeader("Authorization", "Basic MDExNDM4MTVAcHcuZWR1LnBsOltBbGxhZHluMV0=");
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Cookie", "ARRAffinity=9762c3336c900b69b18cd4d2ae83ac1101b339632e10a79ede8dc11c83b33a4d; ARRAffinitySameSite=9762c3336c900b69b18cd4d2ae83ac1101b339632e10a79ede8dc11c83b33a4d");
            request.AddParameter("application/json", "{\r\n   \"stockExchange\":\""+miasto+"\",\r\n    \"share\":\""+akcja+"\",\r\n    \"amount\":"+ilosc+",\r\n    \"price\":"+cena+ ",\r\n   \"buySell\":\"" + wybor+"\"\r\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            return response.Content;
        }

        static string StanKonta(RestClient client)
        {
            var request = new RestRequest("client", Method.GET);
            request.AddHeader("Authorization", "Basic MDExNDM4MTVAcHcuZWR1LnBsOltBbGxhZHluMV0=");
            request.AddHeader("Cookie", "ARRAffinity=9762c3336c900b69b18cd4d2ae83ac1101b339632e10a79ede8dc11c83b33a4d; ARRAffinitySameSite=9762c3336c900b69b18cd4d2ae83ac1101b339632e10a79ede8dc11c83b33a4d");
            IRestResponse response = client.Execute(request);
          
            return response.Content;

        }
        
        static void Main(string[] args)
        {
            var client = new RestClient("https://stockserver20201009223011.azurewebsites.net/");
            
            string Portfel = StanKonta(client);
            Console.WriteLine(Portfel);
            Start:
            Console.WriteLine("DOSTEPNE GIELDY");
            

            var request = new RestRequest("stockexchanges", DataFormat.Json);

            var responseJson = client.Get(request);

            string[] stockExchanges = JsonConvert.DeserializeObject<string[]>(responseJson.Content);
            int l = 1;
            foreach (string stockExchange in stockExchanges)
                
            {
              
                Console.WriteLine(l+". "+stockExchange);
                l++;

            }
            Console.WriteLine("Wybierz gielde ktorej chcesz zobaczyc oferte.");
            int Selection = int.Parse(Console.ReadLine());
            string wybraneMiasto = stockExchanges[Selection - 1];
            Console.WriteLine(wybraneMiasto);
            string[] listaAkcji = Shareslist(wybraneMiasto, client);
            int i = 1;
            foreach (string akcja in listaAkcji)
            {
                
                Console.WriteLine(i+"."+akcja);
                i++;
            }

            Console.WriteLine("Wybierz numer akcji ktorej chcesz zobaczyc ceny.");
            Selection = int.Parse(Console.ReadLine());
            string wybranaAkcja = listaAkcji[Selection - 1]; 
            Console.WriteLine(wybranaAkcja);
            PriceData[] listaCen = Pricelist(wybraneMiasto, wybranaAkcja, client);
            foreach (PriceData cena in listaCen)
            Console.WriteLine("time " + cena.time + " price " + cena.price+ " buySell " + cena.buySell + " amount " + cena.amount);
            
            Console.WriteLine("Aby kupic wpisz buy, aby sprzedac wpisz sell"); 
            string Selection1 = Console.ReadLine();

            Console.WriteLine("Podaj ilosc akcji ktora chcesz sprzedac/kupic [l. calkowita]");
            int Selection2 = int.Parse(Console.ReadLine());
            Console.WriteLine("Za ile");
            string Selection3 = Console.ReadLine();
       
            string wynik = KupSprzedaj(Selection1, Selection2, wybranaAkcja ,wybraneMiasto, Selection3);
            Console.WriteLine(wynik);
            Portfel = StanKonta(client);
            Console.WriteLine(Portfel);
            Console.WriteLine("Jeżeli chcesz kontynuować zakupy gieldowe wcisnij enter. Aby wyjsc wcisnij q");
            string koniec = Console.ReadLine();
            if(koniec=="q")
            {
                Console.WriteLine("Dziekuje za skorzystanie z klienta gieldowego. Zapraszam ponownie!");
                System.Environment.Exit(0);
            }
            else
            goto Start;
            
        }

    }
}

