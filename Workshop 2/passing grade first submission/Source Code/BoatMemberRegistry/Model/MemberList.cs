using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace BoatMemberRegistry.Model
{
    [DataContract]
    public class MemberList : MemberObserver
    {
        private List<Member> m_members;

        private List<MemberObserver> m_observers;

        //Constructor   
        public MemberList()
        {
            this.Initialize();
        }
        private void Initialize()
        {
            m_observers = new List<MemberObserver>();
            if (m_members == null) { m_members = new List<Member>(); }
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
        private List<Member> Members
        {
            get
            {
                return m_members;
            }
            set
            {
                m_members = value;
            }
        }
        public int MemberCount
        {
            get
            {
                return m_members.Count();
            }
        }

        //Methods
        public IEnumerable<Member> GetMembers()
        {
            return m_members.AsEnumerable<Member>();
        }
        public Member GetMember(int index)
        {
            if (index >= 0 && index < this.MemberCount)
            {
                return m_members[index];
            }
            return null;
        }
        public Member FindMember(string uniqueId)
        {
            foreach (Member member in m_members)
            {
                if (member.UniqueId == uniqueId)
                {
                    return member;
                }
            }

            return null;
        }

        public bool AddMember(Member a_member, bool generateUniqueId = false)
        {
            //check if to generate unique id for the member
            if (generateUniqueId) //String.IsNullOrWhiteSpace(a_member.UniqueId) && 
            {
                a_member.SetUniqueId(this.generateUniqueMemberId());
            }

            if (a_member.HasCorrectDetails)
            {
                //subscribe to member events
                a_member.AddSubscriber(this);

                //add it to array
                m_members.Add(a_member);

                //notify subscribers
                this.NotifySubscribersChangeMade(a_member);

                return true;
            }

            return false;
        }

        public bool DeleteMember(string uniqueId)
        {
            Member member = this.FindMember(uniqueId);
            if (member != null)
            {
                return this.DeleteMember(member);
            }
            return false;
        }
        private bool DeleteMember(Member a_member)
        {
            bool success = this.Members.Remove(a_member);

            if (success)
            {
                //un-subscribe to member events
                a_member.RemoveSubscriber(this);

                this.NotifySubscribersChangeMade(a_member);
            }

            return success;
        }

        private string generateUniqueMemberId()
        {
            string memberId = "";
            do
            {
                memberId = this.generateRandomString();
            }
            while (!this.isMemberIdUnique(memberId));

            return memberId;
        }
        private bool isMemberIdUnique(string memberId)
        {
            foreach (Model.Member member in m_members)
            {
                if (member.UniqueId == memberId)
                {
                    //not unique
                    return false;
                }
            }

            return true;
        }
        private string generateRandomString()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            var result = new string(
                Enumerable.Repeat(chars, 8)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());
            return result;
        }

        //subscriber methods
        public void AddSubscriber(MemberObserver a_sub)
        {
            if (!m_observers.Contains(a_sub))
            {
                m_observers.Add(a_sub);
            }
        }
        private void NotifySubscribersChangeMade(Member a_member)
        {
            if (m_observers != null)
            {
                foreach (MemberObserver o in m_observers)
                {
                    o.ChangeMade(a_member);
                }
            }
        }
        public void SetupSubscriptions()
        {
            foreach (Member member in m_members)
            {
                //subscribe to the member's events..
                member.AddSubscriber(this);

                //..and setup the member's subscriptions to its sub-class(es) as well
                member.SetupSubscriptions();
            }
        }

        //MemberObserver method(s)
        public void ChangeMade(Model.Member a_member)
        {
            //send the call along to the subscribers
            this.NotifySubscribersChangeMade(a_member);
        }

    }
}
