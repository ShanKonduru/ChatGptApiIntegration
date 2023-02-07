﻿using System.Text;

if (args.Length > 0) {
    HttpClient client = new HttpClient ();
    // client.DefaultRequestHeaders.Add ("Content-Type", "application/json");
    client.DefaultRequestHeaders.Add ("Authorization", "Bearer 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");
    var content = new StringContent ("{\"model\": \"text-davinci-003\",\"prompt\":\"" + args[0] + "\",\"temperature\": 1,\"max_tokens\": 100,\"top_p\": 1,\"frequency_penalty\": 0.0,\"presence_penalty\": 0.0}",
        Encoding.UTF8, "application/json");
    HttpResponseMessage response = await client.PostAsync ("https://api.openai.com/v1/completions", content);
    Task<string> responseText = response.Content.ReadAsStringAsync();

    Console.WriteLine(responseText.Result);
} else {
    Console.WriteLine ("need at least one question to be asked");
}