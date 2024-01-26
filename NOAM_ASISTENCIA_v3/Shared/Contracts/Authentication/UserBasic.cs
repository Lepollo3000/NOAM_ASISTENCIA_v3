﻿namespace NOAM_ASISTENCIA_v3.Shared.Contracts.Authentication;

/// <summary>
/// Basic user information to register and login.
/// </summary>
public class UserBasic
{
    /// <summary>
    /// The email address.
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// The password.
    /// </summary>
    public string Password { get; set; } = string.Empty;
}
