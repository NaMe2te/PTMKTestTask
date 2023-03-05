namespace PTMKTest.Console.Exseptions;

public class InvalidUserParametersException : Exception
{
    public InvalidUserParametersException()
    : base("User parameters were entered incorrectly") { }
}