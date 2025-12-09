using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace SignUpApp
{
    [TestFixture]
    internal class InputTest
    {
        [Test]
        public void CheckValidBirthDate_Valid()
        {
            string birthDate = "01:03:2001";
            Form1 form = new Form1();
            bool expected = false;
            bool actual = form.CheckValidBirthDate(birthDate);
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void CheckValidPassword_Valid()
        {
            //To pass the test, the password must contain at least 8 characters
            //, including at least one uppercase letter, one lowercase letter, one number, and one special character.
            //So this password is not valid
            string password = "Abc12345";
            Form1 form = new Form1();
            bool expected = false;
            bool actual = form.CheckValidPassword(password);
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void IsPasswordsMatched_Valid()
        {
            string password = "123456";
            string confirmPassword = "123456";
            Form1 form = new Form1();
            bool expected = true;
            bool actual = form.IsPasswordsMatched(password, confirmPassword);
            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}
