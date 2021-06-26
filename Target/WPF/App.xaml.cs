using System.Windows;
using etwest.Models;

namespace etwest
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static Client Client = new Client();
        // TODO: ここを接続先のURLにする
        public const string EndPoint = "https://etwest.minamo.io/";
    }
}
