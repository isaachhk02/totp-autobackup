using System.Diagnostics;

namespace totp_autobackup;

public class autobackup
{
    public void Backup(string dir, string min)
    {
        int minute_conversion = Int32.Parse(min);
        int minute = DateTime.Now.Minute + minute_conversion;
        if (minute >= DateTime.Now.Minute)
        {
            string date = DateTime.Now.Date.ToShortTimeString();
            ProcessStartInfo cmd = new ProcessStartInfo
            {
                Arguments = "-c " + Environment.GetEnvironmentVariable("HOME") + "/go/bin/totp-cli" + " dump > " + dir + "/backup" + date + ".json",
                CreateNoWindow = false,
                FileName = "/bin/bash",
                UserName = Environment.GetEnvironmentVariable("USER"),
                WindowStyle = ProcessWindowStyle.Normal,
                UseShellExecute = true,
            };
            Process.Start(cmd);
        }
    }
}