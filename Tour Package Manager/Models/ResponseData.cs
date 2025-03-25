using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tour_Package_Manager.Models
{
    public class ResponseData
    {
        public string responseMessage { get; set; }
        public int responseCode { get; set; }
        public object data { get; set; }
        public void setResponseData(int responseCodeX, string responseMessageX, object dataX)
        {
            responseMessage = responseMessageX;
            responseCode = responseCodeX;
            data = dataX;
        }
        public void setResponseData(string responseCodeX, string responseMessageX, object dataX)
        {
            responseMessage = responseMessageX;
            responseCode = Convert.ToInt32(responseCodeX);
            data = dataX;
        }
    }
}