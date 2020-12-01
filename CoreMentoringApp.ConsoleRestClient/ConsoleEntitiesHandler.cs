using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CoreMentoringApp.ConsoleRestClient
{
    public class ConsoleEntitiesHandler : IEntitiesHandler
    {
        public async Task HandleAsync<T>(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                Console.WriteLine(JsonConvert.SerializeObject(entity));
            }

            Console.WriteLine();
            Console.WriteLine("<----------------------------------------------------->");
            Console.WriteLine();
        }
    }
}
