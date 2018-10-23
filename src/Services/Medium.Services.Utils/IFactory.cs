namespace Medium.Services.Utils
{
    public interface IFactory<out T> where T :class
    {
        T Create();
    }
}
