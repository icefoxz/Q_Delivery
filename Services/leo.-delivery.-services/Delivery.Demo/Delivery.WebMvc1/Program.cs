using Delivery.WebMvc1.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

    // ↓ Jwt 认证配置
})
.AddJwtBearer(options =>
{
    //options.TokenValidationParameters = tokenValidationParameters;
    options.SaveToken = true;
    options.Events = new JwtBearerEvents()
    {
        // 在安全令牌通过验证和ClaimsIdentity通过验证之后调用
        // 如果用户访问注销页面
        OnTokenValidated = context =>
        {
            if (context.Request.Path.Value.ToString() == "/account/logout")
            {
                var token = ((context as TokenValidatedContext).SecurityToken as JwtSecurityToken).RawData;
            }
            return Task.CompletedTask;
        }
    };
});
// 添加 httpcontext 拦截
builder.Services.AddSingleton<IAuthorizationHandler,PermissionHandler>();

//builder.Services.AddSingleton(roleRequirement);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles(); 

app.UseAuthorization();
app.UseAuthentication();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
