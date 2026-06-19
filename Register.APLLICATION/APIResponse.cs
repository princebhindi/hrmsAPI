using System;
using System.Collections.Generic;
using System.Text;

namespace Register.APPLICATION
{
    public class APIResponse<T>
    {
      public  bool Sucess { get; set; }
      public string? Message { get; set; }
      public T? Data { get; set; }
        

    }
}
