
namespace cApp.PositiveT.Rpg.Infrastruct
{
    public interface IMessenger
    {
        void Write(string s);
        string ReadKey();
    }
}
