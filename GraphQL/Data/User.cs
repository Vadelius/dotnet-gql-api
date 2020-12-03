using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

    namespace Server.GraphQL.Data
    {
        public class User
        {
        public int Id { get; set; }

        [Required]
        [StringLength(30)]
        public string? Username { get; set; }

        [Required]
        [StringLength(100)]
        public string? Password { get; set; }

        [Required]
        [StringLength(50)]
        public string? Name { get; set; }
        public int Experience { get; set; }
        public int Points { get; set; }
        public ICollection<Activity> Activities { get; set; } = new List<Activity>();
        public ICollection<Exercise> Exercises { get; set; } = new List<Exercise>();
    }
}