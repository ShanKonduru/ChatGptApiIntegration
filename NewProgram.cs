using System;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace OpenAI_ChatGPT_Integration {
    class NewProgram {
        static readonly HttpClient client = new HttpClient ();
        static void Main (string[] args) {
            Console.WriteLine ("Enter your text:");
            var inputText = Console.ReadLine ();

            // Define the API endpoint
            // var apiEndpoint = "https://api.openai.com/v1/engines/text-davinci-002/completions";
            // var apiEndpoint = "https://api.openai.com/v1/completions";
            var apiEndpoint = "https://api.openai.com/v1/engines/text-davinci-002/completions";

            // Replace the API key with your own API key
            var masterApiKey = "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";

            var openAIApiKey = new CreateOpenAIApiKey (masterApiKey, apiEndpoint);
            var apiKey = openAIApiKey.CreateAPIKey ("MyAPIKey", "all", DateTime.Parse ("2024-01-01"));
            Console.WriteLine ("API Key: " + apiKey);

            // Define the request body
            var requestBody = new {
                prompt = inputText,
                max_tokens = 100,
                n = 1,
                stop = "",
                temperature = 0.5f
            };

            // Serialize the request body to a JSON string
            var requestJson = JsonConvert.SerializeObject (requestBody);

            // Create the request message
            var request = new HttpRequestMessage {
                Method = HttpMethod.Post,
                RequestUri = new Uri (apiEndpoint),
                Content = new StringContent (requestJson, Encoding.UTF8, "application/json"),
            };

            // Add the API key to the request headers
            request.Headers.Add ("Authorization", $"Bearer {apiKey}");

            // Send the API request
            var response = client.SendAsync (request).Result;

            // Check if the API response is successful
            if (response.IsSuccessStatusCode) {
                // Deserialize the API response to a dynamic object
                var responseJson = response.Content.ReadAsStringAsync ().Result;
                var responseObject = JsonConvert.DeserializeObject<dynamic> (responseJson);

                if (responseObject != null) {
                    // Get the generated text from the API response
                    var generatedText = responseObject.choices[0].text;

                    Console.WriteLine ("Generated text:");
                    Console.WriteLine (generatedText);
                }
            } else {
                Console.WriteLine ("API request failed");
                Console.WriteLine (response.StatusCode);
            }
        }
    }
}