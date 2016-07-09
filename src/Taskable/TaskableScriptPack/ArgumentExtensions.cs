using TaskableCore;

namespace TaskableScriptPack
{
    public static class ArgumentExtensions
    {
        public static Options CreateAsOptions(this Arguments arguments)
        {
            return new Options();
        }
    }
}
