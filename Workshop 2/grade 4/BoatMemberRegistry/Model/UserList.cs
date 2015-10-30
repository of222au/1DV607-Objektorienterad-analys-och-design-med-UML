using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace BoatMemberRegistry.Model
{
    [DataContract]
    public class UserList : UserObserver
    {
        private List<User> m_users;

        private List<UserObserver> m_observers;

        //Constructor   
        public UserList()
        {
            this.Initialize();
        }
        private void Initialize()
        {
            m_observers = new List<UserObserver>();
            if (m_users == null) { m_users = new List<User>(); }
        }

        // This method is called after the object 
        // is completely deserialized. 
        [OnDeserialized]
        void OnDeserialized(StreamingContext context)
        {
            this.Initialize();
        }

        //Properties
        [DataMember]
        private List<User> Users
        {
            get
            {
                return m_users;
            }
            set
            {
                m_users = value;
            }
        }
        public int UserCount
        {
            get
            {
                return m_users.Count();
            }
        }

        //Methods
        public IEnumerable<User> GetUsers()
        {
            return m_users.AsEnumerable<User>();
        }
        public User GetUser(int index)
        {
            if (index >= 0 && index < this.UserCount)
            {
                return m_users[index];
            }
            return null;
        }
        public User FindUser(string name)
        {
            foreach (User user in m_users)
            {
                if (user.Name == name)
                {
                    return user;
                }
            }

            return null;
        }

        public bool AddUser(User a_user)
        {
            if (IsUsernameUnique(a_user.Name) && a_user.HasCorrectDetails)
            {
                //subscribe to user events
                a_user.AddSubscriber(this);

                //add it to array
                m_users.Add(a_user);

                //notify subscribers
                this.NotifySubscribersChangeMade(a_user);

                return true;
            }

            return false;
        }

        private bool DeleteUser(User a_user)
        {
            bool success = this.Users.Remove(a_user);

            if (success)
            {
                //un-subscribe to user events
                a_user.RemoveSubscriber(this);

                this.NotifySubscribersChangeMade(a_user);
            }

            return success;
        }

        private bool IsUsernameUnique(string username)
        {
            foreach (Model.User user in m_users)
            {
                if (user.Name == username)
                {
                    //not unique
                    return false;
                }
            }

            return true;
        }

        public bool CheckUserCredentials(User userCredentials)
        {
            foreach (User user in m_users)
            {
                if (user.Name == userCredentials.Name)
                {
                    if (user.Password == userCredentials.Password)
                    {
                        return true;
                    }
                }
            }

            return false;
        }


        //subscriber methods
        public void AddSubscriber(UserObserver a_sub)
        {
            if (!m_observers.Contains(a_sub))
            {
                m_observers.Add(a_sub);
            }
        }
        private void NotifySubscribersChangeMade(User a_user)
        {
            if (m_observers != null)
            {
                foreach (UserObserver o in m_observers)
                {
                    o.UserChangeMade(a_user);
                }
            }
        }
        public void SetupSubscriptions()
        {
            foreach (User user in m_users)
            {
                //subscribe to the user's events..
                user.AddSubscriber(this);
            }
        }

        //UserObserver method(s)
        public void UserChangeMade(Model.User a_user)
        {
            //send the call along to the subscribers
            this.NotifySubscribersChangeMade(a_user);
        }

    }
}
