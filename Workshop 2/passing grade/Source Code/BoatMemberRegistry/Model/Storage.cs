using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.IO;

namespace BoatMemberRegistry.Model
{
    class Storage
    {
        private const string dataFolderName = "data";
        private const string fileName = "storage.xml";

        //Properties
        private string fullFilePath
        {
            get
            {
                // to get the location the assembly is executing from
                //(not necessarily where the it normally resides on disk)
                // in the case of the using shadow copies, for instance in NUnit tests, 
                // this will be in a temp directory.
                string path = System.Reflection.Assembly.GetExecutingAssembly().Location;

                //To get the location the assembly normally resides on disk or the install directory
                //string path = System.Reflection.Assembly.GetExecutingAssembly().CodeBase;

                //once you have the path you get the directory with:
                var directory = System.IO.Path.GetDirectoryName(path);
                
                directory += "\\" + dataFolderName;

                //if directory doesn't exist, try to create it
                if (!System.IO.Directory.Exists(directory))
                {
                    try
                    {
                        System.IO.Directory.CreateDirectory(directory);
                    }
                    catch { }
                }

                return directory + "\\" + fileName;
            }
        }

        //Methods
        public MemberList LoadMemberList()
        {
            try
            {
                MemberList loaded = null;

                if (System.IO.File.Exists(this.fullFilePath))
                {
                    using (FileStream readFileStream = new FileStream(this.fullFilePath, FileMode.Open, FileAccess.Read, FileShare.Read))
                    {
                        DataContractSerializer deserializer = new DataContractSerializer(typeof(MemberList));
                        loaded = (MemberList)deserializer.ReadObject(readFileStream);
                    }

                    if (loaded != null)
                    {
                        //setup subscriptions
                        loaded.SetupSubscriptions();
                    }

                    return loaded;
                }
            }
            catch { }

            return null;
        }
        public bool SaveMemberList(MemberList a_memberList)
        {
            try
            {
                DataContractSerializer serializer = new DataContractSerializer(typeof(MemberList));
                using (FileStream fs = File.Open(this.fullFilePath, FileMode.Create))
                {
                    serializer.WriteObject(fs, a_memberList);
                }

                return true;
            }
            catch { }

            return false;
        }


    }
}
