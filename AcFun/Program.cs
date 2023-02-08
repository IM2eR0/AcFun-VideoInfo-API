namespace AcFun
{
    class Program
    {
        private static string _version = "1.0";
        static void Main() => new Program().MainAsync();
        async void MainAsync()
        {
            Util.Print("AcFun视频信息解析软件 v"+_version);
            Util.Print("作者：初雪 OriginalSnow");
            Util.Print("项目地址：https://github.com/IM2eR0/AcFun-VideoInfo-API");
            Console.WriteLine();

            Util.Print("正在启动 HttpListener 监听...");

            try
            {
                new WebApi().Start();
            }
            catch(Exception ex)
            {
                Util.Print(ex.Message, Util.PrintType.ERROR);
            }
        }
    }
}