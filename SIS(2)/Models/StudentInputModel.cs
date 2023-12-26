using SIS_2_.Models.Domain;
using System.ComponentModel.DataAnnotations;

namespace SIS_2_.Models
{
    public class StudentInputModel
    {
        public int Id { get; set; }
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
}
