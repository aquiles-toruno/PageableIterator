using PageableIterator.Models;
using System.Collections;
using System.Collections.Generic;

namespace PageableIterator
{
    public abstract class Pageable<T> : IEnumerable<T>, IEnumerable
    {
        public virtual IEnumerator<T> GetEnumerator()
        {
            foreach (Page<T> page in AsPages())
            {
                foreach (T value in page.Values)
                {
                    yield return value;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public abstract IEnumerable<Page<T>> AsPages(int? pageSizeHint = default);
    }
}