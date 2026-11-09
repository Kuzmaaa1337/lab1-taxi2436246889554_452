namespace TaxiApp;

internal static class Program
{
    /// <summary>
    /// Точка входа в приложение.
    /// </summary>
    [STAThread]
    private static void Main()
    {
        // Настройка стилей, шрифтов и DPI (генерируется автоматически для .NET 6+)
        ApplicationConfiguration.Initialize();
        Application.Run(new MainForm());
    }
}
