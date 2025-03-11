using Microsoft.AspNetCore.Mvc;
using ConnectLogger.Auth.Api.Presentation.Extensions;
using ConnectLogger.Auth.Api.Application.Interfaces.UseCases.SignIn;
using ConnectLogger.Auth.Api.Application.Interfaces.UseCases.SignUp;
using ConnectLogger.Auth.Api.Application.Interfaces.UseCases.SignOut;
using Microsoft.AspNetCore.Authorization;


namespace ConnectLogger.Auth.Api.Presentation.Controllers
{
    [ApiController]
    [Route("auth")]
    [Authorize]
    public class AuthController(
        ISignUpUseCase signUpUseCase,
        ISignInUseCase signInUseCase,
        ISignOutUseCase signOutUseCase)
        : ControllerBase
    {
        private readonly ISignUpUseCase _signUpUseCase = signUpUseCase;
        private readonly ISignInUseCase _signInUseCase = signInUseCase;
        private readonly ISignOutUseCase _signOutUseCase = signOutUseCase;

        [HttpPost("sign-up", Name = "SignUp")]
        [AllowAnonymous]
        public async Task<IResult> SignUpAsync(
            [FromBody] SignUpRequest request,
            CancellationToken cancellationToken = default)
        {
            var response = await _signUpUseCase.ExecuteAsync(request, cancellationToken);

            return response.GetHttpResult();
        }

        [HttpPost("sign-in", Name = "SignIn")]
        [AllowAnonymous]
        public async Task<IResult> SignInAsync(
            [FromBody] SignInRequest request,
            CancellationToken cancellationToken = default)
        {
            var response = await _signInUseCase.ExecuteAsync(request, cancellationToken);

            return response.GetHttpResult();
        }

        [HttpPost("sign-out", Name = "SignOut")]
        public async Task<IResult> SignOutAsync(
            CancellationToken cancellationToken = default)
        {
            var response = await _signOutUseCase.ExecuteAsync(cancellationToken);

            if (!response.IsSuccess)
            {
                return response.GetHttpResult();
            }

            return Results.NoContent();
        }
    }
}
