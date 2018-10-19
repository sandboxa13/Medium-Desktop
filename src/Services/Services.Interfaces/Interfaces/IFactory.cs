namespace Services.Interfaces.Interfaces
{
    public interface IFactory<out T> where T :class
    {
        T Create();
    }
}
