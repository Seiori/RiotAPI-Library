# C# Riot API Library

This is a Simple C# Class Library which Aims to make interacting with the Riot API Endpoints as easy as possible

It contains a Method for Every Single Riot API End Point currently Accessibly.

# Making Requests

IMPORTANT: Make sure before you attempt any requests, you set the API KEY variable. To do this simple do as follows:
    - "RiotAPI.APIKey = "YOUR_API_KEY";
Once you do this, you should have no issues accessing any of the methods and obtaining responses.

To make a Request using this, simply start by importing this into your project, or installing it using Nuget Package Manager.

Once that is done, you can go ahead and get started:
    - Firstly begin by calling to the class as follows "RiotAPI.~". Doing this should bring up a list of Methods for you to Call to.
    - Next, simply select which Method you wish to access, the naming should be as clear as possible to their Riot API Endpoint Counterpart.
    - Once selected, simply insert your data parameters into the Method as necessary.
    - Finally, make sure to catch the response. In this case it will always be a string. You can catch it simply using a "var", or you can take advantage of Newtonsoft.JSON and do something like JObject response = JObject.Parse(RiotAPI.RandomMethod(EUW1, "Seiori")); Using a JObject like this will allow you to easily read all of the elements within the response. If you get an error using that, try changing to a JArray. Responses will always come in one of the two ways.

Other then that, you should be sorted! Like I said although this Library is simply for personal use. I thought I would share it to make it easier for others too! Feel free to contact me if you have any issues, or adapt it however you like.

PS: I currently only use the League of Legends Endpoints, so the Valorant/TFT/LOR ones might have issues or might be fine. If you encounter any feel free to let me know and I can look into it. I am unable to test a lot of them at the moment due to accessibility. Some methods will be accessible based on your API KEY!