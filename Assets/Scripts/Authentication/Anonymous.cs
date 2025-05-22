using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;

public class Anonymous : IAuthentication
{
    private bool _isInitialized = false;

    public async void AuthenticationAsync()
    {
        Debug.Log("Sign in");
        try
        {
            if (!_isInitialized)
            {
                await UnityServices.InitializeAsync();
                _isInitialized = true;
            }

            if (!AuthenticationService.Instance.IsSignedIn)
            {
                AuthenticationService.Instance.SignedIn += () =>
                {
                    Debug.Log("Signed in: " + AuthenticationService.Instance.PlayerId);
                };

                await AuthenticationService.Instance.SignInAnonymouslyAsync();
            }
            else
            {
                Debug.Log("Already signed in: " + AuthenticationService.Instance.PlayerId);
            }
        }
        catch (RequestFailedException ex)
        {
            Debug.LogError($"Authentication failed: {ex.Message} (Code: {ex.ErrorCode})");
        }
    }
}
