using System.Text.Json.Serialization;

namespace E_Commerce.Application.Common
{
    public sealed record Error(string Code , string Description , ErrorType ErrorType = ErrorType.Failuer)
    {
        public static Error Failuer(string code= "General.Failuer" , string description= "General Failuer has accurred")
            => new (code , description , ErrorType.Failuer);
        public static Error Validation(string code= "General.Validation", string description= "General Validation has accurred")
            => new (code , description , ErrorType.Validation);
        public static Error NotFound(string code= "General.NotFound", string description= "Resource Notfound")
            => new (code , description , ErrorType.NotFound);
        public static Error Conflict(string code= "General.Conflict", string description= "General Conflict has accurred")
            => new (code , description , ErrorType.Conflict);

        public static Error Unauthorized(string code= "General.Unauthorized", string description= "Access is denied due to bed ")
            => new (code , description , ErrorType.Unauthorized);

        public static Error Forbidden(string code= "General.Forbidden", string description= "This operation is Forbidden")
            => new (code , description , ErrorType.Forbidden);

        public static Error InvalidCredentials(string code= "General.InvalidCredentials", string description= "Provided Credentials are Invalid")
            => new (code , description , ErrorType.InvalidCredentials);


    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ErrorType
    {
        Failuer = 0,
        Validation=1,
        NotFound=2,
        Conflict=3,
        Unauthorized=4,
        Forbidden =5,
        InvalidCredentials=6
    }
}
