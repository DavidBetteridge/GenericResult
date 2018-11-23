using System;

namespace Final
{
    class Program
    {
        static void Main(string[] args)
        {
            Test("admin", 1);
            Test("test", 1);
            Test("admin", 2);

            Console.ReadKey(true);
        }

        private static void Test(string userID, int customerID)
        {
            var response = GetCustomer(userID, customerID);

            switch (response.ResponseType)
            {
                case ResponseType.OK:
                    Console.WriteLine(response.Detail.Name);
                    break;
                case ResponseType.NotFound:
                    Console.WriteLine("Not Found " + response.Reason);
                    break;
                case ResponseType.UnAuthorised:
                    Console.WriteLine("UnAuthorised " + response.Reason ?? "");
                    break;
                default:
                    break;
            }
        }

        static Response<Customer> GetCustomer(string userID, int customerID)
        {
            if (userID != "admin") return Unauthorised("You must be admin");
            if (customerID != 1) return NotFound();

            var customer = new Customer() { Name = "David" };
            var supplier = new Supplier() { Name = "David" };

            return customer;
        }

        private static Response Unauthorised(string reason) => Response.UnAuthorised(reason);
        private static Response NotFound() => Response.NotFound();

    }

    enum ResponseType
    {
        OK = 1,
        NotFound = 2,
        UnAuthorised = 3
    }

    class Response<TValue>
    {
        public TValue Detail { get; }
        public string Reason { get; }
        public ResponseType ResponseType { get; }

        public Response(TValue Detail)
        {
            this.ResponseType = ResponseType.OK;
            this.Detail = Detail;
        }
        public Response(ResponseType responseType, string reason)
        {
            this.ResponseType = responseType;
            this.Reason = reason;
        }

        public static implicit operator Response<TValue>(Response response) => new Response<TValue>(response.ResponseType, response.Reason);
        public static implicit operator Response<TValue>(TValue value) => new Response<TValue>(value);
    }
    class Response
    {
        public string Reason { get; }
        public ResponseType ResponseType { get; }

        public Response(ResponseType responseType)
        {
            this.ResponseType = responseType;
        }
        public Response(ResponseType responseType, string reason)
        {
            this.ResponseType = responseType;
            this.Reason = reason;
        }

        public static Response NotFound() => new Response(ResponseType.NotFound);
        public static Response UnAuthorised(string reason) => new Response(ResponseType.UnAuthorised, reason);
    }

    class Customer
    {
        public string Name { get; set; }
    }

    class Supplier
    {
        public string Name { get; set; }
    }
}
