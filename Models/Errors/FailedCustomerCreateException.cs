namespace CompanyScheduler.Models.Errors;

class FailedCustomerCreateException : Exception
{
    public int ErrorCode { get; }

    public FailedCustomerCreateException() : base("Failed to Create Customer.") { }

    public FailedCustomerCreateException(string message) : base(message) { }

    public FailedCustomerCreateException(string message, Exception e) : base(message, e) { }

    public FailedCustomerCreateException(string message, int code) : base(message)
    {
        ErrorCode = code;
    }

    public override string ToString()
    {
        return $"{base.ToString()}, ErrorCode: {ErrorCode}";
    }
}