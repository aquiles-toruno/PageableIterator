using PageableIterator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PageableIterator
{
    public abstract class CollectionEnumerator<T>
    {
        public abstract Task<Page<T>> GetNextPageAsync(int? pageSize);
        public Pageable<T> ToSyncCollection() => new StoragePageable(this);

        private class StoragePageable : Pageable<T>
        {
            private CollectionEnumerator<T> _enumerator;
            private int _pageNumber = 0;

            //for mocking
            protected StoragePageable() : base()
            {
            }

            public StoragePageable(CollectionEnumerator<T> enumerator) : base()
            {
                _enumerator = enumerator;
            }

            public override IEnumerable<Page<T>> AsPages(int? pageHintSize = default)
            {
                Page<T> _page;
                do
                {
                    _pageNumber++;
                    Page<T> page = _enumerator.GetNextPageAsync(pageHintSize).Result;
                    _page = page;
                    _page.Number= _pageNumber;
                    yield return page;
                } while (_page != null && _page.Count != 0);
            }

            public override IEnumerator<T> GetEnumerator()
            {
                Page<T> page = _enumerator.GetNextPageAsync(null).Result;

                foreach (T item in page.Values)
                {
                    yield return item;
                }
            }
        }
    }
}