namespace api.DTOs
{
    //This is the output DTO class for the user
    public class UserDto
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string? ProfilePicture { get; set; }
        public string? Bio { get; set; }
    }
}