﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Shared.Extensions.Configurations;

namespace Shared.Extensions.DependencyInjection;

public static class DependencyInjectionRepositoriesExtensions
{
    public static IServiceCollection AddCollection<TRepository, TImplementation>(
        this IServiceCollection services,
        string collectionName)
        where TRepository : class
        where TImplementation : class, TRepository
    {
        services.AddSingleton<TRepository, TImplementation>();

        services.AddSingleton<IMongoCollection<TImplementation>>(sp =>
        {
            var mongoDb = sp.GetRequiredService<IMongoDatabase>();

            return mongoDb.GetCollection<TImplementation>(collectionName);
        });

        return services;
    }

    public static IServiceCollection AddMongo(this IServiceCollection services)
    {
        services.AddSingleton<IMongoClient>(sp =>
        {
            var mongoDbSettings = sp.GetRequiredService<IOptions<IMongoConfiguration>>();

            return new MongoClient(mongoDbSettings.Value.ConnectionString);
        });

        services.AddSingleton<IMongoDatabase>(sp =>
        {
            var mongoDbSettings = sp.GetRequiredService<IOptions<IMongoConfiguration>>();

            var mongoClient = sp.GetRequiredService<IMongoClient>();

            return mongoClient.GetDatabase(mongoDbSettings.Value.DatabaseName);
        });

        return services;
    }
}