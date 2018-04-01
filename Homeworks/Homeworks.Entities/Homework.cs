using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace Homeworks.Entities
{
    public class Homework
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime DueDate { get; set; }
        [NotMapped]
        public int Score {
            get
            {
                return Exercises.Aggregate(0, (sum, e) => sum + e.Score);
            }
            private set {}
        }
        public List<Exercise> Exercises { get; set; }

        public Homework()
        {
            Exercises = new List<Exercise>();
        }
    }
}
