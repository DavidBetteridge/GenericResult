using System;
using System.Collections.Generic;
using System.Text;

namespace Version1
{
    class Program
    {
        GetCustomerResult GetCustomer(int userID, int id)
        {
            if (userID == 1) return new GetCustomerResult(ResponseType.UnAuthorised, "You don't have access");

            if (id != 1) return new GetCustomerResult(ResponseType.NotFound);

            var customer = new Customer();
            return new GetCustomerResult(ResponseType.OK, customer);
        }
    }

    class Customer { public string Name { get; set; } }

    enum ResponseType
    {
        OK = 1,
        NotFound = 2,
        UnAuthorised = 3
    }

    class GetCustomerResult
    {
        private readonly ResponseType _responseType;
        private readonly object _detail;

        public GetCustomerResult(ResponseType responseType)
        {
            _responseType = responseType;
        }

        public GetCustomerResult(ResponseType responseType, object detail)
        {
            _responseType = responseType;
            _detail = detail;
        }
    }
}
