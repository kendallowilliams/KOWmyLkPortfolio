using DevTools.BLL.Services.Interfaces;
using System;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Text;

namespace DevTools.BLL.Services
{
    [Export(typeof(IConsoleService))]
    public class ConsoleService : IConsoleService
    {
        [ImportingConstructor]
        public ConsoleService()
        {
        }

        public string Execute(string executablePath, string args = null)
        {
            ProcessStartInfo processStartInfo = string.IsNullOrWhiteSpace(args) ? new ProcessStartInfo(executablePath)
            {
                RedirectStandardError = true,
                RedirectStandardOutput = true,
                UseShellExecute = false
            } :
            new ProcessStartInfo(executablePath, args)
            {
                RedirectStandardError = true,
                RedirectStandardOutput = true,
                UseShellExecute = false
            };
            Process process = new Process();
            StringBuilder builder = new StringBuilder();

            process.StartInfo = processStartInfo;
            process.OutputDataReceived += (sender, args) =>
            {
                Debug.WriteLine(args.Data);
                builder.AppendLine(args.Data);
            };
            process.ErrorDataReceived += (sender, args) =>
            {
                Debug.WriteLine(args.Data);
                builder.AppendLine(args.Data);
            };
            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();
            process.WaitForExit();

            builder.AppendLine($"Exit Code: {process.ExitCode}");
            if (process.ExitCode != 0) /*then*/ throw new Exception(builder.ToString());

            return builder.ToString();
        }
    }
}
