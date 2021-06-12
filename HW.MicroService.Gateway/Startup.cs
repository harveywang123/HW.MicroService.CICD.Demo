using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Consul;
using Ocelot.Cache.CacheManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ocelot.Provider.Polly;
using IdentityServer4.AccessTokenValidation;

namespace HW.MicroService.Gateway
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
            var authenticationProviderKey = "UserGatewayKey";
            services.AddAuthentication("Bearer")
                .AddIdentityServerAuthentication(authenticationProviderKey, opt =>
                 {
                     opt.Authority = "http://192.168.1.103:7200"; //identityServer4的服务端地址
                     opt.ApiName = "UserApi";
                     opt.RequireHttpsMetadata = false;
                     opt.SupportedTokens = SupportedTokens.Both;

                 });


            services.AddControllers();
            services.AddOcelot()
                .AddConsul()
                .AddCacheManager(builer => builer.WithDictionaryHandle()) //使用 Ocelot 缓存服务
                .AddPolly();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "HW.MicroService.Gateway", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "HW.MicroService.Gateway v1"));
            }
           
            
            app.UseHttpsRedirection();

            app.UseOcelot();//配置使用 Ocelot 网关中间件。

            app.UseRouting();

           

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
