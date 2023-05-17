using System;
namespace Backend_api.Models
{
	public class LoginResult
	{
        public bool Success { get; set; }
        public string Message { get; set; }
        public string Token { get; set; }

        public LoginResult()
		{
		}
	}
}

