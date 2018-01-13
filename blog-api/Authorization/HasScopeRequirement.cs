// <copyright file="HasScopeRequirement.cs" company="Dominik Steffen">
// Software is not for free use. If you want to use it please contact me at dominik.steffen@gmail.com
// </copyright>

namespace BlogAPI.Authorization
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;

    public class HasScopeRequirement : IAuthorizationRequirement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HasScopeRequirement"/> class.
        /// </summary>
        /// <param name="scope">The scope to check</param>
        /// <param name="issuer">The authorization issuer</param>
        public HasScopeRequirement(string scope, string issuer)
        {
            this.Scope = scope ?? throw new ArgumentNullException(nameof(scope));
            this.Issuer = issuer ?? throw new ArgumentNullException(nameof(issuer));
        }

        public string Issuer { get; }

        public string Scope { get; }
    }
}
