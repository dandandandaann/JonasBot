namespace JonasBot;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hi, Jonas.");
        // Console.WriteLine("Você mora dentro da baleia.");
        Console.WriteLine("Whatever I write here will show up on screen.");

        Console.WriteLine("Did you understand?");
        var text = Console.ReadLine();

        if(text == "yes")
            Console.WriteLine("Well done.");
        else 
            Console.WriteLine("Wrong answer.");

        Console.ReadKey();
    }
}
