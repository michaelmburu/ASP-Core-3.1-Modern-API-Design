using AutoMapper;
using CommandAPI.Controllers;
using CommandAPI.Data;
using CommandAPI.DTOs;
using CommandAPI.Models;
using CommandAPI.Profiles;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xunit;

namespace CommandAPI.Tests
{
    public class CommandControllerTests : IDisposable
    {
        Mock<ICommandAPIRepo> mockRepo;
        CommandsProfile realProfile;
        MapperConfiguration configuration;
        IMapper mapper;
        public CommandControllerTests()
        {
            mockRepo = new Mock<ICommandAPIRepo>();
            realProfile = new CommandsProfile();
            configuration = new MapperConfiguration(cfg => cfg.AddProfile(realProfile));
            mapper = new Mapper(configuration);
        }

        public void Dispose()
        {
            mockRepo = null;
            realProfile = null;
            configuration = null;
            mapper = null;
        }

        [Fact]
        public void GetCommandItems_ReturnsZeroItems_WhenDBIsEmpty()
        {
            //Arrange
            mockRepo.Setup(repo => repo.GetAllCommands()).Returns(GetCommands(0));

            var controller = new CommandsController(mockRepo.Object, mapper);

            //Act
            var result = controller.GetAllCommands();

            //Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

        private List<Command> GetCommands(int num)
        {
            var commands = new List<Command>();
            if(num > 0)
            {
                commands.Add(new Command
                {
                    Id = 0,
                    HowTo = "How to generate a migration",
                    CommandLine = "dotnet ef migrations add <Name of Migration>",
                    Platform = ".Net Core Ef"
                });
            }

            return commands;
        }

        [Fact]
        public void GetAllCommands_ReturnsOneItem_WhenDBHasOneResource()
        {
            //Arrange
            mockRepo.Setup(repo => repo.GetAllCommands()).Returns(GetCommands(1));
            var controller = new CommandsController(mockRepo.Object, mapper);

            //Act
            var result = controller.GetAllCommands();

            //Assert
            var okResult = result.Result as OkObjectResult;

            var commands = okResult.Value as List<CommandReadDTO>;

            Assert.Single(commands);
        }

        [Fact]
        public void GetAllCommands_Returns200Ok_WhenDBHasOneResource()
        {
            //Arrange
            mockRepo.Setup(repo => repo.GetAllCommands()).Returns(GetCommands(1));

            var controller = new CommandsController(mockRepo.Object, mapper);

            //Act
            var result = controller.GetAllCommands();


            //Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void GetAllCommands_ReturnCorrectType_WhenDBHasOneResource()
        {
            //Arrange
            mockRepo.Setup(repo => repo.GetAllCommands()).Returns(GetCommands(1));

            var controller = new CommandsController(mockRepo.Object, mapper);

            //Act
            var result = controller.GetAllCommands();

            //Assert
            Assert.IsType<ActionResult<IEnumerable<CommandReadDTO>>>(result);
        }

        //Check 4040 Not Found HTTP response
        [Fact]
        public void GetCommandByID_Returns404NotFound_WhenNonExisitentIDPRovided()
        {
            //Arrange
            mockRepo.Setup(repo => repo.GetCommandById(0)).Returns(() => null);

            var controller = new CommandsController(mockRepo.Object, mapper);

            //Act
            var result = controller.GetCommandById(1);

            //Assert
            Assert.IsType<NotFoundObjectResult>(result.Result);
        }

        //Check 200 OK HTTP Response
        [Fact]
        public void GetCommandByID_Returns200Ok_WhenValidIDIsProvided()
        {
            //Arrange
            mockRepo.Setup(repo => repo.GetCommandById(1)).Returns(new Command { Id = 1, HowTo = "mock", Platform = "Mock", CommandLine = "Mock" });

            var controller = new CommandsController(mockRepo.Object, mapper);

            //Act
            var result = controller.GetCommandById(1);

            //Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void GetCommandByID_Returns200Ok_WhenValidIDProvided()
        {
            //Arrange
            mockRepo.Setup(repo => repo.GetCommandById(1)).Returns(new Command
            {
                Id = 1,
                HowTo = "mock",
                Platform = "Mock",
                CommandLine = "Mock"
            });

            var controller = new CommandsController(mockRepo.Object, mapper);

            //Act
            var Result = controller.GetCommandById(1);

            //Assert
            Assert.IsType<ActionResult<CommandReadDTO>>(Result);
        }

        //Check if the correct object type is returned
        [Fact]
        public void CreateCommand_ReturnsCorrectResourceType_WhenValidObjectSubmitted()
        {
            //Arrange
            mockRepo.Setup(repo => repo.GetCommandById(1)).Returns(new Command { Id = 1, HowTo = " Mock", Platform = "Mock", CommandLine = "Mock" });
            var controller = new CommandsController(mockRepo.Object, mapper);

            //Act
            var result = controller.CreateCommand(new CommandCreateDTO { });

            //Assert
            Assert.IsType<ActionResult<CommandReadDTO>>(result);
        }


    }
}
