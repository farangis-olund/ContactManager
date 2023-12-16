using AddressBookConsoleApp.Interfaces;

namespace AddressBookConsoleApp.Services;

public class ConsoleService : IConsoleService
{
    public string ReadLine()
    {
        return Console.ReadLine()!;
    }

    public void WriteLine(string value)
    {
        Console.WriteLine(value);
    }
    public void Write(string value)
    {
        Console.Write(value);
    }

    public ConsoleKeyInfo ReadKey()
    {
        return Console.ReadKey();
    }

    public void Clear()
    {
        Console.Clear();
    }

}
