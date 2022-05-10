using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OBilet.UI.Models
{
    public class RequestViewModel<T>
    {
        public T Data { get; set; }
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
    }
}
