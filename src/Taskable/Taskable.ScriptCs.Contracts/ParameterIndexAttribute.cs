using System;

namespace TaskableScriptCs.Contracts
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class ParameterIndexAttribute : Attribute
    {
        private readonly int _index;

        public ParameterIndexAttribute(int index)
        {
            _index = index;
        }

        public int Index
        {
            get
            {
                if (_index < 0)
                    throw new InvalidOperationException("Invalid index was identified");
                return _index;
            }
        }
    }
}
