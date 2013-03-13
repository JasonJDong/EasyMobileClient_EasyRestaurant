using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace DMobile.Biz.DataAccess.Manager.DataEntity
{
    [GeneratedCode("xsd", "2.0.50727.42")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    [XmlRoot(ElementName = "databases", Namespace = "http://www.easyrest.com/DatabaseList", IsNullable = false)]
    public class DatabaseList
    {
        [XmlElement(ElementName = "database")]
        public DatabaseMajor[] Databases { get; set; }
    }

    [GeneratedCode("xsd", "2.0.50727.42")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public class DatabaseMajor
    {
        [XmlElement(ElementName = "connection")]
        public ConnectionInfo Connection { get; set; }

        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }

        [XmlAttribute(AttributeName = "owner")]
        public string Owner { get; set; }

        [XmlAttribute(AttributeName = "dbType")]
        public string DBType { get; set; }
    }

    [GeneratedCode("xsd", "2.0.50727.42")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public class ConnectionInfo
    {
        [XmlElement(ElementName = "dataProvider")]
        public DataProvider DataProvider { get; set; }

        [XmlElement(ElementName = "dataServer")]
        public DataServer DataServer { get; set; }

        [XmlElement(ElementName = "dataSource")]
        public DataSource DataSource { get; set; }

        [XmlElement(ElementName = "dataUserID")]
        public DataUserID DataUserID { get; set; }

        [XmlElement(ElementName = "dataPassword")]
        public DataPassword DataPassword { get; set; }
    }

    [GeneratedCode("xsd", "2.0.50727.42")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public class ValueCommonBase
    {
        [XmlAttribute(AttributeName = "value")]
        public string Value { get; set; }
    }

    [GeneratedCode("xsd", "2.0.50727.42")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public class DataProvider : ValueCommonBase
    {
    }

    [GeneratedCode("xsd", "2.0.50727.42")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public class DataServer : ValueCommonBase
    {
    }

    [GeneratedCode("xsd", "2.0.50727.42")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public class DataSource : ValueCommonBase
    {
    }

    [GeneratedCode("xsd", "2.0.50727.42")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public class DataUserID : ValueCommonBase
    {
    }

    [GeneratedCode("xsd", "2.0.50727.42")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public class DataPassword : ValueCommonBase
    {
        [XmlIgnore]
        public string DecryptPassword { get; set; }
    }
}