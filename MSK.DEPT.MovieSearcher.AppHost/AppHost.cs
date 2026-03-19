using Scalar.Aspire;

var builder = DistributedApplication.CreateBuilder(args);

var cache = builder.AddRedis("cache");

var server = builder.AddProject<Projects.MSK_DEPT_MovieSearcher_Server>("server")
    .WithReference(cache)
    .WaitFor(cache)
    .WithHttpHealthCheck("/health")
    .WithExternalHttpEndpoints();

var scalar = builder.AddScalarApiReference();
scalar.WithApiReference(server);

builder.Build().Run();
