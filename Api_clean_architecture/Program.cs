using Tecnicos.Services.DI;


           var builder = WebApplication.CreateBuilder(args);
            /*builder.Services.AddDbContext<Api_clean_architectureContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("Api_clean_architectureContext") ?? throw new InvalidOperationException("Connection string 'Api_clean_architectureContext' not found.")));*/

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //Inyeccion del contexto
            builder.Services.RegisterServices();


            var app = builder.Build();

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