namespace CrmBackend.Utils;

public static class RegisterMiddlewaresHelper
{
    public static void RegisterMiddlewares(this WebApplication app)
    {
        app.UseAuthentication();
        app.UseAuthorization();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();

        app.UseCors(builder => builder
                                    .AllowAnyOrigin()
                                    .AllowAnyHeader()
                                    .AllowAnyMethod());

        //app.MapHub<NotificationHub>("/notification");
    }
}
