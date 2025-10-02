namespace BookingApp.Api.Middleware;

public class DateTimeHeaders
{
    private readonly RequestDelegate _next;

    public DateTimeHeaders(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        context.Request.Headers.Add("Date-Time-For-Request", DateTime.Now.ToString());
        await Task.FromResult(_next(context));
    }

    public static class DateTimeHeadersExtensions
    {
        public static IApplicationBuilder UseDateTimeHeaders(IApplicationBuilder builder)
        {
            return builder.UseMiddleware<DateTimeHeaders>();
        }
    }
}