
using System.Collections.Generic;
using CommandLine;

namespace AlmOps.ConsoleApp
{
    public class CommandLineOptions
    {
        public const string VariableSeparator = ":";

        [Value(0, MetaValue = "Action", Required = true, HelpText = "Action (possible values: \"config\", \"list\", \"show\", \"queue\", \"create\").")]
        public string Action { get; set; }

        [Value(1, MetaValue = "Resource", Required = false, HelpText = "Resource (possible values: \"projects\", \"builds\", \"build\", \"release\", \"artifacts\").")]
        public string Resource { get; set; }

        [Option("user", Required = false, HelpText = "Username (usually the email address).")]
        public string Username { get; set; }

        [Option("token", Required = false, HelpText = "Token (PAT = Personal Access Token).")]
        public string Token { get; set; }

        [Option("org", Required = false, HelpText = "Organization name.")]
        public string Organization { get; set; }

        [Option('p', "project", Required = false, HelpText = "Project name.")]
        public string Project { get; set; }

        [Option("id", Required = false, HelpText = "Resource ID.")]
        public string Id { get; set; }

        [Option('n', "name", Required = false, HelpText = "Resource name.")]
        public string Name { get; set; }

        [Option('b', "branch", Required = false, HelpText = "Branch name.")]
        public string Branch { get; set; }

        [Option('t', "tag", Required = false, HelpText = "Tag.")]
        public string Tag { get; set; }

        [Option("var", Required = false, HelpText = "Variables.")]
        public IEnumerable<string> Variables { get; set; }

        [Option('q', "query", Required = false, HelpText = "Information to send back.")]
        public string Query { get; set; }

        [Option('v', "verbose", Required = false, HelpText = "Set output to verbose messages.")]
        public bool IsVerbose { get; set; }
    }
}
