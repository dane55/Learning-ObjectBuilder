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
using System.Reflection;

namespace Microsoft.Practices.ObjectBuilder
{
    /// <summary>
    /// �������ԣ�����ѡ��Ĺ��캯�������û��ṩ�Ĳ��������ģ� ObjectBuilder�ṩ�Ĺ��칹�캯�������������
    /// </summary>
    public class ConstructorPolicy : ICreationPolicy
    {
        private ConstructorInfo constructor;  //�����๹�캯�������Բ��ṩ�Թ��캯��Ԫ���ݵķ���Ȩ
        private List<IParameter> parameters = new List<IParameter>();  //��ǰ�����캯���Ĳ����б�

        /// <summary>
        /// ʵ���� <see cref="ConstructorPolicy"/> ��.
        /// </summary>
        public ConstructorPolicy() { }

        /// <summary>
        /// ʵ���� <see cref="ConstructorPolicy"/> �࣬�����ṩ�Ĳ�������ʹ�÷���������Ҫ���õĹ��캯��
        /// </summary>
        /// <param name="parameters">���ݸ����캯���Ĳ���</param>
        public ConstructorPolicy(params IParameter[] parameters)
        {
            foreach (IParameter parameter in parameters)
            {
                AddParameter(parameter);
            }
        }

        /// <summary>
        /// ʵ���� <see cref="ConstructorPolicy"/> �࣬ ʹ���ṩ�� <see cref="ConstructorInfo"/> �͹��캯������.
        /// </summary>
        /// <param name="constructor">�����๹�캯�������Բ��ṩ�Թ��캯��Ԫ���ݵķ���Ȩ</param>
        /// <param name="parameters">���ݸ����캯���Ĳ���</param>
        public ConstructorPolicy(ConstructorInfo constructor, params IParameter[] parameters)
            : this(parameters)
        {
            this.constructor = constructor;
        }

        /// <summary>
        /// ��������ӵ����ڲ��ҹ��캯���Ĳ����б���
        /// </summary>
        /// <param name="parameter">��Ҫ��ӵĲ���</param>
        public void AddParameter(IParameter parameter)
        {
            parameters.Add(parameter);
        }

        /// <summary>
        /// �������ʽ��������Ĺ��캯��ConstructorInfo������
        /// </summary>
        /// <param name="context">����ִ��������</param>
        /// <param name="type">��Ҫ�����Ķ��������</param>
        /// <param name="id">��Ҫ�����Ķ����Ψһ���</param>
        /// <returns>Ҫʹ�õĹ��캯��������Ҳ������ʵĹ��캯�����򷵻�null</returns>
        public ConstructorInfo SelectConstructor(IBuilderContext context, Type type, string id)
        {
            if (constructor != null)
            {
                return constructor;
            }
            List<Type> types = new List<Type>();

            foreach (IParameter parm in parameters)
            {
                types.Add(parm.GetParameterType(context));
            }
            return type.GetConstructor(types.ToArray());
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
            List<object> results = new List<object>();

            foreach (IParameter parm in parameters)
                results.Add(parm.GetValue(context));

            return results.ToArray();
        }
    }
}
