namespace Version2
{
    /*
     * Add Static Methods
     * Rename to 'Result'
     */

    class Program
    {
        Result GetCustomer(int userID, int id)
        {
            if (userID == 1) return Result.UnAuthorised("You don't have access");

            if (id != 1) return Result.NotFound();

            var customer = new Customer();
            return Result.Ok(customer);
        }
    }

    class Customer { public string Name { get; set; } }

    enum ResponseType
    {
        OK = 1,
        NotFound = 2,
        UnAuthorised = 3
    }

    class Result
    {
        public object Detail { get; }
        public ResponseType ResponseType { get; }


        public static Result NotFound() => new Result(ResponseType.NotFound);
        public static Result UnAuthorised(string reason) => new Result(ResponseType.UnAuthorised, reason);
        public static Result Ok(object detail) => new Result(ResponseType.OK, detail);
        public Result(ResponseType responseType)
        {
            ResponseType = responseType;
        }

        public Result(ResponseType responseType, object detail)
        {
            ResponseType = responseType;
            Detail = detail;
        }
    }
}
