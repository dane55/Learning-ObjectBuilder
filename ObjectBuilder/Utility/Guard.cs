//===============================================================================
// Microsoft patterns & practices
// ObjectBuilder Application Block
//===============================================================================
// Copyright ?Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===============================================================================

using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using System.Reflection;

namespace Microsoft.Practices.ObjectBuilder
{
    /// <summary>
    /// ��֤��
    /// </summary>
	internal static class Guard
    {

        /// <summary>
        /// �����Ƿ���ݣ� ȷ����ǰ�� System.Type ��ʵ���Ƿ���Դ�ָ�� Type ��ʵ�����䡣
        /// </summary>
        /// <param name="assignee"></param>
        /// <param name="providedType"></param>
        /// <param name="classBeingBuilt"></param>
		public static void TypeIsAssignableFromType(Type assignee, Type providedType, Type classBeingBuilt)
        {
            if (!assignee.IsAssignableFrom(providedType))
                throw new IncompatibleTypesException(string.Format(CultureInfo.CurrentCulture, Properties.Resources.TypeNotCompatible, assignee, providedType, classBeingBuilt));
        }

        /// <summary>
        /// ���������Ƿ����
        /// </summary>
        /// <param name="methodInfo">�ṩ�йط����͹��캯������Ϣ��</param>
        /// <param name="parameters">��������</param>
        /// <param name="typeBeingBuilt"></param>
        public static void ValidateMethodParameters(MethodBase methodInfo, object[] parameters, Type typeBeingBuilt)
        {
            ParameterInfo[] paramInfos = methodInfo.GetParameters();
            for (int i = 0; i < paramInfos.Length; i++)
            {
                if (parameters[i] != null)
                {
                    Guard.TypeIsAssignableFromType(paramInfos[i].ParameterType, parameters[i].GetType(), typeBeingBuilt);
                }
            }
        }
    }
}
