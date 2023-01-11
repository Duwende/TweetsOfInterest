using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using RestSharp;
using RestSharp.Authenticators;
using TweetUI.Models;

namespace TweetUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            try
            {

                InitializeComponent();

                var TweetList = GetTweets().Result;
                Tweets.ItemsSource = TweetList;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<TweetModel>> GetTweets()
        {
            var client = new RestClient("https://localhost:7175");
            var request = new RestRequest("api/Tweets");
            var TweetResponse = client.Get<List<TweetModel>>(request);

            return TweetResponse;
        }
    }
}
