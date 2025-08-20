using csmic;
using csmic.stdlib;
using NUnit.Framework;
using stdlib;
using stdlib.functions;
using System.Globalization;
using System.Reflection.Metadata;

namespace tests;

public class StdlibFunctionsTests
{
    private InputInterpreter _interp = null!;

    [SetUp]
    public void Setup()
    {
        CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
        _interp = new InputInterpreter();
        Constants.Initialize(_interp);
        Functions.Initialize(_interp); ;
    }
}

