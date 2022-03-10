using WebApi.Test.Helpers.Context.Bar.Contracts;

namespace WebApi.Test.Helpers.Context.Bar
{
    public class Bar : IBar
    {
        public int SomeOperation(int someNumber)
        {
            return someNumber + 1;
        }
    }
}
