using System.Collections.Generic;
using System.Linq;

namespace Exchange.Web.BusinessLogic.Helpers
{
    public class ConnectionMappingHelper<T>
    {
        private readonly Dictionary<T, HashSet<string>> _connections =
            new Dictionary<T, HashSet<string>>();

        public int Count
        {
            get => _connections.Count;
        }

        public void Add(T key, string connectionId)
        {
            lock (_connections)
            {
                if (!_connections.TryGetValue(key, out HashSet<string> connections))
                {
                    connections = new HashSet<string>();
                    _connections.Add(key, connections);
                }

                lock (connections)
                {
                    connections.Add(connectionId);
                }
            }
        }

        public IEnumerable<string> GetConnections(T key)
        {
            if (_connections.TryGetValue(key, out HashSet<string> connections))
            {
                return connections;
            }

            return Enumerable.Empty<string>();
        }

        public void Remove(T key, string connectionId)
        {
            lock (_connections)
            {
                if (!_connections.TryGetValue(key, out HashSet<string> connections))
                {
                    return;
                }

                lock (connections)
                {
                    connections.Remove(connectionId);

                    if (connections.Count == default(int))
                    {
                        _connections.Remove(key);
                    }
                }
            }
        }
    }
}
