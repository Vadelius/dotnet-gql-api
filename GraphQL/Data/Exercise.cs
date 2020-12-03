using HotChocolate;
using HotChocolate.Types;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.GraphQL.Data
{
    public class Exercise
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string? Name { get; set; }

        [Required]
        [StringLength(300)]
        public string? Description { get; set; }

        [Required]
        public double Multiplier { get; set; }
        public bool Verified { get; set; }

        [GraphQLType(typeof(NonNullType<DateType>))]
        public DateTime Created { get; set; }
        public int CreatedById { get; set; }
        public User? CreatedBy { get; set; }
        public ICollection<Activity> Activities { get; set; } = new List<Activity>();

    }
}