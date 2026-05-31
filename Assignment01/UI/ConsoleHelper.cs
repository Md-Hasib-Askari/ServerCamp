namespace Assignment01.UI;

public static class ConsoleHelper
{
    public static void PrintHeader(string title)
    {
        Console.WriteLine();
        WithColor(
            ConsoleColor.Cyan,
            () =>
            {
                Console.WriteLine("==================================================");
                Console.WriteLine($"  {title}");
                Console.WriteLine("==================================================");
            }
        );
    }

    public static void PrintSuccess(string message) =>
        WithColor(ConsoleColor.Green, () => Console.WriteLine($"  [OK] {message}"));

    public static void PrintError(string message) =>
        WithColor(ConsoleColor.Red, () => Console.WriteLine($"  [X] {message}"));

    public static void PrintInfo(string message) =>
        WithColor(ConsoleColor.Yellow, () => Console.WriteLine($"  -> {message}"));

    public static void PrintSeparator() =>
        WithColor(
            ConsoleColor.DarkGray,
            () => Console.WriteLine("  --------------------------------------------------")
        );

    public static void PrintItem(string item) => Console.WriteLine($"  {item}");

    public static string ReadInput(string prompt)
    {
        Console.Write($"  {prompt}: ");
        return Console.ReadLine()?.Trim() ?? "";
    }

    public static void PressAnyKey()
    {
        Console.WriteLine();
        WithColor(
            ConsoleColor.DarkGray,
            () => Console.WriteLine("  Press any key to return to menu...")
        );
        Console.ReadKey(true);
    }

    // small helper so we don't repeat the reset-color dance everywhere
    private static void WithColor(ConsoleColor color, Action action)
    {
        var prev = Console.ForegroundColor;
        Console.ForegroundColor = color;
        try
        {
            action();
        }
        finally
        {
            Console.ForegroundColor = prev;
        }
    }
}
