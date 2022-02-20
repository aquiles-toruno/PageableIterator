using System.Collections.Generic;
using System.Linq;

namespace PageableIterator.Models
{
    public class Page<T>
    {
        private Page(IEnumerable<T> values)
        {
            Values = values;
        }

        public IEnumerable<T> Values { get; }
        public int Number { get; set; }
        public int Count => Values.Count();

        public static Page<T> FromValues(IEnumerable<T> values) => new Page<T>(values);
    }
}