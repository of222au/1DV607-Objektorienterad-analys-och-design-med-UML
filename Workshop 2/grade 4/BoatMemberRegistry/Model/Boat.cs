using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace BoatMemberRegistry.Model
{
    [DataContract]
    public class Boat
    {
        public enum BoatType
        {
            Undefined = 0,
            Sailboat = 1,
            Motorsailer = 2,
            Kayak__Canoe = 3,
            Other = 4
        }

        private BoatType m_type;
        private double m_length; // in meters

        private List<BoatObserver> m_observers;

        //Constructors
        public Boat()
        {
            this.Initialize();
        }
        public Boat(BoatType type, double length)
        {
            this.Initialize();

            m_type = type;
            m_length = length;
        }
        private void Initialize()
        {
            m_observers = new List<BoatObserver>();
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
        public BoatType Type
        {
            get
            {
                return m_type;
            }
            set
            {
                if (value != BoatType.Undefined)
                {
                    if (m_type != value)
                    {
                        m_type = value;
                        this.NotifySubscribersChangeMade();
                    }
                }
                else
                {
                    throw new Exception("Boat type can not be undefined.");
                }
            }
        }

        [DataMember]
        public double Length
        {
            get
            {
                return m_length;
            }
            set
            {
                if (value > 0 && value <= 100)
                {
                    if (m_length != value)
                    {
                        m_length = value;
                        this.NotifySubscribersChangeMade();
                    }
                }
                else
                {
                    throw new Exception("Boat length must be larger than 0 and smaller or equal to 100 meters.");
                }
            }
        }

        //readonly
        public string TypeAsString
        {
            get
            {
                return Enum.GetName(typeof(BoatType), m_type).Replace("__", "/");  
            }
        }

        public bool HasCorrectDetails
        {
            get
            {
                if (this.Type != BoatType.Undefined &&
                    this.Length != 0) //the rest of the length 'rules' are checked when setting Length
                {
                    return true;
                }

                return false;
            }
        }

        //subscriber methods
        public void AddSubscriber(BoatObserver a_sub)
        {
            if (!m_observers.Contains(a_sub))
            {
                m_observers.Add(a_sub);
            }
        }
        public bool RemoveSubscriber(BoatObserver a_sub)
        {
            return m_observers.Remove(a_sub);
        }
        private void NotifySubscribersChangeMade()
        {
            if (m_observers != null)
            {
                foreach (BoatObserver o in m_observers)
                {
                    o.MemberChangeMade(this);
                }
            }
        }
    }
}
