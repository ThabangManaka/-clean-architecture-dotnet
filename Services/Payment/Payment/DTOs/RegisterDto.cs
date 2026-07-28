namespace Payment.DTOs
{
    public record RegisterDto
  (
      string FirstName,
      string LastName,
      string Email,
      string Password
  );
}
