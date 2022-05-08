using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.Models
{
    public class Message
    {
        public int Id { get; set; }

        [StringLength(24)]
        public string FromAccountId { get; set; } = null!;

        [StringLength(24)]
        public string ToAccountId { get; set; } = null!;

        public int MessageTypeId { get; set; }

        [StringLength(2048)]
        public string MessageData { get; set; } = null!;

        public MessageType MessageType { get; set; } = null!;
    }
}
