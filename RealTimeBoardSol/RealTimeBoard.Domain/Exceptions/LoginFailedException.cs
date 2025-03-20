namespace RealTimeBoard.Domain.Extensions;

public class LoginFailedException(string email) : Exception($"Invalid email: {email} or password.");