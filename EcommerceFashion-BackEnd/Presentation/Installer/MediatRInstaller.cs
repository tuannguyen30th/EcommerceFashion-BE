using Application.AccountService.Commands;
using MediatR;
using System.Reflection;

namespace Presentation.Installer
{
    public class MediatRInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            //--CommandHandlers
            //--AccountCommandHandlers
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(SendEmailCommandHandler).GetTypeInfo().Assembly));
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(AccountSignUpCommandHandler).GetTypeInfo().Assembly));
            //services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(AccountLoginCommandHandler).GetTypeInfo().Assembly));
            //services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(AccountVerifyEmailCommandHandler).GetTypeInfo().Assembly));
            //services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(SendVerificationEmailCommandHandler).GetTypeInfo().Assembly));
            //services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(ResendVerificationEmailCommandHandler).GetTypeInfo().Assembly));
            //services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(AddAccountsCommandHandler).GetTypeInfo().Assembly));
            //services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(ChangePasswordCommandHanlder).GetTypeInfo().Assembly));
            //services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(DeleteAccountCommandHanlder).GetTypeInfo().Assembly));
            //services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(ForgotPasswordCommandHandler).GetTypeInfo().Assembly));
            //services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(LoginGoogleCommandHandler).GetTypeInfo().Assembly));
            //services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(ResendVerificationEmailCommandHandler).GetTypeInfo().Assembly));
            //services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(ResetPasswordCommandHandler).GetTypeInfo().Assembly));
            //services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(RestoreAccountCommandHandler).GetTypeInfo().Assembly));
            //services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(UpdateAccountCommandHandler).GetTypeInfo().Assembly));
            //services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(RefreshTokenCommandHandler).GetTypeInfo().Assembly));
            ////--QueryHandlers
            ////--ProductQueryHandlers
            //services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(GetAllProductsQueryHandler).GetTypeInfo().Assembly));
            ////--AccountCommandHandlers
            //services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(GetAccountQueryHandler).GetTypeInfo().Assembly));
            //services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(GetAllAccountsQueryHandler).GetTypeInfo().Assembly));

        }
    }
}
