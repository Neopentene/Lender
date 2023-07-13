using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lender.Models
{
    public class Condition<T>
    {
        readonly List<Func<T, Exception?>> conditions;

        T data;

        public bool HasError;
        public T Data
        {
            get => data;
            set
            {
                Check(value, true);
                data = value;
            }
        }

        public Condition(T data, IEnumerable<Func<T, Exception?>>? conditionalLogic = null)
        {
            conditions = conditionalLogic != null ? new(conditionalLogic) : new();
            this.data = data;
            Check(this.data, true);
        }

        public void AddConditions(IEnumerable<Func<T, Exception?>> conditionalLogic) => conditions.AddRange(conditionalLogic);

        public Exception? Check(T value, bool modifyFlag = false)
        {
            foreach (Func<T, Exception?> condition in conditions)
                if (condition(value) is Exception result) { HasError = modifyFlag || HasError; return result; }
            HasError = !modifyFlag && HasError;
            return null;
        }
        
        public static implicit operator T(Condition<T> dataHolder) => dataHolder.Data;
    }
}
