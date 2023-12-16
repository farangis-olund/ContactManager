namespace AddressBookConsoleApp.Interfaces
{
    public interface IConsoleService
    {
        void Clear();
        ConsoleKeyInfo ReadKey();
        string ReadLine();
        void Write(string value);
        void WriteLine(string value);
    }
}