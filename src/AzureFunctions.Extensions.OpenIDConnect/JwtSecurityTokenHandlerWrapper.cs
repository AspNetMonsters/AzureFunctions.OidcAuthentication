﻿namespace AzureFunctions.Extensions.OpenIDConnect
{
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using Microsoft.IdentityModel.Tokens;

    internal class JwtSecurityTokenHandlerWrapper : IJwtSecurityTokenHandlerWrapper
    {
        /// <summary>
        /// Reads and validates a 'JSON Web Token' (JWT) and throws an exception if
        /// the token could not be validated.
        /// </summary>
        /// <param name="token">
        /// A JSON Web Token (JWT) encoded as a JWS or JWE in Compact Serialized Format.
        /// </param>
        /// <param name="tokenValidationParameters">
        /// Contains parameters used in the validation of the token.
        /// </param>
        public ClaimsPrincipal ValidateToken(
            string token,
            TokenValidationParameters tokenValidationParameters)
        {
            var handler = new JwtSecurityTokenHandler();

            // Try to validate the token.
            // Throws if the the token cannot be validated.
            // We don't need the ClaimsPrincipal that is returned.
            return handler.ValidateToken(
                token,
                tokenValidationParameters,
                out _); // Discard the output SecurityToken. We don't need it.
        }
    }
}
