namespace Version3
{
    /*
     * Show typing issue - create supplier class and try returning it - it works
     *   We really want - Result<Customer> GetCustomer(int userID, int id)
     *   class Result<TValue> - lots of errors,  create a new class
     *   we don't want if (id != 1) return Result<Customer>.NotFound();
     *   
     *   Add this function to the program class
     *      Result<Customer> Convert(Result result) => new Result<Customer>(result.ResponseType, result.Detail);
     *      if (userID == 1) return Convert(Result.UnAuthorised("You don't have access"));
     *      
     *  but we can do better than that!    
     *  
     *  public static implicit operator Result<TValue>(Result result) => new Result<TValue>(result.ResponseType, result.Detail);
     *  
     *  but we can still return Result.Ok(supplier),  we really want   return customer;
     *  public static implicit operator Result<TValue>(TValue value) => new Result<TValue>(ResponseType.OK, value);
     *  
     *  return customer;  works
     *  return supplier;  doesn't work
     *  but return Result.Ok(supplier); works :-(
     *  
     *  just delete the overload!
     *  
     *  You can still write return Ok();  
     */

    class Program
    {
        Result<Customer> GetCustomer(int userID, int id)
        {
            if (userID == 1) return Result.UnAuthorised("You don't have access");

            if (id != 1) return Result.NotFound();

            var customer = new Customer();
            var supplier = new Supplier();

            return customer;
        }
    }

    class Customer { public string Name { get; set; } }
    class Supplier { public string Name { get; set; } }

    enum ResponseType
    {
        OK = 1,
        NotFound = 2,
        UnAuthorised = 3
    }

    class Result<TValue>
    {
        public object Detail { get; }
        public ResponseType ResponseType { get; }

        public Result(ResponseType responseType, object detail)
        {
            ResponseType = responseType;
            Detail = detail;
        }

        public static implicit operator Result<TValue>(Result result) => new Result<TValue>(result.ResponseType, result.Detail);
        public static implicit operator Result<TValue>(TValue value) => new Result<TValue>(ResponseType.OK, value);

    }

    class Result
    {
        public object Detail { get; }
        public ResponseType ResponseType { get; }

        public static Result NotFound() => new Result(ResponseType.NotFound);
        public static Result UnAuthorised(string reason) => new Result(ResponseType.UnAuthorised, reason);
        public static Result Ok() => new Result(ResponseType.OK);
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
