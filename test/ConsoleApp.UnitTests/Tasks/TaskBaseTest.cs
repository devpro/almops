﻿using System.Collections.Generic;
using System.Threading.Tasks;
using AlmOps.ConsoleApp.Tasks;
using FluentAssertions;
using Xunit;

namespace AlmOps.ConsoleApp.UnitTests.Tasks
{
    [Trait("Category", "UnitTests")]
    public class TaskBaseTest
    {
        class DummyTask : TaskBase
        {
            public override Task<string> ExecuteAsync(CommandLineOptions options)
            {
                return null;
            }

            public new Dictionary<string, string> GetVariables(List<string> inputVariables, string separator)
            {
                return base.GetVariables(inputVariables, separator);
            }
        }

        [Fact]
        public void TaskBaseGetVariables_ShouldReturnDictionary()
        {
            var task = new DummyTask();
            var output = task.GetVariables(new List<string> { "toto:titi", "tutu:tyty" }, ":");
            output.Should().NotBeNull();
            output.Should().NotBeEmpty();
            output.Count.Should().Be(2);
            output.Should().ContainKey("toto");
            output["toto"].Should().Be("titi");
            output.Should().ContainKey("tutu");
            output["tutu"].Should().Be("tyty");
        }

        [Fact]
        public void TaskBaseGetVariables_WithSeperatorCharacter_ShouldReturnDictionary()
        {
            var task = new DummyTask();
            var output = task.GetVariables(new List<string> { "connectionString:https://www.google.com", "complexPassword:Pass:Word:123" }, ":");
            output.Should().NotBeNull();
            output.Should().NotBeEmpty();
            output.Count.Should().Be(2);
            output.Should().ContainKey("connectionString");
            output["connectionString"].Should().Be("https://www.google.com");
            output.Should().ContainKey("complexPassword");
            output["complexPassword"].Should().Be("Pass:Word:123");
        }
    }
}
