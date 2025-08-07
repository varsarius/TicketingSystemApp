using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketingSystemBackend.Application.DTOs.Auth;

namespace TicketingSystemBackend.Application.Interfaces;

/// <summary>
/// Provides authentication-related functionality such as user registration.
/// </summary>
public interface IAuthRepository
{
    /// <summary>
    /// Registers a new user with the specified email, username, and password.
    /// </summary>
    /// <param name="email">The email address of the user to register.</param>
    /// <param name="username">The username for the new user.</param>
    /// <param name="password">The password for the new user. It will be hashed internally.</param>
    /// <param name="cancellationToken">A token to cancel the registration operation.</param>
    /// <returns>A task representing the asynchronous registration operation.</returns>
    Task RegisterUserAsync(string email, string username, string password, CancellationToken cancellationToken);

    /// <summary>
    /// Authenticates a user and returns access and refresh tokens if successful.
    /// </summary>
    /// <param name="email">The user's email address.</param>
    /// <param name="password">The user's password.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>A <see cref="LoginResponse"/> containing tokens and metadata.</returns>
    Task<(string Token, string RefreshToken)> LoginAsync(string email, string password, CancellationToken cancellationToken);

}
