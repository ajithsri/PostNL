using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace PostNL.Provider
{
    /// <summary>
    /// Represents a custom message header.
    /// </summary>
    public class PostNLLCCustomMessageHeader : MessageHeader
    {
        private const string HeaderName = "Security";
        private const string HeaderNamespace = "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd";


        /// <summary>
        /// Gets the name of the message header.
        /// </summary>
        /// <returns>The name of the message header.</returns>
        public override string Name
        {
            get { return HeaderName; }
        }

        /// <summary>
        /// Gets the namespace of the message header.
        /// </summary>
        /// <returns>The namespace of the message header.</returns>
        public override string Namespace
        {
            get { return HeaderNamespace; }
        }


        /// <summary>
        /// Called when the header content is serialized using the specified XML writer.
        /// </summary>
        /// <param name="writer">
        /// An <see cref="T:System.Xml.XmlDictionaryWriter" /> that is used to serialize the header contents.
        /// </param>
        /// <param name="messageVersion">
        /// The object that contains information related to the version of SOAP associated with a message and its exchange.
        /// </param>
        protected override void OnWriteHeaderContents(XmlDictionaryWriter writer, MessageVersion messageVersion)
        {
            var userName = ConfigurationManager.AppSettings["PostNL_UserName"];
            var password = ConfigurationManager.AppSettings["PostNL_Password"];
            writer.WriteStartElement("UsernameToken");
            writer.WriteStartElement("Username");
            writer.WriteValue(userName);
            writer.WriteEndElement();
            writer.WriteStartElement("Password");
            writer.WriteValue(password);
            writer.WriteEndElement();
            writer.WriteEndElement();
        }
    }
}
