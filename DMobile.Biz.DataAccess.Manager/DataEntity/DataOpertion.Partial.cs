using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text.RegularExpressions;

namespace DMobile.Biz.DataAccess.Manager.DataEntity
{
    partial class DataOperations
    {
        public IList<string> GetCommandNames()
        {
            if (DataCommand == null || DataCommand.Length == 0)
            {
                return new string[0];
            }

            var result = new List<string>(DataCommand.Length);

            for (int i = 0; i < DataCommand.Length; i++)
            {
                result.Add(DataCommand[i].Name);
            }
            return result;
        }
    }

    partial class DataOperationsDataCommand
    {
        /// <summary>
        /// <![CDATA[(?<!@)零宽度负预测先行断言，(?!@)零宽度负预测后发断言，表示不匹配@@此类系统变量]]>
        /// </summary>
        public const string REGEX_GET_PARAMS = "(?<!@)@(?!@)\\w+";

        /// <summary>
        /// <![CDATA[(?<!@)零宽度正预测先行断言，(?!@)零宽度负预测后发断言，表示不匹配通过Declare声明的本地变量]]>
        /// </summary>
        public const string REGEX_GET_LOCAL_PARAMS = "(?<=declare\\s*)@(?!@)\\w+";

        public DataCommand GetDataCommand(string defaultDb, DbUtility actualData)
        {
            //DbCommand dbCommand = GetDbCommand(actualData);
            DbCommand dbCommand = GetDbCommandFromCommandText(actualData);
            string database = Database ?? defaultDb;
            return new DataCommand(database, dbCommand);
        }

        private DbCommand GetDbCommand(DbUtility actualData)
        {
            DbCommand cmd = actualData.CreateDbCommand();
            if (cmd != null)
            {
                cmd.CommandText = CommandText.Trim();
                cmd.CommandTimeout = TimeOut;
                cmd.CommandType = (CommandType) Enum.Parse(typeof (CommandType), CommandType.ToString());

                if (Parameters != null && Parameters.Param != null && Parameters.Param.Length > 0)
                {
                    foreach (DataOperationsDataCommandParametersParam param in Parameters.Param)
                    {
                        cmd.Parameters.Add(param.GetDbParameter(cmd));
                    }
                }

                return cmd;
            }
            return null;
        }

        private DbCommand GetDbCommandFromCommandText(DbUtility actualData)
        {
            DbCommand cmd = actualData.CreateDbCommand();
            if (cmd != null)
            {
                cmd.CommandText = CommandText.Trim();
                cmd.CommandTimeout = TimeOut;
                cmd.CommandType = (CommandType) Enum.Parse(typeof (CommandType), CommandType.ToString());

                MatchCollection paramsName = Regex.Matches(CommandText, REGEX_GET_PARAMS, RegexOptions.IgnoreCase);
                MatchCollection localDefParams = Regex.Matches(CommandText, REGEX_GET_LOCAL_PARAMS,
                                                               RegexOptions.IgnoreCase);

                var tempParams = new List<string>();
                var tempLocalParams = new List<string>();

                foreach (Match match in paramsName)
                {
                    tempParams.Add(match.Value);
                }

                foreach (Match match in localDefParams)
                {
                    tempLocalParams.Add(match.Value);
                }

                tempParams.RemoveAll(tempLocalParams.Contains);

                foreach (string match in tempParams)
                {
                    if (!cmd.Parameters.Contains(match))
                    {
                        DbParameter param = cmd.CreateParameter();
                        param.ParameterName = match;
                        param.Direction = ParameterDirection.Input;
                        param.DbType = DbType.String;
                        cmd.Parameters.Add(param);
                    }
                }

                return cmd;
            }
            return null;
        }
    }

    partial class DataOperationsDataCommandParametersParam
    {
        public DbParameter GetDbParameter(DbCommand command)
        {
            var dbType = (DbType) Enum.Parse(typeof (DbType), DbType.ToString());

            DbParameter param = command.CreateParameter();
            param.ParameterName = Name;
            param.DbType = dbType;
            param.Direction = (ParameterDirection) Enum.Parse(typeof (ParameterDirection), Direction.ToString());

            if (Size != -1)
            {
                param.Size = Size;
            }

            return param;
        }
    }
}