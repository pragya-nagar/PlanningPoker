using Microsoft.Extensions.Options;
using Moq;
using PlanningPoker.Middleware;
using PlanningPoker.Services;
using PlanningPoker.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace TestCases.Services
{
    public class ServicesTestCases
    {
        [Fact]
        public void GenerateResetTokenTestCases()
        {
            var Id = 2;
            var email = "pragyanagar99@gmail.com";
            var tokenString = "ywgdhsjfndnfkdn";

            var appsMock = new Mock<IOptions<AppSettings>>();
            var tokenMock = new Mock<ITokenService>();

            var emailMock = new Mock<IEmailSender>();
            var utilitiesClass = new Utilities(appsMock.Object, tokenMock.Object, emailMock.Object);
            tokenMock.Setup(a => a.AddForUser(Id, email, tokenString)).Returns(Id);

            var result = utilitiesClass.GenerateResetToken(Id, email);

            Assert.NotNull(result);

        }
        [Fact]
        public void ResetMailAsyncTestCases()
        {
            var Id = 2;
            var email = "pragyanagar99@gmail.com";
            var from = "nagarpragay77@gmail.com";
            var subject = "Reset Password";
            var Content = "Reset Token";
            var token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJlbWFpbCI6InByYWd5YW5hZ2FyOTlAZ21haWwuY29tIiwibmJmIjoxNTg4ODQwNTU1LCJleHAiOjE1ODk0NDUzNTUsImlhdCI6MTU4ODg0MDU1NX0.P2vWOAfoAbS64xf8D8xbTg4DrrvloFw-P0Y7bvsM44Y";
            var appsMock = new Mock<IOptions<AppSettings>>();
            var tokenMock = new Mock<ITokenService>();

            var emailMock = new Mock<IEmailSender>();
            var utilities = new Mock<IUtilities>();
            var utilitiesClass = new Utilities(appsMock.Object, tokenMock.Object, emailMock.Object);
            utilities.Setup(g => g.GenerateResetToken(Id, email)).Returns(token);
            emailMock.Setup(e => e.SendEmail(email, from, subject, Content)).Returns(email);


            var result = utilitiesClass.ResetMailAsync(Id, email);


            Assert.Null(result);
        }
        [Fact]
        public void GetSubFromTokenTest()
        {
            var token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJlbWFpbCI6InByYWd5YW5hZ2FyOTlAZ21haWwuY29tIiwibmJmIjoxNTg4ODQwNTU1LCJleHAiOjE1ODk0NDUzNTUsImlhdCI6MTU4ODg0MDU1NX0.P2vWOAfoAbS64xf8D8xbTg4DrrvloFw-P0Y7bvsM44Y";
            var appsMock = new Mock<IOptions<AppSettings>>();
            var tokenMock = new Mock<ITokenService>();
            var emailMock = new Mock<IEmailSender>();
            var utilitiesClass = new Utilities(appsMock.Object, tokenMock.Object, emailMock.Object);
            tokenMock.Setup(g => g.GetToken(token)).Returns(token);
            
            var result = utilitiesClass.GetSubFromToken(token);
            
            Assert.NotNull(result);


        }
    }
}
