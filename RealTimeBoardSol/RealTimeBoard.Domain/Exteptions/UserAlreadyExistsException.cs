namespace RealTimeBoard.Domain.Exteptions;

public class UserAlreadyExistsException(string email) : Exception($"User with email: {email} already exists");