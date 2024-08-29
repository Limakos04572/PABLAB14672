namespace PabLab.WebAPI.gRPCModule
{
    public class CounterMiddleware
    {
        RequestDelegate _next;

        public CounterMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, CounterClass counter)
        {
            if (context.Request.Headers.ContainsKey("Authorization"))
            {
                counter.Count++;
            }

            await _next(context);
        }
    }
}
