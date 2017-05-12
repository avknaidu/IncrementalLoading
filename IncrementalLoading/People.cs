using Microsoft.Toolkit.Uwp;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace IncrementalLoading
{
    public class PeopleSource : IIncrementalSource<Person>
    {
        private List<Person> _people;
        public PeopleSource()
        {
            _people = new List<Person>();
            for (int i = 1; i <= 20; i++)
            {
                var p = new Person { Name = "Person " + i };
                _people.Add(p);
            }
        }

        public async Task<IEnumerable<Person>> GetPagedItemsAsync(int pageIndex, int pageSize, CancellationToken cancellationToken = default(CancellationToken))
        {
            IEnumerable<Person> result = null;
            int count = pageSize * pageIndex;
            for (int i = count + 1; i <= count + pageSize; i++)
            {
                var p = new Person { Name = "Person " + i };
                _people.Add(p);
            }
            result = (from p in _people select p).Skip(count + pageSize).Take(pageSize).AsEnumerable<Person>();
            await Task.Delay(1000);
            return result;
        }
    }
}
