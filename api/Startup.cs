using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OnlineChat.Hubs;
using OnlineChat.Repository;
using OnlineChat.Services;
using OnlineChat.Services.Interfaces;
using Swashbuckle.AspNetCore.Swagger;

namespace OnlineChat
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                .AddJsonOptions(
                    options => options.SerializerSettings.ReferenceLoopHandling =
                        Newtonsoft.Json.ReferenceLoopHandling.Ignore
                );
            services.AddDbContext<RepositoryContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")), ServiceLifetime.Singleton);

            services.AddSingleton<IChatService, ChatService>();
            services.AddSingleton<IMessageService, MessageService>();
            services.AddTransient<IUserService, UserService>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info {Title = "Online Chat application.", Version = "v1"});
            });
            services.AddCors();
            services.AddSignalR();
            services.AddCors(o => o.AddPolicy("CorsPolicy", builder =>
            {
                builder
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .WithOrigins("http://localhost:3000")
                    .AllowCredentials();
            }));
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
        
            app.UseCors("CorsPolicy");
            app.UseSwagger();

            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"); });
            //app.UseStaticFiles();
            
            app.UseSignalR(routes => { routes.MapHub<ChatHub>("/chathub"); });
            
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}