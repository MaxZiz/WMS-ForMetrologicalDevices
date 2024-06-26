using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WebApplication5.Controllers;
using WMS.Service.Implementations;
using WMS.Service.Interfaces;
using Moq;
using WMS.Domain.Entities;
using WMS.DAL.Interfaces;
using WMS.Controllers;
using WMS.Domain.Helpers;
using WMS.Domain.Responses;
using WMS.Domain.ViewModels;

namespace TestProject
{
    public class UnitTest
    {

        [Fact]
        public void GetUsersIsRight()
        {
            //Arrange
            BaseResponse<IEnumerable<UserViewModel>> users = new BaseResponse<IEnumerable<UserViewModel>>();
            users.Data = GetTestUsers();
            users.StatusCode = WMS.Domain.Enums.StatusCode.OK;
            var mock = new Mock<IUserService>();
            mock.Setup(repo => repo.GetUsers()).Returns(users);
            var controller = new UserController(mock.Object);

            // Act
            var result = controller.GetUsers();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<UserViewModel>>(viewResult.Model);
            Assert.Equal(GetTestUsers(), model);
        }

        private IEnumerable<UserViewModel> GetTestUsers()
        {
            var users = new List<UserViewModel>
            {
                new UserViewModel { Id=1, Name="Иванов И.И."},
                new UserViewModel { Id=2, Name="Петров П.П."},
                new UserViewModel { Id=3, Name="Александров А.А."}
            };
            return users;
        }

        [Fact]
        public void HashingPasswordIsRight()
        {
            string password = "123456";
            string hashedPassword = HashPassword.GetHashPassword(password);
            Assert.NotEqual(hashedPassword, password);
        }

        [Fact]
        public void HashingPasswordLengthIsRight()
        {
            string password = "123456";
            string hashedPassword = HashPassword.GetHashPassword(password);
            Assert.NotEqual(hashedPassword, password);
        }
    }
}
