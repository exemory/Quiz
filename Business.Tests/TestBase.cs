using AutoFixture;

namespace Business.Tests;

public abstract class TestBase
{
    protected readonly IFixture Fixture = UnitTestHelper.CreateFixture();
}