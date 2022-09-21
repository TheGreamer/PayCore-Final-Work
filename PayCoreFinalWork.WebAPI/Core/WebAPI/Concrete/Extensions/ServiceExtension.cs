using FluentValidation.AspNetCore;
using PayCoreFinalWork.Business.Abstract;
using PayCoreFinalWork.Business.Concrete.Services;
using PayCoreFinalWork.Business.Concrete.ValidationRules;
using PayCoreFinalWork.Core.WebAPI.Concrete.Utilities;
using PayCoreFinalWork.DataAccess.Abstract;
using PayCoreFinalWork.DataAccess.Concrete;
using System.Reflection;

namespace PayCoreFinalWork.Core.WebAPI.Concrete.Extensions
{
    public static class ServiceExtension
    {
        // Tüm session ve service kayıtlarının eklenebilmesi için gereken extension metod.
        public static IServiceCollection AddRegisteries(this IServiceCollection services)
        {
            services.AddScoped<ICategorySession, CategorySession>()
                    .AddScoped<ICategoryService, CategoryService>()
                    .AddScoped<IProductSession, ProductSession>()
                    .AddScoped<IProductService, ProductService>()
                    .AddScoped<IAccountSession, AccountSession>()
                    .AddScoped<IAccountService, AccountService>()
                    .AddScoped<IOfferSession, OfferSession>()
                    .AddScoped<IOfferService, OfferService>()
                    .AddScoped<ITokenService, TokenService>()
                    .AddScoped<IEmailService, EmailService>();

            return services;
        }

        // AutoMapper konfigürasyonun yapılması için gereken extension metod.
        public static IServiceCollection ConfigureAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            return services;
        }

        // Tüm validator sınıflarının konfigürasyonunun yapılması için gereken extension metod.
        public static IMvcBuilder ConfigureFluentValidation(this IMvcBuilder services)
        {   
            services.AddFluentValidation(f => f.RegisterValidatorsFromAssemblyContaining<LoginValidator>())
                    .AddFluentValidation(f => f.RegisterValidatorsFromAssemblyContaining<AccountValidator>())
                    .AddFluentValidation(f => f.RegisterValidatorsFromAssemblyContaining<CategoryValidator>())
                    .AddFluentValidation(f => f.RegisterValidatorsFromAssemblyContaining<ProductValidator>())
                    .AddFluentValidation(f => f.RegisterValidatorsFromAssemblyContaining<OfferValidator>());

            return services;
        }

        // Tarih bilgilerinin JSON çıktısını değiştirebilmek için gereken extension metod.
        public static IMvcBuilder ConfigureCustomDateTime(this IMvcBuilder services)
        {
            services.AddJsonOptions(j => j.JsonSerializerOptions.Converters.Add(new DateTimeConverter()));
            return services;
        }
    }
}