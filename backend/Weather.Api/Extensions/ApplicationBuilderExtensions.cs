namespace Weather.Api.Extensions;

public static class ApplicationBuilderExtensions
{
    public static WebApplication UseAppMiddlewares(this WebApplication app)
    {
        app.UseCors("web");

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        return app;
    }
}
