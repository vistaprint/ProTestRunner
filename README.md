# ProTestRunner #
Programmatic Test Runner is a small package for running NUnit tests contained in an assembly from .NET code.

The default tests discoverage is done by an NUnit `RemoteTestRunner`, or you can provide your own.

This package provides a default implementation of the `EventListener` NUnit interface that outputs to Console when a run starts, ends, when there's an unhandled exception in the tests, when a test starts, finished, and when a test outputs a line. It also allows you to define your own implementation of `EventListener` or modify the existing one to define what happens when a test outputs a line, and a run finishes.

Some sample usages can be found in `ProTestRunner.UnitTests`.

## Dependencies ##
ProTestRunner relies on `NUnit 2.6.4` and `NUnit.Runners 2.6.3` NuGet packages to obtain the `NUnit.Core` `and NUnit.Core.Interfaces` dlls.

## License ##
This is licensed under the Apache 2.0 license.