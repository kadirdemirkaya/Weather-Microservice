namespace Services.DataCaptureService.Api.Registrations
{
    public static class AuthenticationRegistration
    {
        public static WebApplication AuthenticationApplicationRegistration(this WebApplication app)
        {
            app.UseAuthentication();

            app.UseAuthorization();

            return app;
        }
    }
}
