using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Xml.Serialization;

namespace DMobile.Biz.DataAccess.Manager.DataEntity
{
    /// <remarks/>
    [GeneratedCode("xsd", "2.0.50727.42")]
    [Serializable]
    //[System.Diagnostics.DebuggerStepThroughAttribute()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    [XmlRoot(ElementName = "dataOperations", Namespace = "http://www.easyrest.com/DataOperations", IsNullable = false)]
    public partial class DataOperations
    {
        [XmlElement(ElementName = "defaultSetting")]
        public DefaultSetting DefaultSetting { get; set; }

        /// <remarks/>
        [XmlElement(ElementName = "dataCommand")]
        public DataOperationsDataCommand[] DataCommand { get; set; }
    }

    public class DefaultSetting
    {
        [XmlAttribute(AttributeName = "database")]
        public string Database { get; set; }
    }

    /// <remarks/>
    [GeneratedCode("xsd", "2.0.50727.42")]
    [Serializable]
    //[System.Diagnostics.DebuggerStepThroughAttribute()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public partial class DataOperationsDataCommand
    {
        public DataOperationsDataCommand()
        {
            CommandType = DataOperationsDataCommandCommandType.Text;
            TimeOut = 300;
        }

        /// <remarks/>
        [XmlElement(ElementName = "commandText")]
        public string CommandText { get; set; }

        /// <remarks/>
        [XmlElement(ElementName = "parameters")]
        public DataOperationsDataCommandParameters Parameters { get; set; }

        /// <remarks/>
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }

        /// <remarks/>
        [XmlAttribute(AttributeName = "database")]
        public string Database { get; set; }

        /// <remarks/>
        [XmlAttribute(AttributeName = "commandType"), DefaultValue(DataOperationsDataCommandCommandType.Text)]
        public DataOperationsDataCommandCommandType CommandType { get; set; }

        /// <remarks/>
        [XmlAttribute(AttributeName = "timeOut"), DefaultValue(300)]
        public int TimeOut { get; set; }
    }

    /// <remarks/>
    [GeneratedCode("xsd", "2.0.50727.42")]
    [Serializable]
    //[System.Diagnostics.DebuggerStepThroughAttribute()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public class DataOperationsDataCommandParameters
    {
        /// <remarks/>
        [XmlElement(ElementName = "param")]
        public DataOperationsDataCommandParametersParam[] Param { get; set; }
    }

    /// <remarks/>
    [GeneratedCode("xsd", "2.0.50727.42")]
    [Serializable]
    //[System.Diagnostics.DebuggerStepThroughAttribute()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public partial class DataOperationsDataCommandParametersParam
    {
        public DataOperationsDataCommandParametersParam()
        {
            Direction = DataOperationsDataCommandParametersParamDirection.Input;
            DbType = DataOperationsDataCommandParametersParamDbType.String;
            Size = -1;
        }

        /// <remarks/>
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }

        /// <remarks/>
        [XmlAttribute(AttributeName = "dbType")]
        public DataOperationsDataCommandParametersParamDbType DbType { get; set; }

        /// <remarks/>
        [XmlAttribute(AttributeName = "direction"),
         DefaultValue(DataOperationsDataCommandParametersParamDirection.Input)]
        public DataOperationsDataCommandParametersParamDirection Direction { get; set; }

        /// <remarks/>
        [XmlAttribute(AttributeName = "size"), DefaultValue(-1)]
        public int Size { get; set; }
    }

    /// <remarks/>
    [GeneratedCode("xsd", "2.0.50727.42")]
    [Serializable]
    [XmlType(AnonymousType = true)]
    public enum DataOperationsDataCommandParametersParamDbType
    {
        /// <remarks/>
        AnsiString,

        /// <remarks/>
        AnsiStringFixedLength,

        /// <remarks/>
        Binary,

        /// <remarks/>
        Boolean,

        /// <remarks/>
        Byte,

        /// <remarks/>
        Currency,

        /// <remarks/>
        Date,

        /// <remarks/>
        DateTime,

        /// <remarks/>
        Decimal,

        /// <remarks/>
        Double,

        /// <remarks/>
        Int16,

        /// <remarks/>
        Int32,

        /// <remarks/>
        Int64,

        /// <remarks/>
        SByte,

        /// <remarks/>
        Single,

        /// <remarks/>
        String,

        /// <remarks/>
        StringFixedLength,

        /// <remarks/>
        Time,

        /// <remarks/>
        UInt16,

        /// <remarks/>
        UInt32,

        /// <remarks/>
        UInt64,

        /// <remarks/>
        VarNumeric,

        /// <remarks/>
        Xml,
    }

    /// <remarks/>
    [GeneratedCode("xsd", "2.0.50727.42")]
    [Serializable]
    [XmlType(AnonymousType = true)]
    public enum DataOperationsDataCommandParametersParamDirection
    {
        /// <remarks/>
        Input,

        /// <remarks/>
        InputOutput,

        /// <remarks/>
        Output,

        /// <remarks/>
        ReturnValue,
    }

    /// <remarks/>
    [GeneratedCode("xsd", "2.0.50727.42")]
    [Serializable]
    [XmlType(AnonymousType = true)]
    public enum DataOperationsDataCommandCommandType
    {
        /// <remarks/>
        StoredProcedure,

        /// <remarks/>
        TableDirect,

        /// <remarks/>
        Text,
    }
}