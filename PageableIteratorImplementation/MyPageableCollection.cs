using Newtonsoft.Json;
using PageableIterator;
using PageableIterator.Models;
using PageableIteratorImplementation.DTO;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace PageableIteratorImplementation
{
    public class MyPageableCollection : CollectionEnumerator<PostDto>
    {
        private readonly HttpClient _httpClient;
        private int _offset = 0;
        public MyPageableCollection()
        {
            _httpClient = new HttpClient();
        }

        public override async Task<Page<PostDto>> GetNextPageAsync(int? pageSize = default)
        {
            const string uriTemplate = "https://jsonplaceholder.typicode.com/posts?_start={0}&_limit={1}";

            try
            {
                var request = await _httpClient.GetAsync(string.Format(uriTemplate, _offset, pageSize));
                var response = request.Content;

                if (pageSize.HasValue)
                    _offset += pageSize.Value;

                var stringResponse = await response.ReadAsStringAsync();

                var posts = JsonConvert.DeserializeObject<IEnumerable<PostDto>>(stringResponse);

                return Page<PostDto>.FromValues(posts);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
