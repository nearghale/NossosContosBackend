using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nossos_Contos.Services.AWS
{
	public class CognitoService
	{

		private const string USER_POOL_ID = "us-east-2_t4UQdsSO0";
		private AmazonCognitoIdentityProviderClient client;

		public CognitoService()
		{
			client = new AmazonCognitoIdentityProviderClient("AKIAJ4VTUE37VZ6LJAEA", "cv5yRi5GBwRkGvXT8Tar21KE7xB3M5bpejrrrG+6", Amazon.RegionEndpoint.USEast2);
		}

		public void SignUp(Models.SignUp model)
		{
			var request = new AdminCreateUserRequest()
			{
				UserPoolId = USER_POOL_ID,
				ForceAliasCreation = true,
				MessageAction = MessageActionType.SUPPRESS,
				TemporaryPassword = model.password,
				Username = model.user_name,
				UserAttributes = new List<AttributeType>()
			};
			request.UserAttributes.Add(new AttributeType() { Name = "birthdate", Value = model.birth_date});
			request.UserAttributes.Add(new AttributeType() { Name = "name", Value = model.name });
			request.UserAttributes.Add(new AttributeType() { Name = "family_name", Value = model.family_name });
			request.UserAttributes.Add(new AttributeType() { Name = "email", Value = model.email });

			var response = client.AdminCreateUserAsync(request);
			response.Wait();

			var result = response.Result;
		}

		public Models.Token Authenticate(Models.Authentication model)
		{
			var request = new AdminInitiateAuthRequest
			{
				UserPoolId = USER_POOL_ID,
				ClientId = "3fd3acd1ve2j1mcp100i83o9bl",
				AuthFlow = AuthFlowType.ADMIN_NO_SRP_AUTH
			};

			request.AuthParameters.Add("USERNAME", model.username);
			request.AuthParameters.Add("PASSWORD", model.password);

			var response = client.AdminInitiateAuthAsync(request);
			response.Wait();

			var result = response.Result;
			if (result.ChallengeName == ChallengeNameType.NEW_PASSWORD_REQUIRED)
			{
				ChangePassword(model);
				return Authenticate(model);
			}

			var token = new Models.Token()
			{
				access_token = result.AuthenticationResult.IdToken
			};
			return token;
		}

		public void ChangePassword(Models.Authentication model)
		{
			var request = new AdminSetUserPasswordRequest()
			{
				Password = model.password,
				Permanent = true,
				Username = model.username,
				UserPoolId = USER_POOL_ID
			};
			client.AdminSetUserPasswordAsync(request).Wait();
		}
	}
}
