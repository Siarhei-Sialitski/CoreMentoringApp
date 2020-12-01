using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoreMentoringApp.ConsoleRestClient
{
    public interface IEntitiesHandler
    {
        Task HandleAsync<T>(IEnumerable<T> entities);
    }
}
