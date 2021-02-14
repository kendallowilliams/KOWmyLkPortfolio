using DevTools.BLL.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;

namespace DevTools.BLL.Models
{
    public class ScaffoldDbContextProfile : IScaffoldDbContextProfile
    {
        public ScaffoldDbContextProfile()
        {
            ScaffoldDbContextConfig = new ScaffoldDbContextConfig();
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string ProjectLocation { get; set; }

        [Required]
        public string StartupProjectLocation { get; set; }

        public bool Verbose { get; set; }

        [Required]
        public string BuildConfiguration { get; set; }

        public ScaffoldDbContextConfig ScaffoldDbContextConfig { get; set; }

        public bool IsValid(out IEnumerable<string> errors)
        {
            ValidationContext context = new ValidationContext(this);
            List<ValidationResult> results = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(this, context, results);

            errors = results.Select(error => error.ErrorMessage);

            return isValid;
        }

        public string GetProjectBuildArguments(string outputPath)
        {
            IEnumerable<string> args = Enumerable.Empty<string>();

            args = args.Append($"build \"{ProjectLocation}\"");
            args = args.Append($"--output \"{outputPath}\"");
            args = args.Append($"--configuration {BuildConfiguration}");
            if (Verbose) /*then*/ args = args.Append($"--verbose");

            return string.Join(" ", args);
        }

        public string GetStartupProjectBuildArguments(string outputPath)
        {
            IEnumerable<string> args = Enumerable.Empty<string>();

            args = args.Append($"build \"{StartupProjectLocation}\"");
            args = args.Append($"--output \"{outputPath}\"");
            args = args.Append($"--configuration {BuildConfiguration}");
            if (Verbose) /*then*/ args = args.Append($"--verbose");

            return string.Join(" ", args);
        }

        public string GetProjectCleanArguments()
        {
            IEnumerable<string> args = Enumerable.Empty<string>();

            args = args.Append($"clean \"{ProjectLocation}\"");
            args = args.Append($"--configuration {BuildConfiguration}");
            if (Verbose) /*then*/ args = args.Append($"--verbosity detailed");

            return string.Join(" ", args);
        }

        public string GetStartupProjectCleanArguments()
        {
            IEnumerable<string> args = Enumerable.Empty<string>();

            args = args.Append($"clean \"{StartupProjectLocation}\"");
            args = args.Append($"--configuration {BuildConfiguration}");
            if (Verbose) /*then*/ args = args.Append($"--verbosity detailed");

            return string.Join(" ", args);
        }
    }
}
