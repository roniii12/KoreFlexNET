using DAL;
using KoreFlex.Filters;
//using KoreFlex.Data;
//using KoreFlex.Model;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace KoreFlex
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<JwtKeys>(Configuration.GetSection("jwtKeys"));
            //services.AddScoped<AuthorizeJwt>();
            //services.AddDbContext<ApplicationIdentityDbContext>(options =>
            //    options.UseSqlServer(
            //        Configuration.GetConnectionString("IdentityConnection")));
            //services.AddDbContext<KoreFlexDb>(options =>
            //    options.UseSqlServer(
            //        Configuration.GetConnectionString("KoreFlexConnection")));
            services.AddDbContext<MoodyDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("MoodyConnection"),
                    b => b.MigrationsAssembly("DAL")));
            services.AddDatabaseDeveloperPageExceptionFilter();
            services.AddDataProtection()
                .UseCryptographicAlgorithms(
                    new AuthenticatedEncryptorConfiguration()
                    {
                        EncryptionAlgorithm = EncryptionAlgorithm.AES_256_CBC,
                        ValidationAlgorithm = ValidationAlgorithm.HMACSHA256
                    });
            services.AddDefaultIdentity<User>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequiredLength = 2;
                options.Password.RequiredUniqueChars = 1;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
            })
                .AddEntityFrameworkStores<MoodyDbContext>();
            //services.AddAuthorization(opts =>
            //{

            //})
            services.AddControllersWithViews();
            services.AddAuthentication("Identity.Application")
            //    opts => {
            //    opts.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            //    opts.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            //})
            .AddCookie() 
            .AddJwtBearer(opts => {
                opts.RequireHttpsMetadata = false;
                opts.SaveToken = true;
                opts.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.ASCII.GetBytes(Configuration["jwtKeys:JwtSecret"])),
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
                opts.Events = new JwtBearerEvents
                {
                    OnTokenValidated = async ctx =>
                    {
                        //var usrmgr = ctx.HttpContext.RequestServices
                        //    .GetRequiredService<UserManager<User>>();
                        //var signinmgr = ctx.HttpContext.RequestServices
                        //    .GetRequiredService<SignInManager<User>>();
                        //string username =
                        //    ctx.Principal.FindFirst(ClaimTypes.Name)?.Value;
                        //User idUser = await usrmgr.FindByNameAsync(username);
                        //ctx.Principal =
                        //    await signinmgr.CreateUserPrincipalAsync(idUser);
                    }
                };
            });
            services.ConfigureApplicationCookie(opts =>
            {
                opts.LoginPath = new PathString("/");
                opts.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                opts.Cookie.SameSite = SameSiteMode.Strict;
                opts.Cookie.IsEssential = true;
                opts.Events = new CookieAuthenticationEvents
                {
                    OnRedirectToLogin = async opt =>
                    {
                        opt.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    }
                };
            });
            services.AddAntiforgery(options => options.HeaderName = "X-XSRF-TOKEN");
            //services.AddRazorPages();
            services.AddMvc(opt =>
            {
                var policy = new AuthorizationPolicyBuilder()
                     .RequireAuthenticatedUser()
                     .Build();
                opt.Filters.Add(new AuthorizeFilter(policy));
                opt.Filters.Add(typeof(AuthorizeJwt));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //app.Use((context, next) =>
            //{
            //    var ip = context.Request.HttpContext.Connection.RemoteIpAddress;


            //})

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.Use(async (context, next) =>
            {
                string path = context.Request.Path.Value;
                if (path != null && !path.ToLower().Contains("/login"))
                {
                    var prop = new Microsoft.AspNetCore.Authentication.AuthenticationProperties();
                    var user = context.User;
                    var ip = context.Response.HttpContext.Connection.RemoteIpAddress;
                }
                if (path != null && path.ToLower().Contains("/registers"))
                {
                    string tokenHead = context.Request.Headers["Authorization"];
                    tokenHead = tokenHead.Replace("Bearer ","");
                    //string token = tokenHead.ToString();
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var validParams = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.ASCII.GetBytes(Configuration["jwtSecret"])),
                        ValidateAudience = false,
                        ValidateIssuer = false,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero
                    };
                    SecurityToken securityToken; // = tokenHandler.ReadToken(tokenHead) as JwtSecurityToken;
                    var valid = tokenHandler.ValidateToken(tokenHead, validParams, out securityToken);
                    var securityTokenJwt = securityToken as JwtSecurityToken;
                    var stringClaimValue = securityTokenJwt.Claims.First(claim => claim.Type == "unique_name").Value;
                    var str = stringClaimValue;
                }
                await next();
            });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                       name: "default",
                       pattern: "{controller=Home}/{action=Index}/{id?}");
                //endpoints.MapRazorPages();
            });
        }
    }
}
