using System.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;

public class Anonymous : IAuthentication
{
    public async void AuthenticationAsync()
    {
        await UnityServices.InitializeAsync();

        AuthenticationService.Instance.SignedIn += () =>
        {
            Debug.Log("Sign in " + AuthenticationService.Instance.PlayerId);
        };
        //SignIn with Anonymous
        await AuthenticationService.Instance.SignInAnonymouslyAsync();
    }
}   
