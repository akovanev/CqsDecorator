namespace CqsDecorators
{
    public interface IDecoratorFactory
    {
        T BuildDecoratorsChain<T>();
    }
}
