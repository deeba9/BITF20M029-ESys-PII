using System.ComponentModel.DataAnnotations;

namespace SIS_2_.Models.Domain
{
    public class Student
    {
        public int Id { get; set; }
        [Required]
        public required string FullName { get; set; }
        [Required]
        public required string RollNumber { get; set; }
        [Required]

        public required string Email { get; set; }
        [Required]
        public required string Gender { get; set; }
        [Required]
        public required string DateOfBirth { get; set; }
        [Required]
        public required string City { get; set; }
        [Required]
        public string InterestName { get; set; }
       
        public required string Department { get; set; }
        [Required]
        public required string DegreeTitle { get; set; }
        [Required]
        public required string Subject { get; set; }
        [Required]
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }

    }
    public class Interest
    {
        public int Id { get; set; }

        [Required]
        public required string InterestName { get; set; }
    }
    public class Account
    {
        [Key] // This attribute defines the primary key
        public int AccountId { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

    }

    public class ActivityLog
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public DateTime Timestamp { get; set; }
        public string ActionType { get; set; }
        // Add other fields as needed
    }

}
