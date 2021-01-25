using StackExchange.Redis;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace RouletteWebAPI.BackEnd
{
    /// <summary>
    /// Expose operations for access and data manipulation over Redis cache storage service.
    /// </summary>
    public class RedisRepository
    {
        #region Properties
        private static string ConnectionString { get; set; }

        /// <summary>
        /// Deferred initializer of connection instance to Redis cache storage service.
        /// </summary>
        private Lazy<ConnectionMultiplexer> LazyConnection = new Lazy<ConnectionMultiplexer>(() =>
        {
            return ConnectionMultiplexer.Connect(ConnectionString);
        });

        /// <summary>
        ///Active connection instance to Redis cache storage service.
        /// </summary>
        private ConnectionMultiplexer Connection
        {
            get
            {
                return LazyConnection.Value;
            }
        }

        /// <summary>
        /// Database instance of an active connection to Redis cache service.
        /// </summary>
        private IDatabase CacheService
        {
            get
            {
                return Connection.GetDatabase();
            }
        }
        #endregion Properties

        public RedisRepository(string connectionString)
        {
            ConnectionString = connectionString;
        }

        #region Methods
        /// <summary>
        /// Retrieve an stored element instance identified with a given key value.
        /// </summary>
        /// <typeparam name="T">Datatype of element to retrieve.</typeparam>
        /// <param name="key">Element key.</param>
        /// <returns>Instance of searched element.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        public T Get<T>(string key)
        {
            return Deserialize<T>(CacheService.StringGet(key));
        }

        /// <summary>
        ///Persist an object instance on opened database of an active Redis cache service connection.
        /// </summary>
        /// <param name="key">Element key.</param>
        /// <param name="value">Object instance to persist.</param>
        public void Save<T>(string key, T value)
        {
            CacheService.StringSet(key, Serialize(value));
        }

        /// <summary>
        /// Persist a given object instance on binary format.
        /// </summary>
        /// <param name="objectValue">Object instance to serialize.</param>
        /// <returns>Bytes sequence that represent a serialized object.</returns>
        private byte[] Serialize<T>(T objectValue)
        {
            if (objectValue == null)
            {
                return null;
            }
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            using (MemoryStream memoryStream = new MemoryStream())
            {
                binaryFormatter.Serialize(memoryStream, objectValue);
                byte[] objectDataAsStream = memoryStream.ToArray();
                return objectDataAsStream;
            }
        }

        /// <summary>
        /// Allow rebuild an object instance from a given bytes sequence.
        /// </summary>
        /// <typeparam name="T">Datatype of instance to deserialize.</typeparam>
        /// <param name="stream">Bytes sequence that contain an object instance.</param>
        /// <returns>Deserialized object instance.</returns>
        private T Deserialize<T>(byte[] stream)
        {
            T deserializeResult = default(T);
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            if (stream == null)
            {
                deserializeResult = default(T);
            }
            try
            {
                using (MemoryStream memoryStream = new MemoryStream(stream))
                {
                    T result = (T)binaryFormatter.Deserialize(memoryStream);
                    deserializeResult = result;
                }
            }
            catch
            {
                deserializeResult = default(T);
            }
            return deserializeResult;
        }
        #endregion Methods
    }
}
