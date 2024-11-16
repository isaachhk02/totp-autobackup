using System.Collections.Specialized;
using System.Diagnostics;

namespace totp_autobackup;

public class Autobackup
{
    public void Start(string[] strings)
    {
        var configdir = Environment.GetEnvironmentVariable("HOME") + "/.config/totp-autobackup";
        if (!Directory.Exists(configdir))
        {
            Directory.CreateDirectory(configdir);
        }

        if (strings.Length <= 0)
            Console.Write("totp-autobackup OPTION \n" +
                          "OPTIONS:\n" +
                          "enable: Enable the service\n" +
                          "disable: Disable the service\n");
        else
            Console.Write("totp-autobackup OPTION \n" +
                          "OPTIONS:\n" +
                          "enable: Enable the service\n" +
                          "disable: Disable the service\n");

        if (strings[0] == "--enable")
        {
            if (Environment.GetEnvironmentVariable("EUID") != "0")
            {
                if (File.Exists("/usr/lib/systemd/systemd"))
                {
                    Process.Start("/usr/bin/sudo", "systemctl enable --now totp-autobackup.service");
                }
                else
                {
                    Console.WriteLine("Eh ahi la importancia de systemd :p");
                }

            }
            else
            {
                Process.Start("/usr/bin/sudo", "systemctl disable --now totp-autobackup.service");
            }
        }

        if (strings[0] == "--disable")
        {
            if (Environment.GetEnvironmentVariable("EUID") != "0")
            {
                Process.Start("/usr/bin/sudo", "systemctl disable --now totp-autobackup.service");
            }
            else
            {
                Process.Start("/usr/bin/sudo", "systemctl disable --now totp-autobackup.service");
            }
        }

        if (strings[0] == "--run")
        {
            bool isRunning = true;
            var outputdir = Environment.GetEnvironmentVariable("HOME") + "/.config/totp-autobackup/dir";
            var minsel = Environment.GetEnvironmentVariable("HOME") + "/.config/totp-autobackup/minute";
            var pass = Environment.GetEnvironmentVariable("HOME") + "/.config/totp-autobackup/password";

            var password = File.ReadAllText(pass);
            var min = File.ReadAllText(minsel);
            var directory = File.ReadAllText(outputdir);
            int min_final = Int32.Parse(min);
            var final_conv = DateTime.Now.Minute + min_final;

            if (!File.Exists(outputdir) && !File.Exists(minsel) && !File.Exists(pass))
            {
                File.Create(outputdir);
                File.Create(minsel);
                File.Create(pass);
            }
            else
            {
                Console.Write($"Settings:\nTarget backup dir: {directory}\nNext backup: {DateTime.Now.Hour + ":" + final_conv}\n Password: ******");
            }



            int backups = 0;
            while (isRunning)
            {
                if (DateTime.Now.Minute == final_conv)
                {
                    
                    Console.WriteLine("Creating backup!");
                    backups = backups + 1;
                    File.Open($"{directory}backup{backups}.json",FileMode.OpenOrCreate,FileAccess.ReadWrite);
                    Process info = new();
                    info.StartInfo.FileName = Environment.GetEnvironmentVariable("HOME") + "/go/bin/totp-cli";
                    info.StartInfo.Arguments = $"dump --yes-please --output {directory}backup{backups}.json";
                    info.StartInfo.RedirectStandardInput = true;
                    info.StartInfo.RedirectStandardOutput = true;
                    info.Start();
                    info.StandardInput.WriteLine(password);
                    info.StandardInput.Flush();
                    info.StandardInput.Close();
                    
                    if (!File.Exists(directory + $"backup{backups}.json"))
                        Console.WriteLine("ERROR: The backup doesn't created!");
                    else
                        Console.WriteLine("INFO: Backup created successfully!");
                    final_conv = DateTime.Now.Minute + min_final;
                    //Console.Clear();
                }

                Thread.Sleep(5000);
            }
        }
    }
}
