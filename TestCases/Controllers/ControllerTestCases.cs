using AutoMapper;
using Microsoft.Extensions.Options;
using Moq;
using PlanningPoker.Controllers;
using PlanningPoker.Domain.Models;
using PlanningPoker.Middleware;
using PlanningPoker.Models;
using PlanningPoker.Services;
using PlanningPoker.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace TestCases.Controllers
{
    public class ControllerTestCases
    {

        [Fact]
        public void SendResetMailTestCase()
        {
            var email = "pragyanagar99@gmail.com";
            var mockUserService = new Mock<IUserServices>();
            var iMapper = new Mock<IMapper>();
            var appsettings = new Mock<IOptions<AppSettings>>();
            var emailService = new Mock<IEmailSender>();
            var tokenService = new Mock<ITokenService>();
            var utilities = new Mock<IUtilities>();
            var LoginApplicationController = new LoginController(utilities.Object,
            mockUserService.Object,
            iMapper.Object,
            appsettings.Object,
            tokenService.Object,
            emailService.Object);
            var details = new User()
            {
                Id = 1,
                FirstName = "Pragya",
                Lastname = "Nagar",
                Email = "pragyanagar99@gmail.com"
            };

            mockUserService.Setup(serv => serv.GetByEmail(email)).Returns(details);
            utilities.Setup(res => res.ResetMailAsync(details.Id, email)).Returns(email);

            var result = LoginApplicationController.SendResetMail(email);


            Assert.NotNull(result);

        }
        [Fact]
        public void ResetPasswordTestCase()
        {
            var token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9." +
    "eyJlbWFpbCI6InByYWd5YW5hZ2FyOTlAZ21haWwuY29tIiwibmJmIjoxNTg4NjcyMjc1LCJleHAiOjE1ODkyNzcwNzUsImlhdCI6MTU4ODY3MjI3NX0" +
    ".A89tbD0Fu34oAOrKvKMBZIgiCE67-kgeaDgz-MBUOA0";
            var Email = "pragyanagar99@gmail.com";
            var Id = 2;
            var Id2 = 2;
            var mockUserService = new Mock<IUserServices>();
            var mapper = new Mock<IMapper>();
            var appsettings = new Mock<IOptions<AppSettings>>();
            var emailService = new Mock<IEmailSender>();
            var tokenService = new Mock<ITokenService>();
            var utilities = new Mock<IUtilities>();
            var loginApplicationController = new LoginController(utilities.Object, mockUserService.Object, mapper.Object,
                appsettings.Object, tokenService.Object, emailService.Object);
            var details = new ResetPasswordModel()
            {
                Password = "abcd@1234",
            };
            utilities.Setup(tok => tok.GetSubFromToken(token)).Returns(Email);
            mockUserService.Setup(u => u.UpdatePasswordByEmail(Id, details.Password, token)).Returns(true);


            var result = loginApplicationController.ResetPassword(Id2, token, details);


            Assert.NotNull(result);


        }
        [Fact]
        public void AuthenticateUserTest()
        {
            var details = new AuthenticateModel()
            {
                Email = "pragyanagar",
                Password = "abcd@1234"
            };
            var userdetails = new User()
            {
                Id = 2,
                FirstName = "Pragya",
                Lastname = "Nagar",
                Email = "pragyanagar99@gmail.com"
            };
            var apps = new AppSettings()
            {
                Secret = "Appsecret"
            };
            var mockUserservice = new Mock<IUserServices>();
            var mapper = new Mock<IMapper>();
            var appsettings = new Mock<IOptions<AppSettings>>();

            var emailService = new Mock<IEmailSender>();
            var tokenService = new Mock<ITokenService>();
            var utilities = new Mock<IUtilities>();

            var loginApplicationController = new LoginController(utilities.Object, mockUserservice.Object, mapper.Object, appsettings.Object,
            tokenService.Object, emailService.Object);
            mockUserservice.Setup(auth => auth.Authenticate(details.Email, details.Password)).Returns(userdetails);
            appsettings.Setup(x => x.ToString()).Returns(apps.Secret);


            var result = loginApplicationController.Authenticate(details);


            Assert.NotNull(result);

        }
        [Fact]
        public void RegisterUserTest()
        {
            var userDetails = new RegisterModel()
            {
                FullName = "Pragya Nagar",
                Email = "pragyanagar99@gmail.com",
                Password = "abcd@123"

            };
            var newUser = new User()
            {
                Id = 2,
                FirstName = "Pragya",
                Lastname = "Nagar",
                Email = "pragyanagar99@gmail.com"
            };
            var userServiceMock = new Mock<IUserServices>();
            var mapperMock = new Mock<IMapper>();
            var emailServiceMock = new Mock<IEmailSender>();
            var appSettingsMock = new Mock<IOptions<AppSettings>>();
            var tokenServiceMock = new Mock<ITokenService>();
            var utilitiesMock = new Mock<IUtilities>();
            var loginApplicationController = new LoginController(utilitiesMock.Object, userServiceMock.Object, mapperMock.Object, appSettingsMock.Object,
                tokenServiceMock.Object, emailServiceMock.Object);
            userServiceMock.Setup(c => c.Create(newUser, userDetails.Password, newUser.FirstName, newUser.Lastname)).Returns(newUser);


            var result = loginApplicationController.Register(userDetails);


            Assert.NotNull(result);

        }
        [Fact]
        public void DeleteUserTest()
        {
            var id = 2;
            var userServiceMock = new Mock<IUserServices>();
            var mapperMock = new Mock<IMapper>();
            var emailServiceMock = new Mock<IEmailSender>();
            var appSettingsMock = new Mock<IOptions<AppSettings>>();
            var tokenServiceMock = new Mock<ITokenService>();
            var utilitiesMock = new Mock<IUtilities>();
            var loginApplicationController = new LoginController(utilitiesMock.Object, userServiceMock.Object, mapperMock.Object, appSettingsMock.Object,
                tokenServiceMock.Object, emailServiceMock.Object);
            userServiceMock.Setup(c => c.Delete(id));

            var result = loginApplicationController.Delete(id);


            Assert.NotNull(result);
        }
    }


}




