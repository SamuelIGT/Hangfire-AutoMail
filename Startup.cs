using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using Hangfire;
using Hangfire.SqlServer;
using HangfireEmailSchedule.Repository;
using HangfireEmailSchedule.Service;
using HangfireEmailSchedule.Util;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace HangfireEmailSchedule
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
            services.AddDbContext<DBContext>(options => 
                options.UseSqlServer(Configuration.GetConnectionString("ConnectionStr"))
            );

            services.AddHangfire(configuration => {
                configuration.UseSqlServerStorage(Configuration.GetConnectionString("ConnectionStrHangFire"));
            });

            RegisterServices(services);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        private void RegisterServices(IServiceCollection services) {
            services.AddTransient<IPost, PostRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            app.UseHangfireServer();
            app.UseHangfireDashboard();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();

            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var services = serviceScope.ServiceProvider;
                var db = services.GetService<DBContext>();

                EmailManager emailManager = new EmailManager();
                DateTime now = DateTime.Now;
                int hour = now.Hour;
                int minute = now.Minute;
               
                string to = "samuel_alves@atlantico.com.br";
                string from = "samuel.br.igt@gmail.com";
                string text = $"Email teste: {now.ToString()} <br> Posts Existentes Post: <br>{db.Posts.AsEnumerable().Count()}";

                RecurringJob.AddOrUpdate("EmailRecurringJob2",
                    () => emailManager.SendEmail("Samuel Alves", to, from, text),
                    Cron.Minutely);
            }


        }
    }
}
