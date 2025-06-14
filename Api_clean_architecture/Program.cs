using Api_clean_architecture.Context;
using Microsoft.EntityFrameworkCore;
using Tecnicos.Data.Context;
using Tecnicos.Services.DI;


           var builder = WebApplication.CreateBuilder(args);
            /*builder.Services.AddDbContext<Api_clean_architectureContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("Api_clean_architectureContext") ?? throw new InvalidOperationException("Connection string 'Api_clean_architectureContext' not found.")));*/

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

        // Carga la configuración
        builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

        // Registra el DbContextFactory
        builder.Services.RegisterDbContextFactory(builder.Configuration);

            builder.Services.AddDbContextFactory<TecnicosContext>(o => 
            o.UseSqlServer(builder.Configuration.GetConnectionString("SqlConStr"))
            );

                        // Inyección del contexto
            builder.Services.RegisterServices(builder.Configuration);


var app = builder.Build();
            // Redirigir la raíz a la ruta de Swagger
            app.MapGet("/", () => Results.Redirect("/swagger/index.html"));

            // Configure the HTTP request pipeline.
            /* if (app.Environment.IsDevelopment())
           {*/
                app.UseSwagger();
                app.UseSwaggerUI();
           // }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();