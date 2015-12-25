using System;
using System.IO;
using System.Xml;
using System.Collections;
using System.Collections.Generic;

namespace UBB2HTML
{
	internal struct tagReguex
	{
		public string Regex;
		public string Rule;
	}
	/*
	 * the xml file format is
	 * <rules>
	 *     <version ver="ver" />
	 *     <rule>
	 *         <regex>the match regex</regex>
	 *         <replace>replace rules</replace>
	 *     </rule>
	 *     <rule>.....</rule>
	 *     ......
	 * </rules>
	 */
	public sealed class RulesConfigure:IDisposable
	{
		private Stream xmlFile = null;
		private bool isDisposed;
		private List<tagReguex> lstRules;
		public static readonly string Version = "1.0";

		/// <summary>
		/// Initializes a new instance of the <see cref="UBB2HTM.RulesConfigure"/> class.
		/// </summary>
		/// <param name="file">File.</param>
		public RulesConfigure (string file)
		{
			this.xmlFile = File.Open (file, FileMode.Open);
			this.isDisposed = false;
			this.Load ();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="UBB2HTM.RulesConfigure"/> class.
		/// </summary>
		/// <param name="file">File.</param>
		public RulesConfigure (Stream file)
		{
			this.xmlFile = file;
			this.isDisposed = false;
			this.Load ();
		}

		/// <summary>
		/// Load this instance.
		/// </summary>
		private void Load ()
		{
			XmlDocument xml = new XmlDocument ();
			using (XmlTextReader xmlreader = new XmlTextReader (this.xmlFile)) {
				xml.Load (xmlreader);
				XmlElement root = xml.DocumentElement;
				if (root.Name != "rules") {
					throw new BadXmlConfigureFile ();
				}
				XmlNodeList vernode = xml.GetElementsByTagName ("version");
				if (vernode.Count != 1) {
					throw new BadXmlConfigureFile ("version Error", 
					                               "version", 
					                               "Could not Get Versin Element");
				}
				XmlAttributeCollection nodeAttributes = vernode [0].Attributes;
				if (nodeAttributes.Count != 1) {
					throw new BadXmlConfigureFile ("Version Error",
					                               vernode [0].Name,
					                               "Attribute Error");
				}
				XmlAttribute nodeAttr = nodeAttributes [0];
				if (nodeAttr.Name != "ver") {
					throw new BadXmlConfigureFile ("Attribute Error",
					                               vernode [0].Name,
					                               "Attribute Name Wrong");
				}
				if (nodeAttr.Value != RulesConfigure.Version) {
					throw new BadXmlConfigureFile ("Version is not be used",
					                               vernode [0].Name,
					                               "Version Error");
				}
				//where it all be checked

			}

		}
		#region IDisposable implementation
		public void Dispose ()
		{
			this.Dispose (true);
			GC.SuppressFinalize (this);
		}

		private void Dispose (bool isDisposing)
		{
			if (this.isDisposed)
				return;
			if (isDisposing) {

			}
			if (xmlFile != null) {
				xmlFile.Close ();
				xmlFile.Dispose ();
			}
			this.isDisposed = true;
		}

		~RulesConfigure ()
		{
			this.Dispose (false);
		}
		#endregion
	}
}

