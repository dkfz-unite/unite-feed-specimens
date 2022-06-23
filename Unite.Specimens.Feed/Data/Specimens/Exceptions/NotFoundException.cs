namespace Unite.Specimens.Feed.Data.Specimens.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException()
    {
    }

    public NotFoundException(string message) : base(message)
    {
    }
}
