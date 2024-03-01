using ErrorOr;
using FinanceSystem.API.Endpoints.Internal;
using FinanceSystem.API.Extensions;
using FinanceSystem.Application.BankAccounts.Commands.AddBankAccount;
using FinanceSystem.Application.BankAccounts.Commands.DeleteBankAccount;
using FinanceSystem.Application.BankAccounts.Commands.UpdateBankAccount;
using FinanceSystem.Application.BankAccounts.Dtos;
using FinanceSystem.Application.BankAccounts.Queries.GetUserBankAccounts;
using FinanceSystem.Contracts.BankAccounts;
using FinanceSystem.Domain.Common.Interfaces;
using FinanceSystem.Infrastructure.Services;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using System.Security.Claims;

namespace FinanceSystem.API.Endpoints
{
    public class BankAccountEndpoints : IEndpoints
    {
        private const string ContentType = "application/json";
        private const string Tag = "BankAccounts";
        private const string BaseRoute = "api/bankaccount";

        public static void DefineEndpoints(IEndpointRouteBuilder app)
        {
            app.MapPost($"{BaseRoute}/create", CreateBankAccountAsync)
                .RequireAuthorization()
                .WithName("CreateBankAccount")
                .Accepts<BankAccountRequest>(ContentType)
                .Produces<BankAccountResponse>(201)
                .Produces<IResult>(401).Produces<IResult>(400)
                .WithTags(Tag);

            app.MapGet($"{BaseRoute}/", GetUserBankAccountsAsync)
                .RequireAuthorization()
                .WithName("GetUserBankAccounts")
                .Produces<IEnumerable<BankAccountLiteDto>>(200)
                .Produces<IResult>(401).Produces<IEnumerable<Error>>(404)
                .WithTags(Tag);

            app.MapPut($"{BaseRoute}/{{bankAccountId}}", UpdateBankAccountAsync)
                .RequireAuthorization()
                .WithName("UpdateBook")
                .Accepts<BankAccountRequest>(ContentType)
                .Produces<BankAccountResponse>(200)
                .Produces<IResult>(401).Produces<IResult>(400)
                .WithTags(Tag);

            app.MapDelete($"{BaseRoute}/{{bankAccountId}}", DeleteBankAccountAsync)
                .RequireAuthorization()
                .WithName("DeleteBook")
                .Produces(204)
                .Produces<IResult>(401).Produces<IEnumerable<IResult>>(404)
                .WithTags(Tag);
        }

        internal static async Task<IResult> CreateBankAccountAsync(
            HttpContext httpContext,
            BankAccountRequest bankAccount,
            IValidator<BankAccountRequest> validator,
            IUserProvider userProvider,
            ISender sender)
        {
            var userId = userProvider.GetUserId(httpContext);
            if(userId.IsError)
            {
                return Results.Unauthorized();
            }

            var validationResult = await validator.ValidateAsync(bankAccount);
            if (!validationResult.IsValid)
            {
                return Results.BadRequest(validationResult.Errors.Select(error => error.ErrorMessage));
            }

            var adddBankAccountCommand = new AddBankAccountCommand(
                userId.Value,
                bankAccount.AccountName,
                bankAccount.AccountNumber,
                bankAccount.BankCode,
                bankAccount.Agency,
                bankAccount.AccountType,
                bankAccount.Balance);

            var result = await sender.Send(adddBankAccountCommand);

            if(result.IsError)
            {
                return result.Errors.GetErrorsResult();
            }

            return Results.Created($"/{BaseRoute}/{result.Value}", result.Value);
        }

        internal static async Task<IResult> GetUserBankAccountsAsync(
            HttpContext httpContext,
            IUserProvider userProvider,
            ISender sender)
        {
            var userId = userProvider.GetUserId(httpContext);

            if (userId.IsError)
            {
                return Results.Unauthorized();
            }

            var result = await sender.Send(new GetUserBankAccountsQuery(userId.Value));

            return Results.Ok(result);
        }

        internal static async Task<IResult> UpdateBankAccountAsync(
            Guid bankAccountId,
            HttpContext httpContext,
            BankAccountRequest bankAccount,
            IValidator<BankAccountRequest> validator,
            IUserProvider userProvider,
            ISender sender)
        {
            var userId = userProvider.GetUserId(httpContext);
            if (userId.IsError)
            {
                return Results.Unauthorized();
            }

            var validationResult = await validator.ValidateAsync(bankAccount);
            if (!validationResult.IsValid)
            {
                return Results.BadRequest(validationResult.Errors.Select(error => error.ErrorMessage));
            }

            var updateBankAccountCommand = new UpdateBankAccountCommand(
                userId.Value,
                bankAccountId,
                bankAccount.AccountName,
                bankAccount.Agency,
                bankAccount.AccountType);

            var result = await sender.Send(updateBankAccountCommand);

            if (result.IsError)
            {
                return result.Errors.GetErrorsResult();
            }

            return Results.Ok();
        }

        internal static async Task<IResult> DeleteBankAccountAsync(Guid bankAccountId,
            HttpContext httpContext,
            IUserProvider userProvider,
            ISender sender)
        {
            var userId = userProvider.GetUserId(httpContext);
            if (userId.IsError)
            {
                return Results.Unauthorized();
            }

            var deleteBankAccountCommand = new DeleteBankAccountCommand(userId.Value, bankAccountId);

            var result = await sender.Send(deleteBankAccountCommand);

            if (result.IsError)
            {
                return result.Errors.GetErrorsResult();
            }

            return Results.Ok();
        }

        public static void AddServices(IServiceCollection services, IConfiguration configuration)
        {
        }
    }
}
