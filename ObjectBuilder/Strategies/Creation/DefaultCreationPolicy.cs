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
using System.Reflection;

namespace Microsoft.Practices.ObjectBuilder
{
    /// <summary>
    /// �򵥵�Ĭ�ϴ������ԣ���ѡ�����ĵ�һ���������캯����ʹ��������������/�������캯��������κβ�����
    /// ObjectBuilder�ṩ�Ĺ��칹�캯�������������
    /// </summary>
    public class DefaultCreationPolicy : ICreationPolicy
    {
        /// <summary>
        /// ѡ�����ڴ�������Ĺ��캯��
        /// </summary>
        /// <param name="context">����ִ��������</param>
        /// <param name="typeToBuild">��Ҫ�����Ķ��������</param>
        /// <param name="idToBuild">��Ҫ�����Ķ����Ψһ���</param>
        /// <returns>Ҫʹ�õĹ��캯��������Ҳ������ʵĹ��캯�����򷵻�null</returns>
        public ConstructorInfo SelectConstructor(IBuilderContext context, Type typeToBuild, string idToBuild)
        {
            ConstructorInfo[] constructors = typeToBuild.GetConstructors();

            if (constructors.Length > 0)
                return constructors[0];

            return null;
        }

        /// <summary>
        /// ��ȡҪ���ݸ����캯���Ĳ���ֵ
        /// </summary>
        /// <param name="context">����ִ��������</param>
        /// <param name="type">��Ҫ�����Ķ��������</param>
        /// <param name="id">��Ҫ�����Ķ����Ψһ���</param>
        /// <param name="constructor">�����๹�캯�������Բ��ṩ�Թ��캯��Ԫ���ݵķ���Ȩ</param>
        /// <returns>���ݸ����캯���Ĳ�������</returns>
		public object[] GetParameters(IBuilderContext context, Type type, string id, ConstructorInfo constructor)
        {
            ParameterInfo[] parms = constructor.GetParameters();
            object[] parmsValueArray = new object[parms.Length];

            for (int i = 0; i < parms.Length; ++i)
                parmsValueArray[i] = context.HeadOfChain.BuildUp(context, parms[i].ParameterType, null, id);

            return parmsValueArray;
        }
    }
}
