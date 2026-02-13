using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Helpers
{
    public class GenericRecord<T> where T : class
    {
        private readonly List<T> _list = new List<T>();
        
        public void AddItem(T item) { _list.Add(item); }
        public List<T> ViewData() => _list; 


    }
}
