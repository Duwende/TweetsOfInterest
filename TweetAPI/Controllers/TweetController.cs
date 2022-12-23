using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TweetAPI.Models;

namespace TweetAPI.Controllers
{
    public class TweetController : Controller
    {
        public static TweetModel[] GetTweets()
        {
            return new TweetModel[] { };
        }
    }
}
