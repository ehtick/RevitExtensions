using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using Nice3point.BenchmarkDotNet.Revit;
using Nice3point.Revit.Extensions.Benchmarks.Benchmarks;

var configuration = ManualConfig.Create(DefaultConfig.Instance)
    .AddJob(Job.ShortRun.WithCurrentConfiguration());

BenchmarkRunner.Run<ToParameterBenchmark>(configuration);