using Lucene.Net.Documents;

namespace Recomendations_app.Models
{
    public class SearchResult
    {
        private readonly Document _doc;

        public SearchResult(Document doc)
        {
            _doc = doc;
        }

    }
}
