namespace RealTimeBoard.Domain.Exteptions;

public class LoginFailedException(string email) : Exception($"Invalid email: {email} or password.");