using Newtonsoft.Json;
//using Newtonsoft.Json.Linq;
using System;
using System.IO;
using Tweetinvi;
using Tweetinvi.Json;
using Tweetinvi.Models;
using Tweetinvi.Parameters;

namespace Team06LiveTwitterData
{
    class Program
    {
        static void Main(string[] args)
        {
            //Tweetinvi url: https://www.nuget.org/packages/TweetinviAPI/
            //Install package - <Install-Package TweetinviAPI -Version 2.1.0>
            //User credentials to log into Twitter account
            //Auth.SetUserCredentials(Consumer Key, Consumer Secret, Access Token, Access Token Secret);

            Auth.SetUserCredentials("q5cb9venpnEIhtlzXtoqyrw2P", "qowemzpxCtdtVznQ382ldsnF7Ge9ZsxjYwUkX5UmxYOdtwkLgd", "959855737169235968-1Ko7UHMnNrWM2wMNO8R1eO3F1hQVLti", "KE9kbt91Efry1zACEc0v6CKhszm4cT4fRYB6ShXcpWzh4");
            var user = Tweetinvi.User.GetAuthenticatedUser();

            //Test to see if it's pulling the user
            Console.WriteLine("User name is: " + user);

            //Returns tweets and outputs in json format to console.  The higher the value, the more info that is retrieved.
            var jsonResponseOne = TimelineJson.GetHomeTimeline(3);
            var json = Tweetinvi.JsonSerializer.ToJson(jsonResponseOne);
            Console.WriteLine("\n\nJsonResponseOne is: \t\t\n\n" + json);

            //Write JSON results to file.  
            File.WriteAllText(@"C:\\Users\\JoseT\\Documents\\_JOSE_FILES\\_School_work\\UCF\cen4910\\Test_Folder\\JerrysStuff.txt", JsonConvert.SerializeObject(json));

            //Search parameters
            var searchParameter = new SearchTweetsParameters("dogoargentino")
            {
                GeoCode = new GeoCode(-122.398720, 37.781157, 1, DistanceMeasure.Miles),
                Lang = LanguageFilter.English,
                SearchType = SearchResultType.Popular,
                MaximumNumberOfResults = 5,
                Until = new DateTime(2015, 06, 02),
                SinceId = 399616835892781056,
                MaxId = 405001488843284480,
                Filters = TweetSearchFilters.Images
            };

            //Print search parameters to console and to text
            Console.WriteLine("\nSearch parameter results: \t\t\n" + searchParameter);
            File.WriteAllText(@"C:\\Users\\JoseT\\Documents\\_JOSE_FILES\\_School_work\\UCF\cen4910\\Test_Folder\\SearchResults.txt", JsonConvert.SerializeObject(searchParameter));

            //Match tweets based on search and write to file 
            var matchingTweets = Search.SearchTweets("Red Lobster");
            Console.WriteLine("\nMatching tweet results: \t\t\n" + matchingTweets);
            File.WriteAllText(@"C:\\Users\\JoseT\\Documents\\_JOSE_FILES\\_School_work\\UCF\cen4910\\Test_Folder\\MatchTweets.txt", JsonConvert.SerializeObject(matchingTweets));
            Console.ReadKey();
        }

    }
}


