using System;

namespace SecureMedicalRecordSystem.Core.Entities;

/// <summary>
/// Secure, database-backed password reset or invitation token.
/// Completely independent of DataProtection key storage and machine/environment environments.
/// </summary>
public class PasswordResetToken : BaseEntity
{
    public Guid UserId { get; set; }
    public string TokenHash { get; set; } = string.Empty;
    public DateTime ExpiresAt { get; set; }
    public bool IsUsed { get; set; } = false;

    // Navigation
    public ApplicationUser User { get; set; } = null!;
}
