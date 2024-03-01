using ErrorOr;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc;

namespace FinanceSystem.API.Extensions
{
    public static class ErrorsExtendions
    {
        public static IResult GetErrorsResult(this IEnumerable<Error> errors)
        {
            if (errors.All(e => e.Type == ErrorType.Validation))
            {
                var modelStateDictionary = new Dictionary<string, string[]>();

                foreach (var error in errors)
                {
                    modelStateDictionary.Add(error.Code, [error.Description]);
                }

                return Results.ValidationProblem(modelStateDictionary);
            }

            if (errors.Any(e => e.Type == ErrorType.Unexpected))
            {
                return Results.Problem();
            }

            var firstError = errors.First();

            var statusCode = firstError.Type switch
            {
                ErrorType.NotFound => StatusCodes.Status404NotFound,
                ErrorType.Validation => StatusCodes.Status400BadRequest,
                ErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
                ErrorType.Conflict => StatusCodes.Status409Conflict,
                _ => StatusCodes.Status500InternalServerError
            };

            return Results.Problem(statusCode: statusCode, title: firstError.Description);
        }
    }
}
