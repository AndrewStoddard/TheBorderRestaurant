using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace TheBorderRestaurantTests.TestLibrary
{
    public class FakeSession : ISession
    {
        #region Data members

        private readonly Dictionary<string, object> sessionStorage = new();

        #endregion

        #region Properties

        public object this[string name]
        {
            get => this.sessionStorage[name];
            set => this.sessionStorage[name] = value;
        }

        public bool IsAvailable { get; }
        public string Id { get; }
        public IEnumerable<string> Keys { get; }

        #endregion

        #region Methods

        public Task LoadAsync(CancellationToken cancellationToken = new())
        {
            throw new NotImplementedException();
        }

        public Task CommitAsync(CancellationToken cancellationToken = new())
        {
            throw new NotImplementedException();
        }

        public bool TryGetValue(string key, out byte[] value)
        {
            try
            {
                if (this.sessionStorage[key] != null)
                {
                    value = Encoding.BigEndianUnicode.GetBytes(this.sessionStorage[key].ToString());
                    return true;
                }

                value = null;
                return false;
            }
            catch (Exception e)
            {
                value = null;
                return false;
            }
        }

        public void Set(string key, byte[] value)
        {
            this.sessionStorage[key] = Encoding.BigEndianUnicode.GetString(value);
        }

        public void Remove(string key)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}