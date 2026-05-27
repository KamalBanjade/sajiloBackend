using System;

namespace SecureMedicalRecordSystem.Infrastructure.Templates;

/// <summary>
/// Provides premium, brand-cohesive HTML templates for all transactional system emails.
/// Aligned with the color palette of Sajilo Swasthya (#003B73 and #00A388).
/// </summary>
public static class EmailTemplates
{
    // Brand Color Constants (Matching PDF Clinical Styling)
    private const string ColorPrimary = "#003B73";     // Deep Clinical Navy Blue
    private const string ColorSecondary = "#00A388";   // Clinical Teal / Emerald Green
    private const string ColorWarning = "#D97706";     // Status Amber
    private const string ColorDanger = "#DC2626";      // Status Red
    private const string ColorNeutralDark = "#1E293B";  // Slate Dark (for high contrast text)
    private const string ColorNeutralLight = "#64748B"; // Slate Medium (for subtle labels)

    #region Layout Shell Builders

    private static string GetHeaderHtml(string subtitle, string bannerColor = ColorPrimary)
    {
        return $@"
        <div style='background: linear-gradient(135deg, {bannerColor} 0%, #0f172a 100%); padding: 36px 32px; text-align: center; border-top-left-radius: 16px; border-top-right-radius: 16px;'>
            <div style='display: inline-block; background: rgba(255, 255, 255, 0.12); border-radius: 50%; padding: 12px; margin-bottom: 12px; border: 1px solid rgba(255, 255, 255, 0.2);'>
                <span style='font-size: 32px; line-height: 1; display: block;'>🏥</span>
            </div>
            <h1 style='color: #ffffff; margin: 0; font-family: ""Outfit"", ""Inter"", system-ui, -apple-system, sans-serif; font-size: 26px; font-weight: 800; letter-spacing: -0.5px;'>Sajilo Swasthya</h1>
            <p style='color: #94a3b8; margin: 4px 0 0; font-family: ""Inter"", system-ui, -apple-system, sans-serif; font-size: 11px; font-weight: 700; letter-spacing: 1.5px; text-transform: uppercase;'>Secure Health Portal</p>
            {(!string.IsNullOrEmpty(subtitle) ? $"<div style='display: inline-block; background: rgba(255, 255, 255, 0.08); border-radius: 20px; padding: 4px 16px; margin-top: 14px; border: 1px solid rgba(255, 255, 255, 0.12);'><span style='color: #38bdf8; font-family: \"Inter\", system-ui, sans-serif; font-size: 12px; font-weight: 700; letter-spacing: 0.5px; text-transform: uppercase;'>{subtitle}</span></div>" : "")}
        </div>";
    }

    private static string GetFooterHtml()
    {
        return $@"
        <div style='background-color: #f8fafc; padding: 28px; text-align: center; font-family: ""Inter"", system-ui, -apple-system, sans-serif; border-bottom-left-radius: 16px; border-bottom-right-radius: 16px; border-top: 1px solid #e2e8f0;'>
            <p style='margin: 0; font-size: 12px; color: #64748b; font-weight: 700; letter-spacing: 0.5px; text-transform: uppercase;'>Sajilo Swasthya Medical Network</p>
            <p style='margin: 6px 0; font-size: 11px; color: #94a3b8; line-height: 1.5;'>This communication, including any attachments, contains encrypted information intended solely for the system user and is protected under HIPAA compliance guidelines.</p>
            <div style='margin: 16px 0; border-top: 1px solid #e2e8f0;'></div>
            <p style='margin: 0; font-size: 11px; color: #94a3b8;'>© {DateTime.Now.Year} Sajilo Swasthya. All rights reserved.</p>
            <p style='margin: 4px 0 0; font-size: 10px; color: #cbd5e1; font-style: italic;'>This is an automated clinical notification. Please do not reply directly to this message.</p>
        </div>";
    }

    private static string GetButtonHtml(string label, string url, string color = ColorSecondary)
    {
        return $@"
        <div style='text-align: center; margin: 32px 0;'>
            <!--[if mso]>
            <v:roundrect xmlns:v=""urn:schemas-microsoft-com:vml"" xmlns:w=""urn:schemas-microsoft-com:office:word"" href=""{url}"" style=""height:48px;v-text-anchor:middle;width:240px;"" arcsize=""17%"" stroke=""f"" fillcolor=""{color}"">
              <w:anchorlock/>
              <center style=""color:#ffffff;font-family:sans-serif;font-size:15px;font-weight:bold;"">{label}</center>
            </v:roundrect>
            <![endif]-->
            <a href='{url}' style='background-color: {color}; color: #ffffff; padding: 14px 32px; text-decoration: none; border-radius: 8px; font-family: ""Inter"", system-ui, -apple-system, sans-serif; font-size: 15px; font-weight: 700; display: inline-block; box-shadow: 0 4px 12px rgba(0,0,0,0.08); transition: background-color 0.2s, transform 0.2s; mso-hide:all;'>{label}</a>
        </div>";
    }

    private static string BuildEmailHtml(string headerHtml, string bodyContentHtml)
    {
        return $@"<!DOCTYPE html>
        <html>
        <head>
            <meta charset='UTF-8'>
            <meta name='viewport' content='width=device-width, initial-scale=1.0'>
            <link href='https://fonts.googleapis.com/css2?family=Inter:wght@400;500;600;700&family=Outfit:wght@700;800&display=swap' rel='stylesheet'>
        </head>
        <body style='font-family: ""Inter"", system-ui, -apple-system, sans-serif; background-color: #f1f5f9; margin: 0; padding: 40px 20px; line-height: 1.6; -webkit-font-smoothing: antialiased;'>
            <div style='max-width: 580px; margin: 0 auto; background-color: #ffffff; border-radius: 16px; overflow: hidden; box-shadow: 0 10px 25px -5px rgba(0, 0, 0, 0.04), 0 8px 10px -6px rgba(0, 0, 0, 0.04); border: 1px solid #e2e8f0;'>
                {headerHtml}
                <div style='padding: 40px 32px; color: {ColorNeutralDark};'>
                    {bodyContentHtml}
                </div>
                {GetFooterHtml()}
            </div>
        </body>
        </html>";
    }

    #endregion

    #region Transactions & Onboarding Templates

    public static string GetDoctorInvitationTemplate(string doctorName, string email, string temporaryPassword, string resetLink)
    {
        var header = GetHeaderHtml("PHYSICIAN ONBOARDING", ColorPrimary);
        var body = $@"
        <h2 style='font-family: ""Outfit"", sans-serif; font-size: 20px; margin-top: 0; color: {ColorPrimary};'>Dear Dr. {doctorName},</h2>
        <p style='font-size: 15px; color: #334155; margin-bottom: 24px;'>Your professional physician account has been successfully created in the <strong>Sajilo Swasthya Portal</strong>. We are honored to have you join our clinical network to manage patient records securely.</p>
        
        <div style='background-color: #f8fafc; padding: 24px; border-radius: 12px; border: 1px solid #cbd5e1; margin: 28px 0;'>
            <h3 style='font-family: ""Outfit"", sans-serif; font-size: 15px; margin-top: 0; margin-bottom: 16px; color: {ColorNeutralLight}; text-transform: uppercase; letter-spacing: 0.5px;'>Your Attending Credentials</h3>
            <table style='width: 100%; border-collapse: collapse; font-size: 14px;'>
                <tr>
                    <td style='padding: 8px 0; color: {ColorNeutralLight}; width: 35%; font-weight: 600;'>Attending Email</td>
                    <td style='padding: 8px 0; color: {ColorNeutralDark}; font-weight: 700;'>{email}</td>
                </tr>
                <tr>
                    <td style='padding: 8px 0; color: {ColorNeutralLight}; font-weight: 600;'>Temporary Pass</td>
                    <td style='padding: 8px 0;'><code style='background: #e2e8f0; color: #0f172a; padding: 4px 8px; border-radius: 6px; font-family: Consolas, monospace; font-weight: 700; font-size: 13px; letter-spacing: 0.5px;'>{temporaryPassword}</code></td>
                </tr>
            </table>
            <div style='margin-top: 16px; background-color: #fffbeb; border-left: 4px solid #d97706; padding: 12px; border-radius: 4px;'>
                <p style='margin: 0; font-size: 12px; color: #92400e; font-weight: 500;'>⚠️ For clinical security compliance, you will be required to configure your permanent password on your first login.</p>
            </div>
        </div>

        <p style='font-size: 14px; color: #334155; margin-bottom: 12px;'>To establish your permanent password and finalize your physician account immediately, click the button below:</p>
        {GetButtonHtml("Configure Account & Password", resetLink, ColorSecondary)}
        <p style='font-size: 12px; color: {ColorNeutralLight}; text-align: center; margin-top: -16px;'>This secure activation link will expire in 24 hours.</p>

        <h3 style='font-family: ""Outfit"", sans-serif; font-size: 14px; border-bottom: 2px solid #f1f5f9; padding-bottom: 8px; margin-top: 32px; color: {ColorPrimary}; text-transform: uppercase; letter-spacing: 0.5px;'>Clinical Onboarding Guide</h3>
        <ol style='padding-left: 20px; font-size: 14px; color: #334155; line-height: 1.8;'>
            <li>Click the secure link above to set your password.</li>
            <li>Configure your Two-Factor Authentication (2FA) for secure system entry.</li>
            <li>Verify your physician credentials and assigned department.</li>
            <li>Begin managing digital patient charts with complete security.</li>
        </ol>

        <div style='margin-top: 32px; border-top: 1px solid #f1f5f9; padding-top: 20px;'>
            <p style='margin: 0; font-size: 13px; color: {ColorNeutralLight};'>Need administrative support? Contact the portal admin team at <a href='mailto:admin@sajiloswasthya.com' style='color: {ColorSecondary}; font-weight: 600; text-decoration: none;'>admin@sajiloswasthya.com</a></p>
        </div>";

        return BuildEmailHtml(header, body);
    }

    public static string GetPatientInvitationTemplate(string patientName, string email, string temporaryPassword, string resetLink)
    {
        var header = GetHeaderHtml("PATIENT ONBOARDING", ColorSecondary);
        var body = $@"
        <h2 style='font-family: ""Outfit"", sans-serif; font-size: 20px; margin-top: 0; color: {ColorSecondary};'>Dear {patientName},</h2>
        <p style='font-size: 15px; color: #334155; margin-bottom: 24px;'>Your healthcare provider has created a secure patient portal chart for you in the <strong>Sajilo Swasthya Portal</strong>. You can now access your clinical summaries, prescriptions, and message your doctors online.</p>
        
        <div style='background-color: #f0fdfa; padding: 24px; border-radius: 12px; border: 1px solid #99f6e4; margin: 28px 0;'>
            <h3 style='font-family: ""Outfit"", sans-serif; font-size: 15px; margin-top: 0; margin-bottom: 16px; color: #0f766e; text-transform: uppercase; letter-spacing: 0.5px;'>Your Patient Credentials</h3>
            <table style='width: 100%; border-collapse: collapse; font-size: 14px;'>
                <tr>
                    <td style='padding: 8px 0; color: #0f766e; width: 35%; font-weight: 600;'>Patient Email</td>
                    <td style='padding: 8px 0; color: {ColorNeutralDark}; font-weight: 700;'>{email}</td>
                </tr>
                <tr>
                    <td style='padding: 8px 0; color: #0f766e; font-weight: 600;'>Temporary Pass</td>
                    <td style='padding: 8px 0;'><code style='background: #ccfbf1; color: #0f766e; padding: 4px 8px; border-radius: 6px; font-family: Consolas, monospace; font-weight: 700; font-size: 13px; letter-spacing: 0.5px;'>{temporaryPassword}</code></td>
                </tr>
            </table>
        </div>

        <p style='font-size: 14px; color: #334155; margin-bottom: 12px;'>To access your patient chart securely, please finalize your account password by clicking the button below:</p>
        {GetButtonHtml("Activate Patient Portal", resetLink, ColorSecondary)}
        <p style='font-size: 12px; color: {ColorNeutralLight}; text-align: center; margin-top: -16px;'>This secure link will expire in 24 hours.</p>

        <div style='margin-top: 32px; border-top: 1px solid #f1f5f9; padding-top: 20px;'>
            <p style='margin: 0; font-size: 13px; color: {ColorNeutralLight};'>If you did not request this account or have questions, please reach out directly to your attending clinic.</p>
        </div>";

        return BuildEmailHtml(header, body);
    }

    #endregion

    #region Security Templates

    public static string GetPasswordResetTemplate(string resetLink)
    {
        var header = GetHeaderHtml("SECURITY CONTROL", ColorWarning);
        var body = $@"
        <h2 style='font-family: ""Outfit"", sans-serif; font-size: 20px; margin-top: 0; color: {ColorNeutralDark};'>Password Reset Request</h2>
        <p style='font-size: 15px; color: #334155; margin-bottom: 20px;'>We received an official request to reset the account password for your <strong>Sajilo Swasthya</strong> profile. If you initiated this request, please click the secure link below to update your password:</p>
        
        {GetButtonHtml("Reset Account Password", resetLink, ColorWarning)}
        <p style='font-size: 12px; color: {ColorNeutralLight}; text-align: center; margin-top: -16px;'>For security, this password reset link will expire in 24 hours.</p>

        <div style='margin-top: 32px; background-color: #f8fafc; padding: 16px; border-radius: 8px; border: 1px solid #e2e8f0;'>
            <p style='margin: 0; font-size: 13px; color: {ColorNeutralLight};'><strong>Did not request this?</strong> If you did not request a password change, you can safely ignore this automated message. Your account credentials will remain entirely unaffected.</p>
        </div>";

        return BuildEmailHtml(header, body);
    }

    public static string GetEmailConfirmationTemplate(string confirmationLink)
    {
        var header = GetHeaderHtml("VERIFY EMAIL", ColorSecondary);
        var body = $@"
        <h2 style='font-family: ""Outfit"", sans-serif; font-size: 20px; margin-top: 0; color: {ColorSecondary};'>Confirm Your Health Account</h2>
        <p style='font-size: 15px; color: #334155; margin-bottom: 20px;'>Welcome to Sajilo Swasthya! To complete your profile setup and enable secure clinical access to medical records, please click the verification button below:</p>
        
        {GetButtonHtml("Verify Email Address", confirmationLink, ColorSecondary)}
        <p style='font-size: 12px; color: {ColorNeutralLight}; text-align: center; margin-top: -16px;'>This secure verification link will expire in 24 hours.</p>

        <div style='margin-top: 32px; border-top: 1px solid #f1f5f9; padding-top: 20px;'>
            <p style='margin: 0; font-size: 12px; color: {ColorNeutralLight};'>By verifying your email, you consent to receive electronic medical communications and clinic notifications securely.</p>
        </div>";

        return BuildEmailHtml(header, body);
    }

    #endregion

    #region Appointment Workflow Templates

    public static string GetAppointmentScheduledTemplate(string patientName, string doctorName, DateTime date, string reason)
    {
        var header = GetHeaderHtml("APPOINTMENT REQUESTED", ColorPrimary);
        var body = $@"
        <h2 style='font-family: ""Outfit"", sans-serif; font-size: 20px; margin-top: 0; color: {ColorPrimary};'>Dear {patientName},</h2>
        <p style='font-size: 15px; color: #334155; margin-bottom: 24px;'>Your appointment request has been successfully submitted and is currently pending review by your attending physician. Below are the requested session details:</p>
        
        <div style='background-color: #f8fafc; padding: 24px; border-radius: 12px; border: 1px solid #e2e8f0; margin: 24px 0;'>
            <table style='width: 100%; border-collapse: collapse; font-size: 14px;'>
                <tr>
                    <td style='padding: 8px 0; color: {ColorNeutralLight}; width: 35%; font-weight: 600;'>Attending Doctor</td>
                    <td style='padding: 8px 0; color: {ColorNeutralDark}; font-weight: 700;'>Dr. {doctorName}</td>
                </tr>
                <tr>
                    <td style='padding: 8px 0; color: {ColorNeutralLight}; font-weight: 600;'>Requested Date</td>
                    <td style='padding: 8px 0; color: {ColorNeutralDark}; font-weight: 700;'>{date:f}</td>
                </tr>
                <tr>
                    <td style='padding: 8px 0; color: {ColorNeutralLight}; font-weight: 600;'>Reason for Visit</td>
                    <td style='padding: 8px 0; color: {ColorNeutralDark}; font-weight: 500;'>{reason}</td>
                </tr>
            </table>
        </div>

        <p style='font-size: 14px; color: #334155;'>We will send you a confirmation notification as soon as the physician's schedule is locked in. Thank you for using Sajilo Swasthya.</p>";

        return BuildEmailHtml(header, body);
    }

    public static string GetAppointmentConfirmedTemplate(string patientName, string doctorName, DateTime date, string department)
    {
        var header = GetHeaderHtml("BOOKING CONFIRMED", ColorSecondary);
        var body = $@"
        <h2 style='font-family: ""Outfit"", sans-serif; font-size: 20px; margin-top: 0; color: {ColorSecondary};'>Dear {patientName},</h2>
        <p style='font-size: 15px; color: #334155; margin-bottom: 24px;'>Your clinical session has been officially <strong>confirmed</strong> by the physician. Below are your scheduled consultation guidelines:</p>
        
        <div style='background-color: #f0fdf4; padding: 24px; border-radius: 12px; border: 1px solid #bbf7d0; margin: 24px 0;'>
            <table style='width: 100%; border-collapse: collapse; font-size: 14px;'>
                <tr>
                    <td style='padding: 8px 0; color: #166534; width: 35%; font-weight: 600;'>Attending Doctor</td>
                    <td style='padding: 8px 0; color: {ColorNeutralDark}; font-weight: 700;'>Dr. {doctorName}</td>
                </tr>
                <tr>
                    <td style='padding: 8px 0; color: #166534; font-weight: 600;'>Department</td>
                    <td style='padding: 8px 0; color: {ColorNeutralDark}; font-weight: 700;'>{department}</td>
                </tr>
                <tr>
                    <td style='padding: 8px 0; color: #166534; font-weight: 600;'>Date & Time</td>
                    <td style='padding: 8px 0; color: #166534; font-weight: 800; font-size: 15px;'>{date:f}</td>
                </tr>
            </table>
        </div>

        <div style='margin-top: 24px; padding: 16px; background-color: #f8fafc; border-radius: 8px; border: 1px solid #e2e8f0;'>
            <p style='margin: 0; font-size: 13px; color: #475569;'>📌 <strong>Attending Notice:</strong> Please check in at least 10 minutes prior to your scheduled time. You can review pre-visit instructions in your Patient Dashboard.</p>
        </div>";

        return BuildEmailHtml(header, body);
    }

    public static string GetAppointmentCancelledTemplate(string userName, DateTime date, string reason, string role)
    {
        var header = GetHeaderHtml("APPOINTMENT CANCELLED", ColorDanger);
        var body = $@"
        <h2 style='font-family: ""Outfit"", sans-serif; font-size: 20px; margin-top: 0; color: {ColorDanger};'>Dear {userName},</h2>
        <p style='font-size: 15px; color: #334155; margin-bottom: 24px;'>Please be advised that your scheduled clinical session on <strong>{date:f}</strong> has been cancelled.</p>
        
        <div style='background-color: #fef2f2; padding: 24px; border-radius: 12px; border: 1px solid #fecaca; margin: 24px 0;'>
            <h3 style='font-family: ""Outfit"", sans-serif; font-size: 14px; margin-top: 0; margin-bottom: 8px; color: #991b1b; text-transform: uppercase; letter-spacing: 0.5px;'>Cancellation Reason</h3>
            <p style='margin: 0; font-size: 14px; color: #991b1b; font-weight: 600;'>{reason}</p>
        </div>

        {(role == "Patient" ? $"<p style='font-size: 14px; color: #334155;'>If you wish to reschedule this appointment, please visit the <strong>Sajilo Swasthya</strong> patient dashboard or contact the clinic reception immediately.</p>" : "")}";

        return BuildEmailHtml(header, body);
    }

    public static string GetAppointmentRescheduledTemplate(string patientName, string doctorName, DateTime oldDate, DateTime newDate)
    {
        var header = GetHeaderHtml("RESCHEDULE NOTICE", ColorWarning);
        var body = $@"
        <h2 style='font-family: ""Outfit"", sans-serif; font-size: 20px; margin-top: 0; color: {ColorWarning};'>Dear {patientName},</h2>
        <p style='font-size: 15px; color: #334155; margin-bottom: 24px;'>Your clinical session with Dr. {doctorName} has been rescheduled to optimize provider availability. Please note the adjusted timeframe:</p>
        
        <div style='background-color: #fffbeb; padding: 24px; border-radius: 12px; border: 1px solid #fef3c7; margin: 24px 0;'>
            <table style='width: 100%; border-collapse: collapse; font-size: 14px;'>
                <tr>
                    <td style='padding: 8px 0; color: #92400e; width: 35%; font-weight: 600;'>Original Time</td>
                    <td style='padding: 8px 0; color: #b45309; text-decoration: line-through;'>{oldDate:f}</td>
                </tr>
                <tr>
                    <td style='padding: 8px 0; color: #92400e; font-weight: 600;'>Rescheduled</td>
                    <td style='padding: 8px 0; color: #92400e; font-weight: 800; font-size: 15px;'>{newDate:f}</td>
                </tr>
                <tr>
                    <td style='padding: 8px 0; color: #92400e; font-weight: 600;'>Attending</td>
                    <td style='padding: 8px 0; color: {ColorNeutralDark}; font-weight: 700;'>Dr. {doctorName}</td>
                </tr>
            </table>
        </div>

        <p style='font-size: 14px; color: #334155;'>Please log in to your dashboard to confirm this new slot or coordinate adjustments.</p>";

        return BuildEmailHtml(header, body);
    }

    public static string GetAppointmentReminderTemplate(string patientName, string doctorName, DateTime date)
    {
        var header = GetHeaderHtml("UPCOMING VISIT REMINDER", ColorPrimary);
        var body = $@"
        <h2 style='font-family: ""Outfit"", sans-serif; font-size: 20px; margin-top: 0; color: {ColorPrimary};'>Dear {patientName},</h2>
        <p style='font-size: 15px; color: #334155; margin-bottom: 24px;'>This is a friendly clinical notification regarding your upcoming consultation scheduled for tomorrow. Below are your session details:</p>
        
        <div style='background-color: #f8fafc; padding: 24px; border-radius: 12px; border: 1px solid #cbd5e1; margin: 24px 0;'>
            <table style='width: 100%; border-collapse: collapse; font-size: 14px;'>
                <tr>
                    <td style='padding: 8px 0; color: {ColorNeutralLight}; width: 35%; font-weight: 600;'>Attending Doctor</td>
                    <td style='padding: 8px 0; color: {ColorNeutralDark}; font-weight: 700;'>Dr. {doctorName}</td>
                </tr>
                <tr>
                    <td style='padding: 8px 0; color: {ColorNeutralLight}; font-weight: 600;'>Date & Time</td>
                    <td style='padding: 8px 0; color: {ColorPrimary}; font-weight: 800; font-size: 15px;'>{date:f}</td>
                </tr>
            </table>
        </div>

        <p style='font-size: 13px; color: {ColorNeutralLight}; line-height: 1.5; margin-top: 24px;'>⚠️ <strong>Note:</strong> If you need to reschedule or cancel your visit, please do so at least 24 hours in advance via your Patient Dashboard to support clinic scheduling.</p>";

        return BuildEmailHtml(header, body);
    }

    public static string GetDoctorNewAppointmentTemplate(string doctorName, string patientName, DateTime date, string reason)
    {
        var header = GetHeaderHtml("NEW PATIENT BOOKING", ColorPrimary);
        var body = $@"
        <h2 style='font-family: ""Outfit"", sans-serif; font-size: 20px; margin-top: 0; color: {ColorPrimary};'>Dear Dr. {doctorName},</h2>
        <p style='font-size: 15px; color: #334155; margin-bottom: 24px;'>A new patient clinical appointment has been scheduled under your registry. The visit parameters are outlined below:</p>
        
        <div style='background-color: #f8fafc; padding: 24px; border-radius: 12px; border: 1px solid #e2e8f0; margin: 24px 0;'>
            <table style='width: 100%; border-collapse: collapse; font-size: 14px;'>
                <tr>
                    <td style='padding: 8px 0; color: {ColorNeutralLight}; width: 35%; font-weight: 600;'>Attending Patient</td>
                    <td style='padding: 8px 0; color: {ColorNeutralDark}; font-weight: 700;'>{patientName}</td>
                </tr>
                <tr>
                    <td style='padding: 8px 0; color: {ColorNeutralLight}; font-weight: 600;'>Date & Time</td>
                    <td style='padding: 8px 0; color: {ColorNeutralDark}; font-weight: 700;'>{date:f}</td>
                </tr>
                <tr>
                    <td style='padding: 8px 0; color: {ColorNeutralLight}; font-weight: 600;'>Reason for Visit</td>
                    <td style='padding: 8px 0; color: {ColorNeutralDark}; font-weight: 500;'>{reason}</td>
                </tr>
            </table>
        </div>

        <p style='font-size: 14px; color: #334155;'>You can review the patient's historical medical records, charts, and pre-populate observation templates directly inside your Doctor Dashboard.</p>";

        return BuildEmailHtml(header, body);
    }

    #endregion

    #region Follow-Up & Specialty Templates

    public static string GetFollowUpScheduledTemplate(string patientName, string doctorName, DateTime appointmentDate)
    {
        var formattedDate = appointmentDate.ToLocalTime().ToString("dddd, MMMM d, yyyy");
        var formattedTime = appointmentDate.ToLocalTime().ToString("h:mm tt");

        var header = GetHeaderHtml("FOLLOW-UP CONFIRMED", ColorSecondary);
        var body = $@"
        <h2 style='font-family: ""Outfit"", sans-serif; font-size: 20px; margin-top: 0; color: {ColorSecondary};'>Dear {patientName},</h2>
        <p style='font-size: 15px; color: #334155; margin-bottom: 24px;'>Your physician has successfully scheduled a follow-up consultation for you to monitor your health progress. Below are the confirmed details:</p>
        
        <div style='background-color: #f0fdf4; border: 1px solid #bbf7d0; border-radius: 12px; padding: 24px; margin-bottom: 24px;'>
            <table style='width: 100%; border-collapse: collapse; font-size: 14px;'>
                <tr>
                    <td style='padding: 8px 0; color: #047857; width: 35%; font-weight: 600; text-transform: uppercase; letter-spacing: 0.5px;'>Follow-Up Date</td>
                    <td style='padding: 8px 0; color: {ColorNeutralDark}; font-weight: 700;'>{formattedDate}</td>
                </tr>
                <tr>
                    <td style='padding: 8px 0; color: #047857; font-weight: 600; text-transform: uppercase; letter-spacing: 0.5px;'>Session Time</td>
                    <td style='padding: 8px 0; color: {ColorNeutralDark}; font-weight: 700;'>{formattedTime}</td>
                </tr>
                <tr>
                    <td style='padding: 8px 0; color: #047857; font-weight: 600; text-transform: uppercase; letter-spacing: 0.5px;'>Physician</td>
                    <td style='padding: 8px 0; color: {ColorNeutralDark}; font-weight: 700;'>Dr. {doctorName}</td>
                </tr>
                <tr>
                    <td style='padding: 8px 0; color: #047857; font-weight: 600; text-transform: uppercase; letter-spacing: 0.5px;'>Visit Type</td>
                    <td style='padding: 8px 0; color: #059669; font-weight: 800;'>Clinical Follow-Up</td>
                </tr>
            </table>
        </div>

        <p style='font-size: 13px; color: {ColorNeutralLight}; line-height: 1.5;'>🗓️ <strong>Calendar Integration:</strong> An `.ics` calendar invitation file is attached to this email. You can import it directly to Google Calendar, Microsoft Outlook, or Apple Calendar for seamless tracking.</p>
        <p style='font-size: 13px; color: {ColorNeutralLight}; line-height: 1.5; margin-top: 12px;'>If you require schedule updates, please contact the clinic registration desk as soon as possible.</p>";

        return BuildEmailHtml(header, body);
    }

    #endregion
}
