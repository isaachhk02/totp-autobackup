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
        
        
        
        string outputDir = Environment.GetEnvironmentVariable("HOME") + "/.config/totp-autobackup/dir";
        string minSel = Environment.GetEnvironmentVariable("HOME") + "/.config/totp-autobackup/minute";

        if (!Directory.Exists(Environment.GetEnvironmentVariable("HOME") + "/.config/totp-autobackup"))
        {
            Directory.CreateDirectory(Environment.GetEnvironmentVariable("HOME") + "/.config/totp-autobackup");
        }
        else
        {
            Console.WriteLine("Config founded on: " + Environment.GetEnvironmentVariable("HOME") +
                              "/.config/totp-autobackup");
        }
        
        if (args.Length <= 0)
        {
            Console.Write("totp-autobackup OPTION \n" +
                          "OPTIONS:\n" +
                          "enable: Enable the service\n" +
                          "disable: Disable the service\n");
        }
        else if (args[0] == "--help")
        {
            Console.Write("totp-autobackup OPTION \n" +
                          "OPTIONS:\n" +
                          "enable: Enable the service\n" +
                          "disable: Disable the service\n");
        }
        else if (args[0] == "--enable")
        {
            if (Environment.GetEnvironmentVariable("EUID") != "0")
            {
                if (File.Exists("/usr/lib/systemd/systemd"))
                {
                    ProcessStartInfo cmd = new ProcessStartInfo
                    {
                        Arguments = "/bin/bash -c 'systemctl enable --now totp-autobackup.service'",
                        CreateNoWindow = false,
                        FileName = "/usr/bin/sudo",
                        UserName = Environment.GetEnvironmentVariable("USER"),
                        WindowStyle = ProcessWindowStyle.Normal,
                        UseShellExecute = true,
                    };
                    Process.Start(cmd);
                }
                else
                {
                    Console.WriteLine("Eh ahi la importancia de systemd :p");
                }
                
            }
            else
            {
                ProcessStartInfo cmd = new ProcessStartInfo
                {
                    Arguments = "-c 'systemctl enable --now totp-autobackup.service'",
                    CreateNoWindow = false,
                    FileName = "/bin/bash",
                    UserName = Environment.GetEnvironmentVariable("USER"),
                    WindowStyle = ProcessWindowStyle.Normal,
                };
                Process.Start(cmd);

            }
            
            if (args[0] == "--disable")
                if (Environment.GetEnvironmentVariable("EUID") != "0")
                {
                    ProcessStartInfo cmd = new ProcessStartInfo
                    {
                        Arguments = "/bin/bash -c 'systemctl disable --now totp-autobackup.service'",
                        CreateNoWindow = false,
                        FileName = "/usr/bin/sudo",
                        UserName = Environment.GetEnvironmentVariable("USER"),
                        WindowStyle = ProcessWindowStyle.Normal,
                        UseShellExecute = true,
                    };
                    Process.Start(cmd);
                }
                else
                {
                    ProcessStartInfo cmd = new ProcessStartInfo
                    {
                        Arguments = "-c 'systemctl disable --now totp-autobackup.service'",
                        CreateNoWindow = false,
                        FileName = "/bin/bash",
                        UserName = Environment.GetEnvironmentVariable("USER"),
                        WindowStyle = ProcessWindowStyle.Normal,
                    };
                    Process.Start(cmd);
                }

            if (args[0] == "--run")
            {
                
                if (!File.Exists(outputDir)  || !File.Exists(minSel))
                    File.Create(outputDir); ; File.Create(minSel);
                try
                {
                    outputDir = File.ReadAllText(Environment.GetEnvironmentVariable("HOME") + "/.config/totp-autobackup/dir");
                    minSel = File.ReadAllText(Environment.GetEnvironmentVariable("HOME") + "/.config/totp-autobackup/minute");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                
                
                int minute = Int32.Parse(minSel);
                int min = DateTime.Now.Minute + minute;
                if (min >= DateTime.Now.Minute)
                {
                    string date = DateTime.Now.Date.ToShortTimeString();
                    ProcessStartInfo cmd = new ProcessStartInfo
                    {
                        Arguments = Environment.GetEnvironmentVariable("HOME") + "/go/bin/totp-cli" + " dump > " + outputDir + "/backup" + date + ".json",
                        CreateNoWindow = false,
                        FileName = "/usr/bin/sudo",
                        UserName = Environment.GetEnvironmentVariable("USER"),
                        WindowStyle = ProcessWindowStyle.Normal,
                        UseShellExecute = true,
                    };
                    Process.Start(cmd);
                }
            }
        }
    }
}