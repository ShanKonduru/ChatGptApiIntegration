using System.Text;

if (args.Length > 0) {
    HttpClient client = new HttpClient ();
    client.DefaultRequestHeaders.Add ("Content-Type", "application/json");
    client.DefaultRequestHeaders.Add ("Authorization", "Bearer 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");
    var content = new StringContent ("{\"model\": \"text-davinci-001\",\"prompt\":\"" + args[0] + "\",\"temperature\": 1,\"max_tokens\": 100,\"top_p\": 1,\"frequency_penalty\": 0.0,\"presence_penalty\": 0.0}",
        Encoding.UTF8, "application/json");
    HttpResponseMessage response = await client.PostAsync ("https://api.openai.com/v1/completions", content);
    Task<string> responseText = response.Content.ReadAsStringAsync();

    Console.WriteLine(responseText.ToString());
} else {
    Console.WriteLine ("need at least one question to be asked");
}