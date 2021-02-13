using DevTools.BLL.Models;
using DevTools.BLL.Models.Interfaces;
using DevTools.BLL.Services.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
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

        public string Process(IScaffoldDbContextProfile profile)
        {
            string tempFolder = Path.GetTempPath(),
                   localPath = Path.Combine(tempFolder, Guid.NewGuid().ToString("N")),
                   outputPath = Path.Combine(localPath, "Output"),
                   safeProfileName = new string(profile.Name.Select(ch => Path.GetInvalidFileNameChars().Contains(ch) ? '_' : ch).ToArray()),
                   dateTimeFormat = "yyyyMMdd_HHmmss",
                   outputFile = Path.Combine(outputPath, $"{safeProfileName}_{DateTime.Now.ToString(dateTimeFormat)}.zip");
            StringBuilder builder = new StringBuilder();

            Directory.CreateDirectory(localPath);
            profile.ScaffoldDbContextConfig.Project = Path.Combine(localPath, profile.ScaffoldDbContextConfig.Project);
            profile.ScaffoldDbContextConfig.StartupProject = Path.Combine(localPath, profile.ScaffoldDbContextConfig.StartupProject);
            builder.AppendLine(ScaffoldDbContext(profile.ScaffoldDbContextConfig));
            builder.AppendLine();
            builder.AppendLine(Build(profile, outputPath));

            using (var memoryStream = new MemoryStream())
            {
                using (var ziparchive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
                {
                    foreach (string file in Directory.EnumerateFiles(outputPath))
                    {
                        ziparchive.CreateEntryFromFile(file, Path.GetFileName(file));
                    }
                }

                File.WriteAllBytes(outputFile, memoryStream.ToArray());
            }

            return builder.ToString();
        }

        public string Build(IScaffoldDbContextProfile profile, string outputPath)
        {
            string arguments = $"build \"{profile.ScaffoldDbContextConfig.Project}\" --output \"{outputPath}\"";
            ProcessStartInfo processStartInfo = new ProcessStartInfo("dotnet.exe", arguments)
            {
                RedirectStandardError = true,
                RedirectStandardOutput = true,
                UseShellExecute = false,
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

            return builder.ToString();
        }

        public string ScaffoldDbContext(IScaffoldDbContextConfig config)
        {
            string arguments = $"ef dbcontext scaffold \"{config.ConnectionString}\" {config.Provider} {BuildArgumentList(config)}";
            ProcessStartInfo processStartInfo = new ProcessStartInfo("dotnet.exe", arguments)
            {
                RedirectStandardError = true,
                RedirectStandardOutput = true,
                UseShellExecute = false,
            };
            Process process = new Process();
            StringBuilder builder = new StringBuilder();
            
            if (!ValidateScaffoldDbContextConfig(config, out IEnumerable<string> errors)) /*then*/
                throw new AggregateException(errors.Select(error => new ValidationException(error)));
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

            return builder.ToString();
        }

        private string BuildArgumentList(IScaffoldDbContextConfig config)
        {
            List<string> argumentLists = new List<string>();
            string tableNames = string.Join(" ", (config.Tables ?? Enumerable.Empty<string>()).Select(table => $"-t {table}")),
                   arguments = string.Empty;

            argumentLists.Add($"--project \"{config.Project}\"");
            argumentLists.Add($"--startup-project \"{config.StartupProject}\"");
            if (config.DataAnnotations) /*then*/ argumentLists.Add("--data-annotations");
            argumentLists.Add($"--context {config.Context}");
            argumentLists.Add($"--context-dir \"{config.ContextDir}\"");
            if (!string.IsNullOrWhiteSpace(config.ContextNameSpace)) /*then*/argumentLists.Add($"--context-namespace {config.ContextNameSpace}");
            if (config.Force) /*then*/ argumentLists.Add("--force");
            argumentLists.Add($"--output-dir \"{config.OutputDir}\"");
            if (!string.IsNullOrWhiteSpace(config.NameSpace)) /*then*/argumentLists.Add($"--namespace {config.NameSpace}");
            if (!string.IsNullOrWhiteSpace(config.Schema)) /*then*/ argumentLists.Add($"--schema {config.Schema}");
            argumentLists.Add(tableNames);
            if (config.UseDatabaseNames) /*then*/ argumentLists.Add("--use-database-names");
            if (config.NoOnConfiguring) /*then*/ argumentLists.Add("--no-onconfiguring");
            if (config.NoPluralize) /*then*/ argumentLists.Add("--no-pluralize");
            if (config.Verbose) /*then*/ argumentLists.Add("--verbose");
            arguments = string.Join(" ", argumentLists);

            return arguments;
        }

        public bool ValidateScaffoldDbContextConfig(IScaffoldDbContextConfig config, out IEnumerable<string> errors)
        {
            ValidationContext context = new ValidationContext(config);
            List<ValidationResult> results = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(config, context, results);

            errors = results.Select(error => error.ErrorMessage);

            return isValid;
        }
    }
}
