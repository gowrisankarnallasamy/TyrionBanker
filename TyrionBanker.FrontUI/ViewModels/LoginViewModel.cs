using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using TyrionBanker.FrontUI.Commands;
using TyrionBanker.FrontUI.Services;
using Unity.Attributes;

namespace TyrionBanker.FrontUI.ViewModels
{
    public class LoginViewModel : BindableBase
    {

        #region Private Members
        private bool isValidUser = false;
        private string _userid;
        private string _loginFaildText;
        private Visibility _loginFaildVisibility = Visibility.Hidden;
        #endregion

        #region Public Members
        [Dependency]
        public IBankerApiService BankerApiService { get; set; }
        public bool IsSuccess
        {
            get
            {
                return isValidUser;
            }
        }

        public string UserId
        {
            get
            {
                return _userid;
            }
            set
            {
                SetProperty(ref _userid, value);
            }
        }

        public string LoginFaildText
        {
            get
            {
                return _loginFaildText;
            }
            set
            {
                SetProperty(ref _loginFaildText, value);
            }
        }

        public Visibility LoginFaildVisibility
        {
            get
            {
                return _loginFaildVisibility;
            }
            set
            {
                SetProperty(ref _loginFaildVisibility, value);
            }
        }

        public RelayCommand<IHavePassword> OkClickCommand { get; set; }

        public RelayCommand CancelClickCommand { get; set; }
        #endregion

        #region Public constructor

        /// <summary>
        /// constructor for user login into data migration tool 
        /// </summary>
        public LoginViewModel()
        {
            OkClickCommand = new RelayCommand<IHavePassword>(() => true, DoLogin);
            CancelClickCommand = new RelayCommand(() => true, OnCancel);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// To validate user credential for login
        /// </summary>
        /// <param name="hasPassword"></param>
        async private void DoLogin(IHavePassword hasPassword)
        {
            try
            {
                var token = await BankerApiService.GetBankerTokenAsync(UserId, hasPassword.Password);
                var roles = await BankerApiService.GetRolesAsync();
                UserId = roles.First();
            }
            catch (Exception ex)
            {

            }
            
        }

        /// <summary>
        /// To close data migration tool
        /// </summary>
        private void OnCancel()
        {
            Application.Current.MainWindow.Close();
        }

        #endregion      

    }
}
