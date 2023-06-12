using Grpc.Net.Client;
using GreeterClient;

/*
Console.WriteLine("Hello, World!");
Console.WriteLine("Type something");
var str = Console.ReadLine();
Console.WriteLine($"You typed: {str}");
Console.ReadLine();
*/

using var channel = GrpcChannel.ForAddress("http://localhost:5206"); //"http://localhost:5134");

var client = new Greeter.GreeterClient(channel);
Console.Write("Введите имя: ");
string? name = Console.ReadLine();

var helloReply = await client.SayHelloAsync(new HelloRequest { Name = name });
Console.WriteLine($"Response: {helloReply.Message}");

List<string> exits = new List<string>() { "q", "Q", "exit", "quit", "" };

while (true)
{
    Console.Write("Input number: ");
    string numberStr = Console.ReadLine() ?? "";

    if (exits.Contains(numberStr))
    {
        Console.WriteLine($"===================");
        Console.WriteLine($"OK. Goodbye, {name}");
        Console.WriteLine($"===================");
        break;
    }

    if (!string.IsNullOrWhiteSpace(numberStr))
    {
        SquareReply rep = await client.GetPow2Async(new SquareRequest { Input = numberStr });

        Console.WriteLine($"Response: {rep.Output}");

        if (!string.IsNullOrWhiteSpace(rep.Message))
        {
            Console.WriteLine($">>> {rep.Message}");
        }
    }
}