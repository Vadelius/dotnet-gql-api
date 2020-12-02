using HotChocolate;
using HotChocolate.Types;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.GraphQL.Data
{
    public class Activity
    {
        public int Id { get; set; }

        [Required]
        [StringLength(300)]
        public string? Description { get; set; }

        public DateTimeOffset? StartTime { get; set; }

        public DateTimeOffset? EndTime { get; set; }

        public TimeSpan Duration =>
            EndTime?.Subtract(StartTime ?? EndTime ?? DateTimeOffset.MinValue) ??
                TimeSpan.Zero;

        [Required]
        public int Points { get; set; }

        public bool Verified { get; set; }

        [GraphQLType(typeof(NonNullType<DateType>))]
        public DateTime Created { get; set; }

        [ForeignKey("Exercise")]
        public int ExerciseId { get; set; }
        public Exercise? Exercise { get; set; }

        [ForeignKey("CreatedBy")]
        public int CreatedById { get; set; }
        public User? CreatedBy { get; set; }

    }
}