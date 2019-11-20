using System.IO;
using OdinSerializer;

namespace Utils {
    public static class OdinSerializerApi {
        
        public static void Serialize(object value, Stream stream) {
            SerializationUtility.SerializeValue(value, stream, DataFormat.JSON);
        }

        public static object Deserialize<T>(byte[] bytes) {
            return SerializationUtility.DeserializeValue<T>(bytes, DataFormat.JSON);
        }
        
    }
}