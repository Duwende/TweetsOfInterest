namespace TweetAPI.Models
{
    public class TweetModel
    {
        public string AuthorID { get; set; }
        public string TweetID { get; set; }
        public string Author { get; set; }
        public string UserName { get; set; }
        public DateTime Created { get; set; }
        public string Text { get; set; }
    }
}
