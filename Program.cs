using System.Data;

// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
// https://gpttools.com/comparisontool
string apikey = "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";


var api = new OpenAI_API.OpenAIAPI(engine: Engine.Davinci);

var result = await api.Completions.CreateCompletionAsync("One Two Three One Two", temperature: 0.1);
Console.WriteLine(result.ToString());

