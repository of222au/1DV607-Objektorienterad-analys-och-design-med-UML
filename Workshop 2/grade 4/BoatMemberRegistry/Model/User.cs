using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;

namespace BoatMemberRegistry.Model
{
    [DataContract]
    public class User
    {
        private string m_name;
        private string m_password;

        private List<UserObserver> m_observers;

        public User()
        {
            this.Initialize();
        }
        public User(string name, string password)
        {
            this.Initialize();

            this.Name = name;
            this.Password = password;
        }

        private void Initialize()
        {
            m_observers = new List<UserObserver>();
        }

        //Properties
        [DataMember]
        public string Name
        {
            get
            {
                return m_name;
            }
            set
            {
                string trimmed = value.Trim();
                if (trimmed.Length > 2)
                {
                    if (Regex.IsMatch(value, @"^[_âãäåæçèéêëìíîïðñòóôõøùúûüýþÿa-zA-Z-]+$"))
                    {
                        if (m_name != trimmed)
                        {
                            m_name = trimmed;
                            this.NotifySubscribersChangeMade();
                        }
                    }
                    else
                    {
                        throw new Exception("User name can only contain letters and underscore");
                    }
                }
                else
                {
                    throw new Exception("User name must be at least 3 characters long.");
                }
            }
        }

        [DataMember]
        public string Password
        {
            get
            {
                return m_password;
            }
            set
            {
                if (value.Length > 5)
                {
                    if (m_password != value)
                    {
                        m_password = value;
                        this.NotifySubscribersChangeMade();
                    }
                }
                else
                {
                    throw new Exception("Password must be at least 6 characters long.");
                }
            }
        }

        //readonly
        public bool HasCorrectDetails
        {
            get
            {
                if (!String.IsNullOrWhiteSpace(this.Name) &&
                    !String.IsNullOrWhiteSpace(this.Password))
                {
                    return true;
                }

                return false;
            }
        }

        //Methods

        //subscriber methods
        public void AddSubscriber(UserObserver a_sub)
        {
            if (!m_observers.Contains(a_sub))
            {
                m_observers.Add(a_sub);
            }
        }
        public bool RemoveSubscriber(UserObserver a_sub)
        {
            return m_observers.Remove(a_sub);
        }
        private void NotifySubscribersChangeMade()
        {
            if (m_observers != null)
            {
                foreach (UserObserver o in m_observers)
                {
                    o.UserChangeMade(this);
                }
            }
        }
    }
}
