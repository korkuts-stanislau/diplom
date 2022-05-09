using System.ComponentModel.DataAnnotations;

namespace Resource.Models
{
    public class Contact
    {
        public int Id { get; set; }

        public int FirstProfileId { get; set; }
        
        public int? SecondProfileId { get; set; }
        
        public bool Accepted { get; set; }

        public Profile? FirstProfile { get; set; }
        public Profile? SecondProfile { get; set; }
    }
}
