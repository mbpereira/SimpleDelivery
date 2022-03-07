using WebApi.Test.Helpers.Context.Foo.Contracts;

namespace WebApi.Test.Helpers.Context.Foo
{
    public class Foo : IFoo
    {
        public int OtherOperation(int someNumber)
        {
            return someNumber + 2;
        }
    }
}
