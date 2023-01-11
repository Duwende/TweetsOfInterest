using Microsoft.AspNetCore.Mvc;
using TweetAPI.Models;
using Tweetinvi;
using Tweetinvi.Models;
using Tweetinvi.Models.V2;

namespace TweetAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TweetsController : ControllerBase
    {
        // GET: api/<TweetsController>
        [HttpGet]
        public async Task<IEnumerable<TweetModel>> GetTweets()
        {
            var appConfig = new ConfigurationBuilder()
                                .AddUserSecrets<Program>()
                                .Build();
            var myApiKey = appConfig["Authentication:Twitter:ApiKey"];
            var myApiKeySecret = appConfig["Authentication:Twitter:ApiKeySecret"];
            var myBearerToken = appConfig["Authentication:Twitter:BearerToken"];

            var appCredentials = new ConsumerOnlyCredentials(myApiKey, myApiKeySecret)
            {
                BearerToken = myBearerToken
            };

            var appClient = new TwitterClient(appCredentials);
            var userClient = new TwitterClient(appCredentials);

            //TODO search terms
            var searchIterator = userClient.SearchV2.GetSearchTweetsV2Iterator("#dotnetcore #azure");
            //var searchIterator = userClient.SearchV2.GetSearchTweetsV2Iterator("#dotnetcore OR #azure");

            List<TweetV2> tweets = new List<TweetV2>();
            var numTweetsFound = 0;
            while (numTweetsFound < 20 && !searchIterator.Completed)
            {
                var searchPage = await searchIterator.NextPageAsync();
                var searchResponse = searchPage.Content;
                tweets.AddRange(searchResponse.Tweets);
                numTweetsFound = tweets.Count;
            }

            List<TweetModel> lstTweetsOfInterest = new List<TweetModel>();

            for (int i = 0; (i < numTweetsFound) && (i < 20); i++)
            {
                TweetV2Response publishedTweet = await appClient.TweetsV2.GetTweetAsync(Convert.ToInt64(tweets[i].Id));

                lstTweetsOfInterest.Add(new TweetModel
                {
                    AuthorID = publishedTweet.Tweet.AuthorId,
                    TweetID = publishedTweet.Tweet.Id,
                    Created = publishedTweet.Tweet.CreatedAt.DateTime,
                    Text = publishedTweet.Tweet.Text
                });
            }

            return lstTweetsOfInterest.ToArray();
        }
    }
}
