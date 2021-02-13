using CommandAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CommandAPI.Tests
{
    public class CommandTest : IDisposable
    {
        Command testCommand;

        public CommandTest()
        {
            //Arrange
            testCommand = new Command
            {
                HowTo = "Do something",
                Platform = "some platform",
                CommandLine = "some command line"
            };
        }

        public void Dispose()
        {
            testCommand = null;
        }

        [Fact]
        public void CanChangeHowTo()
        {
            //Arrange
          
            //Act
            testCommand.HowTo = "Execute Unit Tests";

            //Assert
            Assert.Equal("Execute Unit Tests", testCommand.HowTo);
        }


        [Fact]
        public void CanChangePlatForm()
        {
            //Arrange


            //Act
            testCommand.Platform = "net 4.5";

            //Assert

            Assert.Equal("net 4.5", testCommand.Platform);

        }

        [Fact]
        public void CanChangeCommandLine()
        {
            //Arrange

            //Act
            testCommand.CommandLine = "dotnet remove package";

            //Assert
            Assert.Equal("dotnet remove package", testCommand.CommandLine);
        }
    }
}
