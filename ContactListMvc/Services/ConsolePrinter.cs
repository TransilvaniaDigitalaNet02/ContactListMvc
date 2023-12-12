namespace ContactListMvc.Services
{
    public class ConsolePrinter : IConsolePrinter
    {
        private readonly string _id;

        public ConsolePrinter()
        {
            _id = Guid.NewGuid().ToString();
        }

        public void Print(string message)
        {
            Console.WriteLine($"{_id}: {message}");
        }
    }
}
