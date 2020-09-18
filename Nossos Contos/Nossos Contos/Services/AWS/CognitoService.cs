using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nossos_Contos.Models.AWS.Cognito;

namespace Nossos_Contos.Services.AWS
{
	public class CognitoService
	{

		private const string USER_POOL_ID = "us-east-2_criptografia";
		private AmazonCognitoIdentityProviderClient client;

		public CognitoService()
		{
			client = new AmazonCognitoIdentityProviderClient("*******", "**********", Amazon.RegionEndpoint.USEast2);
		}

		public void SignUp(SignUp model)
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
			request.UserAttributes.Add(new AttributeType() { Name = "picture", Value = model.picture });

			var response = client.AdminCreateUserAsync(request);

			response.Wait();		

			var result = response.Result;

		}

		public void Update(string user_name,UpdateUserCognito model)
		{
			var request = new AdminUpdateUserAttributesRequest()
			{
				UserPoolId = USER_POOL_ID,
				Username = user_name


			};
			request.UserAttributes.Add(new AttributeType() { Name = "birthdate", Value = model.birth_date });
			request.UserAttributes.Add(new AttributeType() { Name = "name", Value = model.name });
			request.UserAttributes.Add(new AttributeType() { Name = "family_name", Value = model.family_name });
			request.UserAttributes.Add(new AttributeType() { Name = "picture", Value = model.picture });

			client.AdminUpdateUserAttributesAsync(request).Wait();


		}

		public Token Authenticate(Authentication model)
		{
			var request = new AdminInitiateAuthRequest
			{
				UserPoolId = USER_POOL_ID,
				ClientId = "**********",
				AuthFlow = AuthFlowType.ADMIN_NO_SRP_AUTH
			};

			request.AuthParameters.Add("USERNAME", model.user_name);
			request.AuthParameters.Add("PASSWORD", model.password);

			var response = client.AdminInitiateAuthAsync(request);
			response.Wait();

			var result = response.Result;
			if (result.ChallengeName == ChallengeNameType.NEW_PASSWORD_REQUIRED)
			{
				ChangePassword(model);
				return Authenticate(model);
			}

			var token = new Token()
			{
				access_token = result.AuthenticationResult.IdToken
			};
			return token;
		}

		public void Delete(string username)
		{
			var request = new AdminDeleteUserRequest()
			{
				Username = username,
				UserPoolId = USER_POOL_ID
			};
			client.AdminDeleteUserAsync(request).Wait();
		}

		public void ChangePassword(Authentication model)
		{
			var request = new AdminSetUserPasswordRequest()
			{
				Password = model.password,
				Permanent = true,
				Username = model.user_name,
				UserPoolId = USER_POOL_ID
			};
			client.AdminSetUserPasswordAsync(request).Wait();
		}
	}
}
