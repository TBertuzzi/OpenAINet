using System.Text;
using Newtonsoft.Json;
using OpenAINet;

var openai = new OpenAI();
openai.ExecutarChat().Wait();

