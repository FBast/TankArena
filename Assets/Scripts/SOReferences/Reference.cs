using System;
using SOReferences;

namespace Data.SOReferences {
    /// <summary>
    /// Reference Class.
    /// </summary>
    [Serializable]
    public abstract class Reference
    {
    }

    /// <summary>
    /// Reference Class.
    /// </summary>
    [Serializable]
    public class Reference<T, G> : Reference where G : Variable<T>
    {
        public bool UseConstant = true;

        public T ConstantValue;

        public G Variable;

        public Reference() { }
        public Reference(T value)
        {
            UseConstant = true;
            ConstantValue = value;
        }

        public T Value
        {
            get { return UseConstant ? ConstantValue : Variable.Value; }
            set
            {
                if (UseConstant)
                    ConstantValue = value;
                else
                    Variable.Value = value;
            }
        }

        public static implicit operator T(Reference<T, G> Reference)
        {
            return Reference.Value;
        }

        public static implicit operator Reference<T, G>(T Value)
        {
            return new Reference<T, G>(Value);
        }
    }
}