using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auth.Resourse.Models
{
    public class BookRepository
    {
        public List<Book> Books => new List<Book>
        {
            new Book {Id = 1, Author= "A1",Title ="T1", Price = 100},
            new Book {Id = 2, Author= "A2",Title ="T2", Price = 200},
            new Book {Id = 3, Author= "A3",Title ="T3", Price = 300},
            new Book {Id = 4, Author= "A4",Title ="T4", Price = 400}
        };

        public Dictionary<long, int[]> Orders => new Dictionary<long, int[]>
        {
            {4564565324324 , new int[] { 1,3 } },
            {25353245324523 , new int[] { 2, 3, 4 } },
        };
    }
}
