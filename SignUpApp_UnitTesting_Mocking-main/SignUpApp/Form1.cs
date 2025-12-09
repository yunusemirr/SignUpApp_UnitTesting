using System;
using System.Globalization;
using System.Windows.Forms;

namespace SignUpApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void buttonSubmit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtFirstName.Text) || string.IsNullOrEmpty(txtLastName.Text) || string.IsNullOrEmpty(txtMail.Text)
                || string.IsNullOrEmpty(txtPassword.Text) || string.IsNullOrEmpty(txtConfirmPassword.Text) || string.IsNullOrEmpty(txtBirth.Text))
            {
                MessageBox.Show("Please fill in all fields", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!CheckValidBirthDate(txtBirth.Text))
            {
                return;
            }
            if (!CheckValidMail(txtMail.Text))
            {
                return;
            }
            if (!CheckValidPassword(txtPassword.Text))
            {
                return;
            }
            if(txtPassword.Text != txtConfirmPassword.Text)
            {
                MessageBox.Show("Passwords do not match !!!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            IUserRepository userRepository = new UserRepository();
            bool result = await userRepository.Insert(new User()
            {
                FirstName = txtFirstName.Text,
                LastName = txtLastName.Text,
                EMail = txtMail.Text,
                Password = txtPassword.Text,
                BirthDate = txtBirth.Text
            });
            if (result)
            {
                ShowUserInformation(new User()
                {
                    FirstName = txtFirstName.Text,
                    LastName = txtLastName.Text,
                    EMail = txtMail.Text
                });
            }
            else
            {
                MessageBox.Show("User could not be added", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public bool CheckValidPassword(string password)
        {
            // Password must contain at least 8 characters, 1 uppercase letter, 1 lowercase letter, 1 number and 1 special character
            password = txtPassword.Text;
            bool hasNumber = false;
            bool hasUpper = false;
            bool hasLower = false;
            bool hasSpecial = false;

            foreach (char c in password)
            {
                if (char.IsDigit(c))
                {
                    hasNumber = true;
                }
                else if (char.IsUpper(c))
                {
                    hasUpper = true;
                }
                else if (char.IsLower(c))
                {
                    hasLower = true;
                }
                else if (char.IsSymbol(c) || char.IsPunctuation(c))
                {
                    hasSpecial = true;
                }
            }
            if (password.Length < 8 || !hasNumber || !hasUpper || !hasLower || !hasSpecial)
            {
                MessageBox.Show("Password must contain at least 8 characters, 1 uppercase letter, 1 lowercase letter, 1 number and 1 special character", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        public bool IsPasswordsMatched(string password, string confirmPassword)
        {
            if (password != confirmPassword)
                return false;

            else
            return true;
        }

        public bool CheckValidBirthDate(string birthDate)
        {
            if (!string.IsNullOrEmpty(birthDate))
            {
                DateTime date;
                if (DateTime.TryParseExact(birthDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
                {
                    if (date > DateTime.Now)
                    {
                        MessageBox.Show("Birth date cannot be greater than today", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                    return true;
                }
                else
                {
                    MessageBox.Show("Invalid date format, please use dd/MM/yyyy format", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }
            else
            {
                MessageBox.Show("Please enter a birth date", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
        }

        public bool CheckValidMail(string mail)
        {
            if (!string.IsNullOrEmpty(mail))
            {
                if (mail.Length < 5)
                {
                    return CheckValidTextLength(mail);
                }
                if (!mail.Contains("@") || !mail.Contains("."))
                {
                    MessageBox.Show("Invalid e-mail format", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                return true;
            }
            else
            {
                MessageBox.Show("Please enter an e-mail", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
        }

        public bool CheckValidTextLength(string text)
        {
            if (text.Length < 5)
            {
                MessageBox.Show("Name and surname must contain at least 2 characters", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private void ShowUserInformation(User user)
        {
            MessageBox.Show($"First Name: {user.FirstName}\nLast Name: {user.LastName}\n" +
                $"E-Mail: {user.EMail} added.", "User Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        
    }
}
