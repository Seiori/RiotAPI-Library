### A C# Riot API Library

# Introduction
RiotAPI-Library is a Library designed to make it as simple as possible for C# Developers to interact with the Riot API Endpoints.

You can find a list of the Riot API Endpoints here: [Riot Games APIs](https://developer.riotgames.com/apis)

# Installation
You can simply import the RiotAPI.cs file into your project, or install it as a Nuget Package

# Setup
Before you can go ahead and start making requests, you first need to obtain yourself a Riot API Key. To do so, go to the link above and sign up. Doing so should allow you access to a simple Development Key which you can use with this Library.

Once you have your Key, you will need to provide this to RiotAPI class before you can start making requests:
```
RiotAPI.APIKey = "YOUR_API_KEY";
```
This is all the setup required to start making requests.

Now that's out the way, we can begin interacting with the API!

# Making Requests

Let's make your first request. To begin you always start your request with a call to the Library like:
```
RiotAPI.
```
Typing this inside of your program, should now bring up multiple suggested methods. I attempted to name all the methods as close as possible to their functionality, so go ahead and find the one you wish to use. In my case, let's use the CHAMPION-V3 endpoint, and access all the current champions on free rotation! We do thar as follows:
```
RiotAPI.GetChampionRotation(Regions.EUW1);
```
This is all we need to receive a response from this endpoint with our expected data. Depending on the method you choose, will depend on what other parameters you will have to provide. Some are optional and some are required!

# Capturing Response

Now you know how to make a request, but what about actually receiving and accessing the response? Well to do that, it is very simple! Here are some examples below:
```
var response = RiotAPI.GetChampionRotation(Regions.EUW1);
```
or
```
using Newtonsoft.Json.Linq;
JObject response = JObject.Parse(RiotAPI.GetChampionRotation(Regions.EUW1));
```
Sometimes the response may be in the form of an Array, you can capture that as follows:
```
var response = RiotAPI.GetChampionRotation(Regions.EUW1);
```
or
```
using Newtonsoft.Json.Linq;
JArray response = JArray.Parse(RiotAPI.GetChampionRotation(Regions.EUW1));
```

# Response

Lets look at a response example, I will be using this:
```
using Newtonsoft.Json.Linq;
JObject response = JObject.Parse(RiotAPI.GetChampionRotation(Regions.EUW1));
```
The Response:
```csv
JToken,JToken._annotations,JToken.Type,JToken.HasValues,JToken.Path
"""freeChampionIds"": [
  3,
  5,
  9,
  18,
  25,
  30,
  38,
  57,
  58,
  67,
  80,
  112,
  163,
  200,
  238,
  432,
  518,
  523,
  711,
  897
]",Newtonsoft.Json.Linq.JToken+LineInfoAnnotation,Property,True,freeChampionIds
"""freeChampionIdsForNewPlayers"": [
  222,
  254,
  427,
  82,
  131,
  147,
  54,
  17,
  18,
  37
]",Newtonsoft.Json.Linq.JToken+LineInfoAnnotation,Property,True,freeChampionIdsForNewPlayers
"""maxNewPlayerLevel"": 10",Newtonsoft.Json.Linq.JToken+LineInfoAnnotation,Property,True,maxNewPlayerLevel
```

I personally recommend using Newtonsoft.Json.Linq when interacting with the responses from the API. As it makes it super easy to pull data from the returned data with ease and use them as you wish

# Conclusion

Overall, what I want you to take away from this is that this project is not something serious. This library is for my personal use, and I wanted to try at uploading a package for the first time for others to use. If you have any issues or questions feel free to get in touch with me. I would be more then happy to look into it for you or just simply discuss this project or the other one I am using it for. And yeah I hope you find it useful and easy to use, as that is simply my intention.
