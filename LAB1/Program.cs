using System;

using RestSharp;

using Newtonsoft.Json;

namespace StockClient

{

    class Program

    {

        static void Main(string[] args)

        {
            Console.WriteLine("DOSTEPNE GIELDY");
            var client = new RestClient("https://stockserver20201009223011.azurewebsites.net/");

            var request = new RestRequest("stockexchanges", DataFormat.Json);

            var responseJson = client.Get(request);

            string[] stockExchanges = JsonConvert.DeserializeObject<string[]>(responseJson.Content);

            foreach (string stockExchange in stockExchanges)

                Console.WriteLine(stockExchange);

            Console.WriteLine("Wybierz gielde z ktorej chcesz zobaczyc oferte liczac kolejno od gory. Wprowadz cyfre (od 1 do 5)");

            int Selection = int.Parse(Console.ReadLine());

            switch (Selection)


            {
                case 1:
                    var request1 = new RestRequest("shareslist/Warszawa", DataFormat.Json);
                    var responseJson1 = client.Get(request1);
                    string[] akcje1 = JsonConvert.DeserializeObject<string[]>(responseJson1.Content);

                    foreach (string akcja1 in akcje1)
                    {
                        Console.WriteLine(akcja1);
                    }
                    
                    break;

                case 2:
                    var request2 = new RestRequest("shareslist/Londyn", DataFormat.Json);
                    var responseJson2 = client.Get(request2);
                    string[] akcje2 = JsonConvert.DeserializeObject<string[]>(responseJson2.Content);

                    foreach (string akcja2 in akcje2)
                    {
                        Console.WriteLine(akcja2);
                    }
                    break;
                case 3:
                    var request3 = new RestRequest("shareslist/Praga", DataFormat.Json);
                    var responseJson3 = client.Get(request3);
                    string[] akcje3 = JsonConvert.DeserializeObject<string[]>(responseJson3.Content);

                    foreach (string akcja3 in akcje3)
                    {
                        Console.WriteLine(akcja3);
                    }
                    break;
                case 4:
                    var request4 = new RestRequest("shareslist/Wieden", DataFormat.Json);
                    var responseJson4 = client.Get(request4);
                    string[] akcje4 = JsonConvert.DeserializeObject<string[]>(responseJson4.Content);

                    foreach (string akcja4 in akcje4)
                    {
                        Console.WriteLine(akcja4);
                    }
                    break;
                case 5:
                    var request5 = new RestRequest("shareslist/Paryz", DataFormat.Json);
                    var responseJson5 = client.Get(request5);
                    string[] akcje5 = JsonConvert.DeserializeObject<string[]>(responseJson5.Content);

                    foreach (string akcja5 in akcje5)
                    {
                        Console.WriteLine(akcja5);
                    }
                    break;
                default:
                    Console.WriteLine("Nie ma takiej opcji");
                    break;
            }



            Console.ReadKey();
        }

    }
}