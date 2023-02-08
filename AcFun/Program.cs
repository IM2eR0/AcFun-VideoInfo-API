namespace AcFun
{
    class Program
    {
        static void Main() => new Program().MainAsync();
        async void MainAsync()
        {
            new WebApi().Start();
        }
    }
}