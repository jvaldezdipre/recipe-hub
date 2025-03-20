namespace api.DTOs
{
    /// <summary>
    /// Data Transfer Object for User information. Used when returning user data through the API
    /// to exclude sensitive information like passwords.
    /// </summary>
    public class UserDto
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string? ProfilePicture { get; set; }
        public string? Bio { get; set; }
    }
}