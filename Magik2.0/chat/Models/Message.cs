namespace Chat.Models
{
    public class Message
    {
        public int ProfileId { get; set; }
        public string Username { get; set; } = null!;
        public string Icon { get; set; } = null!;
        public DateTime Date { get; set; }
        public string Text { get; set; } = null!;
    }
}

/*
profileId:number,
username:number,
icon:string,
date:Date,
text:string
*/
