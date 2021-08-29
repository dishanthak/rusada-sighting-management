using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Dishantha.WebAPI.Models
{
    public class ResponseModel<T>
    {
        public ResponseModel(T data)
        {
            Succeeded = true;
            if (data == null)
            {
                Message = "No Data Found";
            }
            else
            {
                Message = "Successfully Completed";
            }
            Errors = null;
            Data = data;
            Status = 200;
        }

        public ResponseModel(Exception ex)
        {
            
            Succeeded = false;
            Message = ex.Message;
            Errors = ex.InnerException?.Message;
            Status = 400;
        }

        public int Status { get; set; }
        public bool Succeeded { get; set; }
        public object Errors { get; set; }
        public int ErrorCode { get; set; }
        public T Data { get; set; }
        public string Message { get; set; }


    }
}
