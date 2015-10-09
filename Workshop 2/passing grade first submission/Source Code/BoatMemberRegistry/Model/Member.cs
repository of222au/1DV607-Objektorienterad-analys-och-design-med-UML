using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace BoatMemberRegistry.Model
{
    [DataContract]
    public class Member : BoatObserver
    {
        private string m_name;
        private string m_personalNumber;
        private string m_uniqueId;
        private List<Boat> m_boats;

        private List<MemberObserver> m_observers;

        //Constructors
        public Member()
        {
            this.Initialize();
        }
        public Member(string name, string personalNumber, string uniqueId, List<Boat> boats = null)
        {
            this.Initialize();

            this.Name = name;
            this.PersonalNumber = personalNumber;
            this.UniqueId = uniqueId;
            this.Boats = (boats != null ? boats : new List<Boat>());
        }
        private void Initialize()
        {
            m_observers = new List<MemberObserver>();
            if (this.Boats == null) { this.Boats = new List<Boat>(); }
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
                    if (m_name != trimmed)
                    {
                        m_name = trimmed;
                        this.NotifySubscribersChangeMade();
                    }
                }
                else
                {
                    throw new Exception("Member name must be at least 3 characters long.");
                }
            }
        }

        [DataMember]
        public string PersonalNumber
        {
            get
            {
                return m_personalNumber;
            }
            set
            {
                string trimmed = value.Trim();
                //if length is 11 and with a "-" (like swedish personal number 123456-7890), then remove the "-" temporary
                if (trimmed.Length == 11 && trimmed[6] == '-')
                {
                    trimmed = trimmed.Substring(0, 6) + trimmed.Substring(7, 4);
                }

                long n;
                if (trimmed.Length == 10 && long.TryParse(trimmed, out n))
                {
                    //add the "-"
                    trimmed = trimmed.Substring(0, 6) + "-" + trimmed.Substring(6, 4);

                    if (m_personalNumber != trimmed)
                    {
                        m_personalNumber = trimmed;
                        this.NotifySubscribersChangeMade();
                    }
                }
                else
                {
                    throw new Exception("Personal number was not filled in correctly.");
                }
            }
        }

        [DataMember]
        public string UniqueId
        {
            get
            {
                return m_uniqueId;
            }
            private set
            {
                if (m_uniqueId != value)
                {
                    m_uniqueId = value;
                    this.NotifySubscribersChangeMade();
                }
            }
        }

        [DataMember]
        public List<Boat> Boats
        {
            get
            {
                return m_boats;
            }
            private set
            {
                m_boats = value;
                this.NotifySubscribersChangeMade();
            }
        }

        //readonly
        public int BoatCount
        {
            get
            {
                return this.Boats.Count();
            }
        }
        public bool HasCorrectDetails
        {
            get
            {
                if (!String.IsNullOrWhiteSpace(this.Name) &&
                    !String.IsNullOrWhiteSpace(this.PersonalNumber) &&
                    !String.IsNullOrWhiteSpace(this.UniqueId))
                {
                    return true;
                }

                return false;
            }
        }


        //Methods
        public bool AddBoat(Boat a_boat)
        {
            if (a_boat.HasCorrectDetails)
            {
                //subscribe to boat events
                a_boat.AddSubscriber(this);

                //add it to array
                m_boats.Add(a_boat);

                //notify subscribers
                this.NotifySubscribersChangeMade();

                return true;
            }

            return false;
        }

        public bool DeleteBoat(Boat a_boat)
        {
            bool success = this.Boats.Remove(a_boat);

            if (success)
            {
                //un-subscribe to boat events
                a_boat.RemoveSubscriber(this);

                this.NotifySubscribersChangeMade();
            }

            return success;
        }

        public void SetUniqueId(string uniqueId)
        {
            this.UniqueId = uniqueId;
        }

        public Boat GetBoat(int index)
        {
            if (index >= 0 && index < this.BoatCount)
            {
                return this.Boats[index];
            }
            return null;
        }

        //subscriber methods
        public void AddSubscriber(MemberObserver a_sub)
        {
            if (!m_observers.Contains(a_sub))
            {
                m_observers.Add(a_sub);
            }
        }
        public bool RemoveSubscriber(MemberObserver a_sub)
        {
            return m_observers.Remove(a_sub);
        }
        public void SetupSubscriptions()
        {
            foreach (Boat boat in m_boats)
            {
                boat.AddSubscriber(this);
            }
        }
        private void NotifySubscribersChangeMade()
        {
            if (m_observers != null)
            {
                foreach (MemberObserver o in m_observers)
                {
                    o.ChangeMade(this);
                }
            }
        }

        //BoatObserver method(s)
        public void ChangeMade(Model.Boat a_boat)
        {
            //send the call along to the subscribers
            this.NotifySubscribersChangeMade();
        }

    }
}
