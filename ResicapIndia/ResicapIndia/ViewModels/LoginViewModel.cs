namespace ResicapIndia.ViewModels
{
    using System;
    using System.Diagnostics;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using Xamarin.Forms;

    using ResicapIndia.Validations;
    using ResicapIndia.ViewModels.Base;

    public class LoginViewModel : ViewModelBase
    {
        private bool _isValid;
        public bool IsValid
        {
            get
            {
                return _isValid;
            }
            set
            {
                _isValid = value;
                RaisePropertyChanged(() => IsValid);
            }
        }

        private ValidatableObject<string> _userName;
        public ValidatableObject<string> UserName
        {
            get
            {
                return _userName;
            }
            set
            {
                _userName = value;
                RaisePropertyChanged(() => UserName);
            }
        }

        private ValidatableObject<string> _password;
        public ValidatableObject<string> Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
                RaisePropertyChanged(() => Password);
            }
        }

        public LoginViewModel()
        {
            _userName = new ValidatableObject<string>();
            _password = new ValidatableObject<string>();

            AddValidations();
        }

        public ICommand LoginCommand => new Command(async () => await LoginCommandAsync());

        public ICommand ForgotPasswordCommand => new Command(async () => await ForgotPasswordCommandAsync());

        public ICommand UserAdderssCommend => new Command(async () => await UserAdderssCommendAsync());

        public ICommand ValidateUserNameCommand => new Command(() => ValidateUserName());

        public ICommand ValidatePasswordCommand => new Command(() => ValidatePassword());

        private async Task LoginCommandAsync()
        {
            IsValid = true;
            bool isValid = Validate();

            if (isValid)
            {
                try
                {
                    await DialogService.ShowAlertAsync("Congratulations, you have logged in successfully!", "Success", "OK");
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"[SignIn] Error signing in: {ex}");
                }
            }
            else
            {
                IsValid = false;
            }
        }

        private async Task ForgotPasswordCommandAsync()
        {
            await DialogService.ShowAlertAsync("Check your inbox for a password reset email.", "Forgot Password", "OK");
        }

        private async Task UserAdderssCommendAsync()
        {
            await NavigationService.NavigateToAsync<UserListViewModel>();
        }

        private void AddValidations()
        {
            _userName.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "A username is required." });
            _password.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "A password is required." });
        }

        private bool Validate()
        {
            bool isValidUser = ValidateUserName();
            bool isValidPassword = ValidatePassword();

            return isValidUser && isValidPassword;
        }

        private bool ValidateUserName()
        {
            return _userName.Validate();
        }

        private bool ValidatePassword()
        {
            return _password.Validate();
        }
    }
}
