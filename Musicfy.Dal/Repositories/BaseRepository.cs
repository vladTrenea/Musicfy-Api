using Neo4jClient;

namespace Musicfy.Dal.Repositories
{
    public class BaseRepository
    {
        protected IGraphClient _graphClient;

        public BaseRepository(IGraphClient graphClient)
        {
            _graphClient = graphClient;
        }
    }
}