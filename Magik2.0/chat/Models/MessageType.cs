using System.ComponentModel.DataAnnotations;

namespace Chat.Models
{
    public class MessageType
    {
        public int Id { get; set; }

        [StringLength(10)]
        public string Name { get; set; } = null!;
    }
}