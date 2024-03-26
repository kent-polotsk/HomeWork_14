using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace _1_store
{
    public class StringIsEmptyException : Exception
    {
        public DateTime Time { get; }

        public StringIsEmptyException() : base("Ошибка: пустая строка.") 
        { Time = DateTime.Now; }

    }
}
