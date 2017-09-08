using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Auth;
using System.Linq;

namespace ComicBook
{
	public partial class MainPage : ContentPage
	{
		const string 	ServiceId  = "ComicBook";
		const string 	Scope	   = "profile";

		Account account;
		AccountStore store;

		public MainPage ()
		{
			InitializeComponent ();

			implicitButton.Clicked += ImplicitButtonClicked;
			authorizationCodeButton.Clicked += AuthorizationCodeButtonClicked;
			getProfileButton.Clicked += GetProfileButtonClicked;
			refreshButton.Clicked += RefreshButtonClicked;

			store = AccountStore.Create ();
			account = store.FindAccountsForService (ServiceId).FirstOrDefault ();
		}

		void ImplicitButtonClicked (object sender, EventArgs e)
		{
			var authenticator = new OAuth2Authenticator
				(
					ServerInfo.ClientId,
					Scope,
					ServerInfo.AuthorizationEndpoint,
					ServerInfo.RedirectionEndpoint
				);

			authenticator.Completed += OnAuthCompleted;
			authenticator.Error     += OnAuthError;

			var presenter = new OAuthLoginPresenter ();
			presenter.Login (authenticator);
		}
			
		void AuthorizationCodeButtonClicked (object sender, EventArgs e)
		{
			var authenticator = new OAuth2Authenticator
				(
					ServerInfo.ClientId,
					ServerInfo.ClientSecret,
					Scope,
					ServerInfo.AuthorizationEndpoint,
					ServerInfo.RedirectionEndpoint,
					ServerInfo.TokenEndpoint
				);

			authenticator.Completed += OnAuthCompleted;
			authenticator.Error     += OnAuthError;

			var presenter = new OAuthLoginPresenter ();
			presenter.Login (authenticator);
		}

		void GetProfileButtonClicked (object sender, EventArgs e)
		{
		}

		void RefreshButtonClicked (object sender, EventArgs e)
		{
		}

		void OnAuthCompleted (object sender, AuthenticatorCompletedEventArgs e)
		{
			var authenticator = sender as OAuth2Authenticator;

			if (authenticator != null) 
			{
				authenticator.Completed -= OnAuthCompleted;
				authenticator.Error 	-= OnAuthError;
			}

			if (e.IsAuthenticated)
			{
				if (this.account != null)
					store.Delete (this.account, ServiceId);

				store.Save(account = e.Account, ServiceId);

				statusText.Text = "Authentication succeeded";
			}
			else
			{
				statusText.Text = "Authentication failed";
			}
		}

		void OnAuthError (object sender, AuthenticatorErrorEventArgs e)
		{
			var authenticator = sender as OAuth2Authenticator;

			if (authenticator != null) 
			{
				authenticator.Completed -= OnAuthCompleted;
				authenticator.Error 	-= OnAuthError;
			}

			statusText.Text = "Authentication error: " + e.Message;
		}
	}
}