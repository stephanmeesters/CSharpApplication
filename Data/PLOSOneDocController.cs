using System.Threading.Tasks;
using System.Net;
using System;
using System.Collections.Generic;

namespace AvaloniaTest.PLOSOne
{
    public class DocController : GenericJSONProcessor<Response>
    {
        private Response? cachedData_;

        public Response? CachedData
        {
            get => cachedData_;
        }

        public DocController()
        {
            cachedData_ = null;
        }

        public async Task<Response> LoadData(string query)
        {
            string url = $"http://api.plos.org/search?q=title:{query}&start=1&rows=100";
            var loader = InitAndParse(url);
            try
            {
                this.cachedData_ = await loader;
                return this.cachedData_;
            }
            catch (WebException)
            {
                throw new Exception(@"An error occurred during the query.");
            }
        }

        public bool IsDataAvailable()
        {
            return cachedData_ != null;
        }

        public Response? RequestData()
        {
            return cachedData_;
        }

        public List<string> listAllDocNames()
        {
            List<string> docNameList = new();

            if(cachedData_ == null)
                return docNameList;

            foreach (Doc doc in cachedData_.response.docs)
            {

                docNameList.Add(doc.short_title);
            }
            return docNameList;
        }

        public Doc? findDoc(string name)
        {
            if (cachedData_ == null)
                return null;

            foreach (Doc doc in cachedData_.response.docs)
            {
                if (name == doc.short_title || name == doc.title_display)
                    return doc;
            }
            return null;
        }

        public long numberOfResultsFound()
        {
            if (cachedData_ == null)
                return 0;
            return cachedData_.response.numFound;
        }
    }
}
