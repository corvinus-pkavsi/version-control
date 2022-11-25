using NUnit.Framework;
using System;
using System.Activities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTestExample.Controllers;

namespace UnitTestExample.Test
{
    public class AccountControllerTestFixture
    {
        
         [
            Test,
            TestCase("abcd1234", false),
            TestCase("irf@uni-corvinus", false),
            TestCase("irf.uni-corvinus.hu", false),
            TestCase("irf@uni-corvinus.hu", true),
            TestCase("")
            ]
         public void TestValidateEmail(string email, bool expectedResult) 
        {
            //Arrange            
            var accountController = new AccountController();

            //Act
            var actualResult = accountController.ValidateEmail(email);

            //Assert
            Assert.AreEqual(expectedResult, actualResult);            

        }

        [
            Test,
            TestCase("Password123",true)     
            ]
        public void TestValidatePassword(string password,bool expectedResult) 
        {
            //Arrange
            var accountController = new AccountController();

            //Act
            var actualResult = accountController.ValidatePassword(password);

            //Assert
            Assert.AreEqual(actualResult,expectedResult);
        }

        public void TestRegisterValidateException(string email,string password) 
        {
            // Arrange
            var accountController = new AccountController();

            //Act
            try
            {
                var actualResult = accountController.Register(email, password);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOf<ValidationException>(ex);
            }
        }
        
    }
}
