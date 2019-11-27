using System.Collections.Generic;
using UnityEngine;

namespace SOReferences.DictionaryStringObjectReference {
    [CreateAssetMenu(fileName = "DictionaryStringObject_Variable", menuName = "SOVariable/DictionaryStringObject")]
    public class DictionaryStringObjectVariable : Variable<Dictionary<string, object>> { }
}