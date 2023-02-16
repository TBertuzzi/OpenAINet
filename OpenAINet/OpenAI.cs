using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace OpenAINet
{
    public class OpenAI
    {
        string _apiKey = "Coloque aqui sua APIKEy";
        HttpClient _client;

        public OpenAI()
        {
            _client = new HttpClient();
            _client.DefaultRequestHeaders.Add("authorization", $"Bearer {_apiKey} ");
        }

        public async Task ExecutarChat()
        {
            var texto = "";
            while (texto != null && texto?.ToUpper() != "SAIR")
            {

                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("O que voce quer saber? Caso queira sair escreva sair ");

                texto = Console.ReadLine();

                if (texto?.ToUpper() == "SAIR")
                    continue;

                if (!string.IsNullOrEmpty(texto))
                {

                    var content = new StringContent("{\"model\": \"text-davinci-001\", \"prompt\": \"" + texto + "\",\"temperature\": 1,\"max_tokens\": 100}",
                    Encoding.UTF8, "application/json");

                    var response = await _client.PostAsync("https://api.openai.com/v1/completions", content);

                    var responseString = await response.Content.ReadAsStringAsync();

                    try
                    {
                        var repostaOpenIA = JsonConvert.DeserializeObject<RespostaOpenIA>(responseString);

                        if (repostaOpenIA.Id == null)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            var erro = JsonConvert.DeserializeObject<ErroOpenIA>(responseString);
                            Console.WriteLine($"Erro Ao Acessar API:");

                            Console.WriteLine(erro?.Error.Message);
                            Console.WriteLine(erro?.Error.Code);
                            Console.WriteLine(erro?.Error.Param);
                        }
                        else
                        {

                            Console.WriteLine($"Resposta:");
                            foreach (var choice in repostaOpenIA?.Choices)
                            {
                                Console.WriteLine(choice.Text);
                            }
                        }
                        Console.ResetColor();

                    }
                    catch (Exception ex)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"Erro ao vonverter o JSON: {ex.Message}");
                        Console.WriteLine($"Resposta Retornada: {responseString}");
                    }
                }
                else
                {
                    Console.WriteLine("Escreve algo");
                }
            }
        }
    }
}
