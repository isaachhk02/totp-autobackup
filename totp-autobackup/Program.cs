/*
 * Created by isaachhk02
 */


using System.Diagnostics;
using Console = System.Console;

namespace totp_autobackup;

class Program
{
    private static void Main(string[] args)
    {
        var backups = new Autobackup();
        backups.Start(args);
    }
}
