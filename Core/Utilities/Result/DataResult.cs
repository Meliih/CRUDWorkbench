using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Result
{
    public class DataResult<T> : Result,IDataResult<T>
    {
        public DataResult(T data,bool sucsess,string message) : base(sucsess,message)
        {
            Data = data;
        }

        public DataResult(T data , bool sucsess):base(sucsess)
        {
            Data = data;
        }
        public T Data { get; }
    }
}
